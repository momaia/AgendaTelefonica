using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace AgendaTelefonica.Models;

public class Agenda
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    [Required]
    public string Nome { get; set; }
    [Required]
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

    public static implicit operator Agenda(List<Agenda> v)
    {
        throw new NotImplementedException();
    }
}