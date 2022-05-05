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
        Guid idVybaveniA = new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9");
        Guid idVybaveniB = new Guid("39d4018a-ba22-4ab6-b582-0d1c35e79e45");
        builder.Entity<Vybaveni>().HasData(
            new Vybaveni() { Id = idVybaveniRevizi, Name="XXX", BoughtDate = new DateTime(2018, 5, 28), Price = 555 },
            new Vybaveni() { Id = idVybaveniA, Name = "AZU", BoughtDate = new DateTime(2000, 1, 12), Price = 10888 },
            new Vybaveni() { Id = idVybaveniB, Name = "JQW", BoughtDate = new DateTime(2020, 7, 16), Price = 999 }
            );
        builder.Entity<Revize>().HasData(
            new Revize() { Id = new Guid("01631c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name="AAA", DateTime = new DateTime(2019, 8, 6) },
            new Revize() { Id = new Guid("aa7a1c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "BBB", DateTime = new DateTime(2020, 12, 31) },
            new Revize() { Id = new Guid("bb0b1c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "CCC", DateTime = new DateTime(1999, 1, 19) },
            new Revize() { Id = new Guid("cc9b1c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniA, Name = "DDD", DateTime = new DateTime(2018, 10, 29) }
            );
        builder.Entity<Ukon>().HasData(
            new Ukon() { Id = new Guid("cc771c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name="HHH", DateTime = new DateTime(2019, 12, 28), Info= "Aliquam ornare" },
            new Ukon() { Id = new Guid("dd881c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "ZZZ", DateTime = new DateTime(2000, 1, 1), Info = "Neque porro" },
            new Ukon() { Id = new Guid("ee991c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "MMM", DateTime = new DateTime(2016, 8, 13), Info = "Duis risus" },
            new Ukon() { Id = new Guid("ff001c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniRevizi, Name = "III", DateTime = new DateTime(2022, 5, 20), Info = "Fusce suscipit" },
            new Ukon() { Id = new Guid("ab501c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniA, Name = "VVV", DateTime = new DateTime(2016, 4, 26), Info = "Cras id dolor" },
            new Ukon() { Id = new Guid("cb661c28-10e9-4a04-bcd1-91cbececad23"), VybaveniId = idVybaveniA, Name = "OOO", DateTime = new DateTime(2016, 9, 3), Info = "Mauris dictum" }
            );
    }

    public DbSet<Vybaveni> Vybavenis => Set<Vybaveni>();              //metoda DbContextu, rika ze se jedna o tabulku
    public DbSet<Revize> Revizes => Set<Revize>();
    public DbSet<Ukon> Ukons => Set<Ukon>();
}
