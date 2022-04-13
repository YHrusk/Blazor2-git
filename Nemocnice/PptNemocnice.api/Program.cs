using PptNemocnice.Shared;
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:7132")
    .WithMethods("GET", "POST", "DELETE", "PUT")
    .AllowAnyHeader()
));

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

List<VybaveniModel> seznam = VybaveniModel.Generovat();

app.MapGet("/vybaveni", () =>
{
    return seznam;
});

app.MapPost("/vybaveni", (VybaveniModel prichoziModel) =>           /*create*/
{
    prichoziModel.Id = Guid.NewGuid();
    seznam.Insert(0, prichoziModel);
    return Results.Created("/vybaveni", prichoziModel.Id);
});

app.MapDelete("/vybaveni/{Id}", (Guid Id) =>
{
    var item = seznam.SingleOrDefault(x=>x.Id == Id);
    if (item == null) return Results.NotFound("Položka nenalezena");
    seznam.Remove(item);
    return Results.Ok();
});

app.MapPut("/vybaveni", (VybaveniModel prichoziModel) =>                                /*edit*/
{
    var staryZaznam = seznam.SingleOrDefault(x => x.Id == prichoziModel.Id);
    if (staryZaznam == null) return Results.NotFound("Tento záznam není v seznamu");
    int ind = seznam.IndexOf(staryZaznam);
    seznam.Insert(ind, prichoziModel);
    seznam.Remove(staryZaznam);
    return Results.Ok();
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>
{
    var item = seznam.SingleOrDefault(x => x.Id == Id);
    if (item != null) return Results.Json(item);
    else return Results.NotFound();
});

List<RevizeModel> listRevizi = RevizeModel.Vygenerovat();

app.MapGet("/revize/{vyhledavanyRetezec}", (string vyhledavanyRetezec) =>
{
    if (string.IsNullOrWhiteSpace(vyhledavanyRetezec)) return Results.Problem("Parametr musi byt neprazdny");
    var kdeJeRetezec = listRevizi.Where(x => x.Name.Contains(vyhledavanyRetezec));
    return Results.Json(kdeJeRetezec);
});


app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}