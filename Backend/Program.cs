//middleware: lese mere om det

/* 
Forklaring:

Backend middleware sjekker f.eks. om brukeren er logget inn eller har lisens.
Hvis ikke, sender den en HTTP-feilkode (401 = Unauthorized, 403 = Forbidden).
React (frontend) får responsen og kan da vise riktig side eller melding.
Frontend (React) bestemmer hva som vises:
Du kan lage en komponent som sjekker om brukeren er logget inn (f.eks. token i localStorage).
Hvis ikke, viser du login-side.
Hvis backend svarer med “403 lisens mangler”, kan du vise en “Du har ikke tilgang”-side.

Eksempelflow:

Bruker åpner siden → React sjekker om token finnes.
React gjør API-kall → Middleware i C# sjekker token og lisens.
Hvis middleware blokkerer → React viser feilmelding eller redirect til login.
Hvis middleware tillater → React viser innhold basert på brukerens rettigheter.

Kort sagt: Middleware beskytter API-et og bestemmer om requesten går gjennom, mens frontend bestemmer hva brukeren ser basert på responsen.

2. Filter
Kjøres på controller eller action-nivå etter at routing er bestemt.
Opererer på spesifikke controller-metoder eller hele controlleren.
Typiske typer:
AuthorizationFilter – sjekker rettigheter på spesifikke actions
ActionFilter – kjører før og etter en action-metode
ExceptionFilter – fanger exceptions for en action/controller

  */
using Microsoft.EntityFrameworkCore;
using Backend.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Registers IgdbService in the dependency injection container 
// (Scoped = one instance per HTTP request)
builder.Services.AddScoped<IgdbService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Sett opp PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


app.UseCors("AllowReact");

app.MapControllers();


app.Run();