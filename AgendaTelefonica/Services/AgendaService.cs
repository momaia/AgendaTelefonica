using AgendaTelefonica.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<List<Agenda>> GetByPhoneAsync(string telefone)
    {
        var filter = Builders<Agenda>.Filter.Or(
        Builders<Agenda>.Filter.Regex(x => x.Telefone, new BsonRegularExpression(telefone)),
        Builders<Agenda>.Filter.Regex(x => x.TelefoneCasa, new BsonRegularExpression(telefone)),
        Builders<Agenda>.Filter.Regex(x => x.TelefoneTrabalho, new BsonRegularExpression(telefone)),
        Builders<Agenda>.Filter.Regex(x => x.TelefoneOutro, new BsonRegularExpression(telefone))
        );

        var result = await _agendaCollection.Find(filter).ToListAsync();

        return result;
    }    

    public async Task<List<Agenda>> GetByEmailAsync(string email)
    {
        var filter = Builders<Agenda>.Filter.Or(
        Builders<Agenda>.Filter.Regex(x => x.Email, new BsonRegularExpression(email)),
        Builders<Agenda>.Filter.Regex(x => x.EmailCasa, new BsonRegularExpression(email)),
        Builders<Agenda>.Filter.Regex(x => x.EmailTrabalho, new BsonRegularExpression(email)),
        Builders<Agenda>.Filter.Regex(x => x.EmailOutro, new BsonRegularExpression(email))
        );

        var result = await _agendaCollection.Find(filter).ToListAsync();

        return result;
    }

    public async Task CreateAsync(Agenda novoContato) =>
        await _agendaCollection.InsertOneAsync(novoContato);

    public async Task UpdateAsync(string id, Agenda atualizaContato) =>
        await _agendaCollection.ReplaceOneAsync(x => x.Id == id, atualizaContato);

    public async Task RemoveAsync(string id) =>
        await _agendaCollection.DeleteOneAsync(x => x.Id == id);
}