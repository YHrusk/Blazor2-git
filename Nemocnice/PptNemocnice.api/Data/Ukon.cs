namespace PptNemocnice.api.Data;

public class Ukon
{
    public string Name { get; set; } = "";
    public Guid Id { get; set; }
    public Guid VybaveniId { get; set; }
    public string Info { get; set; } = "";
    public DateTime? DateTime { get; set; }
    public Vybaveni Vybaveni { get; set; } = null!;
}
