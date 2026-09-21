using Del2databasboll.Data;
using Del2databasboll.Api.Contracts;
using Del2databasboll.Repositories;
using Del2databasboll.Services;
using Microsoft.AspNetCore.Diagnostics;

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

// [Nytt koncept: Global felhantering]
// Varför: Vi vill fånga fel på ett ställe istället för try/catch i varje endpoint.
// ArgumentException -> 400 (klientfel), övriga fel -> 500 (serverfel).
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;

        context.Response.ContentType = "application/json";
        var traceId = context.TraceIdentifier;

        if (exception is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ApiError
            {
                Code = "validation_error",
                Message = exception.Message,
                TraceId = traceId
            });
            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new ApiError
        {
            Code = "server_error",
            Message = "Ett oväntat serverfel inträffade.",
            TraceId = traceId
        });
    });
});

// Initierar databas och seed vid uppstart.
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    initializer.Initiera();
}

app.MapControllers();

app.Run();
