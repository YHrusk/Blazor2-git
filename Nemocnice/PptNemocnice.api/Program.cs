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
    Guid guid = Guid.NewGuid();
    prichoziModel.Id = guid;
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

app.MapPut("/vybaveni", (Guid Id) =>                                /*edit*/
{
    var stary = seznam.SingleOrDefault(x=>x.Id == Id);
    if (stary is null) return Results.NotFound("Položka nenalezena");

    var nove = new VybaveniModel
    {
        Id = stary.Id,
        Name = "AAAAA",
        BoughtDate = stary.BoughtDate,
        LastRevisionDate = stary.LastRevisionDate,
        IsInEditMode = stary.IsInEditMode,
        Price = 99999
    };

    seznam.Add(nove);
    seznam.Remove(stary);

    return Results.Ok();
});

//app.MapGet("/vybaveni{Id}", (Guid Id) =>
//{
//    var item = seznam.SingleOrDefault(x => x.Id == Id);
//    if (item == null) return Results.NotFound("Položka nenalezena");
//    return item;
//});


app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}