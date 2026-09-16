# Del2databasboll

En enkel C#-konsolapp för spelare med SQLite-databas.

## Ingår
- Skapar databasen `spelare.db` automatiskt.
- Skapar tabellen `spelare` vid första start.
- CRUD-grund via meny:
  - Lägg till spelare
  - Visa alla spelare
  - Sök spelare
  - Ta bort spelare

## Kör
```bash
dotnet restore
dotnet run --project Del2databasboll.csproj
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
