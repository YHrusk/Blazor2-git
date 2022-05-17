using AutoMapper;
using PptNemocnice.Shared;
using PptNemocnice.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NemocniceDbContext>(opt => opt.UseSqlite("FileName=Nemocnice.db"));

//Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(builder.Configuration["AllowedOrigins"])
    .WithMethods("GET", "POST", "DELETE", "PUT")
    .AllowAnyHeader()
));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors();

app.MapControllers();

app.MapGet("/", () => "Hello");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//List<VybaveniModel> seznam = VybaveniModel.Generovat();

//app.MapGet("/vybaveni", (NemocniceDbContext db, IMapper mapper) =>              //ziskani vsech vybevni s jejich nejnovejsi revizi
//{
//    List<VybaveniModel> models = new();

//    var ents = db.Vybavenis.Include(x => x.Revizes);

//    foreach (var ent in ents)
//    {
//        VybaveniModel vybaveni = mapper.Map<VybaveniModel>(ent);
//        vybaveni.LastRevisionDate = ent.Revizes.OrderByDescending(x => x.DateTime).FirstOrDefault()?.DateTime;
//        models.Add(vybaveni);
//    }
//    return Results.Json(models);
//});

//app.MapPost("/vybaveni", (VybaveniModel prichoziModel, NemocniceDbContext db, IMapper mapper) =>           /*create*/
//{
//    prichoziModel.Id = Guid.Empty;                                //databaze si ID spravuje sama
//    Vybaveni ent = mapper.Map<Vybaveni>(prichoziModel);           //mapovani prichozihoModelu na Vybaveni
//    db.Vybavenis.Add(ent);
//    db.SaveChanges();

//    //
//    return Results.Created("/vybaveni", ent.Id);
//});

//app.MapDelete("/vybaveni/{Id}", (Guid Id, NemocniceDbContext db, IMapper mapper) =>
//{
//    var item = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
//    if (item == null) return Results.NotFound("Položka nenalezena");
//    Vybaveni ent = mapper.Map<Vybaveni>(item);
//    db.Vybavenis.Remove(ent);
//    db.SaveChanges();
//    return Results.Ok();
//});

//app.MapPut("/vybaveni", (VybaveniModel prichoziModel, NemocniceDbContext db, IMapper mapper) =>                             /*edit*/
//{
//    var staryZaznam = db.Vybavenis.SingleOrDefault(x => x.Id == prichoziModel.Id);
//    if (staryZaznam == null) return Results.NotFound("Položka nenalezena");

//    mapper.Map(prichoziModel, staryZaznam);
//    db.SaveChanges();
//    return Results.Ok();
//});

//app.MapGet("/vybaveni/{Id}", (Guid Id, NemocniceDbContext db, IMapper mapper) =>            //detail vybaveni??
//{
//    var ents = db.Vybavenis.Include(x => x.Revizes).SingleOrDefault(x => x.Id == Id);
//    if (ents == null) return Results.NotFound("nenalezeno");

//    VybaveniSRevizesModel vybaveni = mapper.Map<VybaveniSRevizesModel>(ents);
//    return Results.Json(vybaveni);
//    //var item = db.Vybavenis.Include(x => x.Revizes).SingleOrDefault(x => x.Id == Id);
//    //if (item != null) return Results.Json(mapper.Map<VybaveniModel>(item));
//    ////specifikovat mapping - VybaveniSREvizi?
//    //else return Results.NotFound();
//});

//List<RevizeModel> listRevizi = RevizeModel.Vygenerovat();

app.MapGet("/revize/{vyhledavanyRetezec}", (string vyhledavanyRetezec, NemocniceDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(vyhledavanyRetezec)) return Results.Problem("Parametr musi byt neprazdny");

    var kdeJeRetezec = db.Revizes.Where(x => x.Name.Contains(vyhledavanyRetezec));
    return Results.Json(kdeJeRetezec);
});

app.MapPost("/revize", (RevizeModel revizeModel, NemocniceDbContext db, IMapper mapper) =>
{    
    revizeModel.Id = Guid.Empty;                        //vynulovat id, db si id poresi sama
    revizeModel.DateTime = DateTime.UtcNow;             //pridani datumu na serveru
    Revize ent = mapper.Map<Revize>(revizeModel);
    db.Revizes.Add(ent);
    db.SaveChanges();
    return Results.Created("/revize", new RevizeCreatedResponseModel(ent.Id, ent.DateTime));
});

app.MapPost("/ukon", (UkonModel ukonModel, NemocniceDbContext db, IMapper mapper) =>
{
    ukonModel.Id = Guid.Empty;
    var posledniRevizeDatum = db.Vybavenis.Include(x => x.Revizes)      //vybaveni vcetne revizi
    .SingleOrDefault(x => x.Id == ukonModel.VybaveniId)?        //konkretni vybaveni
    .Revizes.OrderBy(x => x.DateTime)       //vsechny revize serazene od nejstarsi
    .LastOrDefault()?.DateTime;     //posledni revize (nejnovejsi) a jeji datum
    if (posledniRevizeDatum == null || posledniRevizeDatum.Value.AddYears(2) < DateTime.UtcNow) //overeni rozdilu 2 let
        return Results.BadRequest("Nemùže se pøidat úkon, pokud je revize starší než 2 roky");

    Ukon ent = mapper.Map<Ukon>(ukonModel);
    db.Ukons.Add(ent);
    db.SaveChanges();
    return Results.Created("/ukon", ent.Id);
});

app.MapGet("/seed/{tajnyKod}", (string tajnyKod, NemocniceDbContext db, IConfiguration config) =>
{
    if (tajnyKod != config["seedSecrete"])
        return Results.NotFound();

    Random rnd = new();
    List<Pracovnik> pracanti = new();
    int pocetPracantu = 10;
    for (int i = 0; i < pocetPracantu; i++)
        pracanti.Add(new() { Name = RandomString(12) });

    db.AddRange(pracanti); db.SaveChanges();

    foreach (var vyb in db.Vybavenis)//pro každé vybavení
    {
        int pocetUkonu = rnd.Next(13, 25);
        for (int i = 0; i < pocetUkonu; i++)//se vytvoøí nìkolik úkonù
        {
            Ukon uk = new()
            {
                DateTime = DateTime.UtcNow.AddDays(rnd.Next(-100, -1)),
                Info = RandomString(56).Replace("x", " "),
                Kod = RandomString(5),
                VybaveniId = vyb.Id,//daného vybavení
                PracovnikId = pracanti[rnd.Next(pocetPracantu - 1)].Id
            };
            db.Ukons.Add(uk);
        }
    }
    db.SaveChanges();

    return Results.Ok();

    string RandomString(int length) =>//lokální funkce
        new(Enumerable.Range(0, length).Select(_ => (char)rnd.Next('a', 'z')).ToArray());
});


app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}