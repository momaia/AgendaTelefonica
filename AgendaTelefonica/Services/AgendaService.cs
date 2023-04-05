using AgendaTelefonica.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AgendaTelefonica.Services;

public class AgendaService
{
    private readonly IMongoCollection<Agenda> _agendaCollection;

    public AgendaService(
        IOptions<AgendaDatabaseSettings> AgendaDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            AgendaDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            AgendaDatabaseSettings.Value.DatabaseName);

        _agendaCollection = mongoDatabase.GetCollection<Agenda>(
            AgendaDatabaseSettings.Value.AgendaCollection);
    }

    public async Task<List<Agenda>> GetAsync() =>
        await _agendaCollection.Find(_ => true).ToListAsync();

    public async Task<Agenda?> GetAsync(string id) =>
        await _agendaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Agenda>> GetByNameAsync(string nome) =>
        await _agendaCollection.Find(x => x.Nome.Contains(nome)).ToListAsync();

    public async Task<List<Agenda>> GetByPhoneAsync(string telefone) =>
        await _agendaCollection.Find(x => x.Telefone.Contains(telefone) 
        || x.TelefoneCasa.Contains(telefone)
        || x.TelefoneTrabalho.Contains(telefone)
        || x.TelefoneOutro.Contains(telefone)).ToListAsync();

    public async Task<List<Agenda>> GetByEmailAsync(string email) =>
        await _agendaCollection.Find(x => x.Email.Contains(email)
        || x.EmailCasa.Contains(email)
        || x.EmailTrabalho.Contains(email)
        || x.EmailOutro.Contains(email)).ToListAsync();

    public async Task CreateAsync(Agenda novoContato) =>
        await _agendaCollection.InsertOneAsync(novoContato);

    public async Task UpdateAsync(string id, Agenda atualizaContato) =>
        await _agendaCollection.ReplaceOneAsync(x => x.Id == id, atualizaContato);

    public async Task RemoveAsync(string id) =>
        await _agendaCollection.DeleteOneAsync(x => x.Id == id);
}