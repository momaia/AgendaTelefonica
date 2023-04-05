namespace AgendaTelefonica.Models;

public class Ligacao
{
    public Agenda Contato { get; set; }
    public string MensagemLigacao { get; set; }
    public DateTime HorarioLigacao { get; set; }
}
