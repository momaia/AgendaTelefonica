using AgendaTelefonica.Models;
using AgendaTelefonica.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace AgendaTelefonica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendaController : ControllerBase
{
    private readonly AgendaService _agendaService;
    private readonly ConnectionFactory _connectionFactory;
    private const string ligacoesQueue = "ligacoes";

    public AgendaController(AgendaService agendaService)
    {
        _agendaService = agendaService;
        _connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
    }

    [HttpGet]
    public async Task<List<Agenda>> ObterLista()
    {
        var result = await _agendaService.GetAsync();

        return result.OrderBy(obj => obj.Nome).ToList();
    }

    [HttpGet("ObterPorNome/{nome}")]
    public async Task<ActionResult<List<Agenda>>> ObterListaPorNome(string nome)
    {
        var result = await _agendaService.GetByNameAsync(nome);

        if (result is null)
        {
            return NotFound();
        }

        return result.OrderBy(obj => obj.Nome).ToList();
    }

    [HttpGet("ObterPorTelefone/{telefone}")]
    public async Task<ActionResult<List<Agenda>>> ObterListaPorTelefone(string telefone)
    {
        var result = await _agendaService.GetByPhoneAsync(telefone);

        if (result is null)
        {
            return NotFound();
        }

        return result.OrderBy(obj => obj.Nome).ToList();
    }

    [HttpGet("ObterPorEmail/{email}")]
    public async Task<ActionResult<List<Agenda>>> ObterListaPorEmail(string email)
    {
        var result = await _agendaService.GetByEmailAsync(email);

        if (result is null)
        {
            return NotFound();
        }

        return result.OrderBy(obj => obj.Nome).ToList();
    }

    [HttpPost("Incluir")]
    public async Task<IActionResult> Post(Agenda novoContato)
    {
        await _agendaService.CreateAsync(novoContato);

        return CreatedAtAction(nameof(ObterLista), new { id = novoContato.Id }, novoContato);
    }

    [HttpPut("Editar/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Agenda contatoAgenda)
    {
        var contato = await _agendaService.GetAsync(id);

        if (contato is null)
        {
            return NotFound();
        }

        contatoAgenda.Id = contato.Id;

        await _agendaService.UpdateAsync(id, contatoAgenda);

        return NoContent();
    }

    [HttpDelete("Deletar/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var contato = await _agendaService.GetAsync(id);

        if (contato is null)
        {
            return NotFound();
        }

        await _agendaService.RemoveAsync(id);

        return NoContent();
    }

    [HttpPost("Ligar")]
    public async Task<IActionResult> PublishMessage([FromBody] Mensagem mensagem)
    {
        var informacoesContato = await _agendaService.GetByPhoneAsync(mensagem.TelefoneContato);

        if (informacoesContato.Count == 0)
        {
            return NotFound();
        }

        var ligacao = new Ligacao()
        {
            Contato = informacoesContato.FirstOrDefault(),
            MensagemLigacao = mensagem.MensagemLigacao,
            HorarioLigacao = DateTime.Now
        };

        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var queueInfo = channel.QueueDeclarePassive(ligacoesQueue);
        var ligacaoJson = JsonConvert.SerializeObject(ligacao);
        var ligacaoBytes = Encoding.UTF8.GetBytes(ligacaoJson);

        channel.BasicPublish(
            exchange: "",
            routingKey: ligacoesQueue,
            basicProperties: null,
            body: ligacaoBytes);

        return Accepted();
    }
}