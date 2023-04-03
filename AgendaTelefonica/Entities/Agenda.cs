namespace ASP.NET_WebAPI6.Entities
{
    public partial class Agenda
    {
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Endereço { get; set; }
        public TelefonesContato Telefones { get; set; }
        public EmailsContato Emails { get; set; }
    }
}
