# Del2databasboll

En enkel C#-lösning för spelare med SQLite-databas.

Består nu av:
- en konsolapp (`Del2databasboll.csproj`)
- ett experiment-API (`Del2databasboll.Api/Del2databasboll.Api.csproj`)

## Ingår
- Skapar databasen `spelare.db` automatiskt.
- Skapar tabellen `spelare` vid första start.
- CRUD via meny:
  - Lägg till spelare
  - Visa alla spelare
  - Sök spelare
  - Ta bort spelare
  - Uppdatera spelare

## Struktur
- `Models/` - domänmodeller (`Spelare`)
- `Repositories/` - databasaccess (SQL)
- `Services/` - affärslogik mellan Program och Repository
- `Data/` - initiering/seed av databas
- `Program.cs` - meny/UI

## Kör
```bash
dotnet restore
dotnet run --project Del2databasboll.csproj
```

## Kör API
```bash
dotnet run --project Del2databasboll.Api/Del2databasboll.Api.csproj --urls http://localhost:5072
```

### Testa API-endpoints
```bash
curl http://localhost:5072/api/players
curl http://localhost:5072/api/players/4
```

```bash
curl -X POST http://localhost:5072/api/players \
  -H "Content-Type: application/json" \
  -d '{"id":12,"namn":"Lina","tröjnummer":11,"mål":5,"matcherSpelade":10}'
```

```bash
curl -X PUT http://localhost:5072/api/players/12 \
  -H "Content-Type: application/json" \
  -d '{"namn":"Lina","tröjnummer":11,"mål":7,"matcherSpelade":11}'
```

```bash
curl -X DELETE http://localhost:5072/api/players/Lina
```

## Databas
- Fil: `spelare.db`
- Tabell: `spelare`
- Kolumner:
  - `id` (PRIMARY KEY)
  - `namn`
  - `trojnummer`
  - `mal`
  - `matcher_spelade`

## Git och databasen
- `spelare.db` är en lokal fil och versioneras inte i Git.
- Filen ignoreras via `.gitignore` (`*.db`).
- Om filen saknas skapas den automatiskt när appen startar.

## Nya koncept i API-delen
- `Controller`: klass som tar emot HTTP-anrop.
- `Endpoint`: en specifik route, t.ex. `GET /api/players`.
- `Dependency Injection` (grundnivå): ASP.NET Core skapar beroenden åt oss via `builder.Services`.
- `HTTP-statuskoder`: t.ex. `200`, `201`, `204`, `400`, `404`.
