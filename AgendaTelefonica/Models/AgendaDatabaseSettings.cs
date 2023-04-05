namespace AgendaTelefonica.Models;

public class AgendaDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string AgendaCollection { get; set; } = null!;
}
