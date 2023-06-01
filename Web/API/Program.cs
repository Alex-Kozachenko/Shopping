using System.Text;
using Shopping.Contracts.Domain.Facade;
using Shopping.Domain.Facade.Import;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISupplyPositionsReader, MTSupplyPositionsReader>();

var app = builder.Build();

app.MapGet("/", async context => { await context.Response.WriteAsync("HI"); } );

app.MapPost("/upload", async (HttpContext context, ISupplyPositionsReader reader) => 
{
    var bodyReader = context.Request.BodyReader;

    
    await bodyReader.CompleteAsync();

    // var 

    // reader.Read(sb.ToString());
});

app.Run();
