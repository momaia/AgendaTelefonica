using Microsoft.AspNetCore.Mvc;
using AgendaTelefonica.Models;
using AgendaTelefonica.Services;

namespace AgendaTelefonica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendaController : ControllerBase
{
    private readonly AgendaService _agendaService;

    public AgendaController(AgendaService agendaService) =>
        _agendaService = agendaService;

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
}