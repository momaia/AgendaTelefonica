namespace ASP.NET_WebAPI6.Entities
{
    public partial class TelefonesContato
    {
        public int IdTelefonesContato { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneCasa { get; set; }
        public string TelefoneTrabalho { get; set; }
        public string EmailOutro { get; set; }
        public Agenda FkContato { get; set; }
    }
}
