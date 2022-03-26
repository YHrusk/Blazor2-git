using PptNemocnice.Shared;
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
    return Results.Created("/vybaveni", prichoziModel);
});

app.MapDelete("/vybaveni/{Id}", (Guid Id) =>
{
    var item = seznam.SingleOrDefault(x=>x.Id == Id);
    if (item == null) return Results.NotFound("Položka nenalezena");
    seznam.Remove(item);
    return Results.Ok();
});

app.MapPut("/vybaveni", (Guid Id, VybaveniModel prichoziModel) =>                                /*edit*/
{
    var entity = seznam.SingleOrDefault(x=>x.Id == Id);
    if (entity is null) return Results.NotFound("Položka nenalezena");

    entity.Id = prichoziModel.Id;
    entity.Name = prichoziModel.Name;
    entity.BoughtDate = prichoziModel.BoughtDate;
    entity.LastRevisionDate = prichoziModel.LastRevisionDate;
    entity.IsInEditMode = prichoziModel.IsInEditMode;
    entity.Price = prichoziModel.Price;

    seznam.Add(entity);
    seznam.Remove(prichoziModel);

    return Results.Ok();
});

app.MapGet("/vybaveni{Id}", (Guid Id) =>
{
    var item = seznam.SingleOrDefault(x => x.Id == Id);
    if (item != null) return Results.Json(item);
    else return Results.NotFound();
});


app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}