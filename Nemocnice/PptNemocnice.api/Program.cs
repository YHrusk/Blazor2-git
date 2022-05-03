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
    policy.WithOrigins("https://localhost:7132")
    .WithMethods("GET", "POST", "DELETE", "PUT")
    .AllowAnyHeader()
));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//List<VybaveniModel> seznam = VybaveniModel.Generovat();

app.MapGet("/vybaveni", (NemocniceDbContext db, IMapper mapper) =>              //ziskani vsech vybevni s jejich nejnovejsi revizi
{
    List<VybaveniModel> models = new();

    var ents = db.Vybavenis.Include(x => x.Revizes);

    foreach (var ent in ents)
    {
        VybaveniModel vybaveni = mapper.Map<VybaveniModel>(ent);
        vybaveni.LastRevisionDate = ent.Revizes.OrderByDescending(x => x.DateTime).FirstOrDefault()?.DateTime;
        models.Add(vybaveni);
    }
    return Results.Json(models);
});

app.MapPost("/vybaveni", (VybaveniModel prichoziModel, NemocniceDbContext db, IMapper mapper) =>           /*create*/
{
    prichoziModel.Id = Guid.Empty;                                //databaze si ID spravuje sama
    Vybaveni ent = mapper.Map<Vybaveni>(prichoziModel);           //mapovani prichozihoModelu na Vybaveni
    db.Vybavenis.Add(ent);
    db.SaveChanges();

    //
    return Results.Created("/vybaveni", ent.Id);
});

app.MapDelete("/vybaveni/{Id}", (Guid Id, NemocniceDbContext db, IMapper mapper) =>
{
    var item = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (item == null) return Results.NotFound("Polo�ka nenalezena");
    Vybaveni ent = mapper.Map<Vybaveni>(item);
    db.Vybavenis.Remove(ent);
    db.SaveChanges();
    return Results.Ok();
});

app.MapPut("/vybaveni", (VybaveniModel prichoziModel, NemocniceDbContext db, IMapper mapper) =>                             /*edit*/
{
    var staryZaznam = db.Vybavenis.SingleOrDefault(x => x.Id == prichoziModel.Id);
    if (staryZaznam == null) return Results.NotFound("Polo�ka nenalezena");;

    mapper.Map(prichoziModel, staryZaznam);
    db.SaveChanges();
    return Results.Ok();
});

app.MapGet("/vybaveni/{Id}", (Guid Id, NemocniceDbContext db, IMapper mapper) =>            //detail vybaveni??
{
    var ents = db.Vybavenis.Include(x => x.Revizes).SingleOrDefault(x => x.Id == Id);
    if (ents == null) return Results.NotFound("nenalezeno");

    VybaveniSRevizesModel vybaveni = mapper.Map<VybaveniSRevizesModel>(ents);
    return Results.Json(vybaveni);
    //var item = db.Vybavenis.Include(x => x.Revizes).SingleOrDefault(x => x.Id == Id);
    //if (item != null) return Results.Json(mapper.Map<VybaveniModel>(item));
    ////specifikovat mapping - VybaveniSREvizi?
    //else return Results.NotFound();
});

//List<RevizeModel> listRevizi = RevizeModel.Vygenerovat();

app.MapGet("/revize/{vyhledavanyRetezec}", (string vyhledavanyRetezec, NemocniceDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(vyhledavanyRetezec)) return Results.Problem("Parametr musi byt neprazdny");

    var kdeJeRetezec = db.Revizes.Where(x => x.Name.Contains(vyhledavanyRetezec));
    return Results.Json(kdeJeRetezec);
});

app.MapPost("/revize", (RevizeModel revizeModel, NemocniceDbContext db, IMapper mapper) =>
{
    revizeModel.Id = Guid.Empty;    //vynulovat id, db si id poresi sama
    Revize ent = mapper.Map<Revize>(revizeModel);
    db.Revizes.Add(ent);
    db.SaveChanges();
    return Results.Created("/revize", ent.Id);
});


app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}