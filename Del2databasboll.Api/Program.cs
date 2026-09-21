using Del2databasboll.Data;
using Del2databasboll.Repositories;
using Del2databasboll.Services;

var builder = WebApplication.CreateBuilder(args);

// [Nytt koncept: Dependency Injection]
// Varför: ASP.NET Core skapar och injicerar beroenden åt oss.
// Här registrerar vi lagerkedjan: Controller -> Service -> Repository.
builder.Services.AddSingleton<ISpelarRepository>(new SpelareRepository("spelare.db"));
builder.Services.AddSingleton<ISpelareService, SpelareService>();
builder.Services.AddSingleton<DatabaseInitializer>();

// [Nytt koncept: Controllers]
// Varför: Controllers mappar HTTP-anrop till C#-metoder (endpoints).
builder.Services.AddControllers();

var app = builder.Build();

// Initierar databas och seed vid uppstart.
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    initializer.Initiera();
}

app.MapControllers();

app.Run();
