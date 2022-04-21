using Microsoft.EntityFrameworkCore;

namespace PptNemocnice.api.Data;

public class NemocniceDbContext : DbContext         //dedi od EntityFrameworkCore
{
    public NemocniceDbContext(DbContextOptions<NemocniceDbContext> options) : base(options)         //pouzivam konstruktor od DbContext
    {

    }

    public DbSet<Vybaveni> Vybavenis => Set<Vybaveni>();              //metoda DbContextu, rika ze se jedna o tabulku
    public DbSet<Revize> Revizes => Set<Revize>();
}
