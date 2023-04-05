namespace AgendaTelefonica.Models;

public class Mensagem
{
    public string Ligacao { get; set; }
    public Agenda Contato { get; set; }

    public Mensagem()
    {
    }

    public Mensagem(string ligacao, Agenda contato)
    {
        Ligacao = ligacao;
        Contato = contato;
    }
}
