using Microsoft.EntityFrameworkCore;

namespace PptNemocnice.api.Data;

public class NemocniceDbContext : DbContext         //dedi od EntityFrameworkCore
{
    public NemocniceDbContext(DbContextOptions<NemocniceDbContext> options) : base(options)         //pouzivam konstruktor od DbContext
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Guid idVybaveniRevizi = new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a");
        builder.Entity<Vybaveni>().HasData(
            new Vybaveni() { Id = idVybaveniRevizi, Name="XXX", BoughtDate = new DateTime(2018, 5, 28), Price = 555 },
            new Vybaveni() { Id = new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9"), Name = "AZU", BoughtDate = new DateTime(2018, 5, 28), Price = 555 },
            new Vybaveni() { Id = new Guid("39d4018a-ba22-4ab6-b582-0d1c35e79e45"), Name = "JQW", BoughtDate = new DateTime(2020, 7, 16), Price = 999 }
            );
        builder.Entity<Revize>().HasData(
            new Revize() { Id = new Guid("01631c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name="AAA", DateTime = new DateTime(2019, 8, 6) },
            new Revize() { Id = new Guid("aa7a1c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "BBB", DateTime = new DateTime(2020, 12, 31) },
            new Revize() { Id = new Guid("bb0b1c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "CCC", DateTime = new DateTime(1999, 1, 19) }
            );
    }

    public DbSet<Vybaveni> Vybavenis => Set<Vybaveni>();              //metoda DbContextu, rika ze se jedna o tabulku
    public DbSet<Revize> Revizes => Set<Revize>();
}
