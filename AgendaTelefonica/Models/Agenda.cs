using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AgendaTelefonica.Models;

public class Agenda
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string TelefoneCasa { get; set; } = null!;
    public string TelefoneTrabalho { get; set; } = null!;
    public string TelefoneOutro { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string EmailCasa { get; set; } = null!;
    public string EmailTrabalho { get; set; } = null!;
    public string EmailOutro { get; set; } = null!;
    public string Empresa { get; set; } = null!;
    public string Endereço { get; set; } = null!;
}
