namespace ASP.NET_WebAPI6.Entities
{
    public partial class EmailsContato
    {
        public int IdmailContato { get; set; }
        public string Emailrincipal { get; set; }
        public string EmailCasa { get; set; }
        public string EmailTrabalho { get; set; }
        public string EmailOutro { get; set; }
        public Agenda FkContato { get; set; }
    }
}
