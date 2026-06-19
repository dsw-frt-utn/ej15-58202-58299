using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>(); // [cite: 33]


builder.Services.AddHealthChecks();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapibuilder
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); // [cite: 98]


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapHealthChecks("/health-check"); // 

app.MapControllers();

app.Run();
