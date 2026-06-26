# VRSimulator.Api

ASP.NET Core backend za evidenciju VR obuka radnika.

## Tehnologije

- .NET 6
- ASP.NET Core Minimal API
- Microsoft hosting/runtime stack
- Privremeno in-memory skladiste za razvoj

Planirana produkciona baza: SQL Server uz Entity Framework Core.

## Pokretanje

```powershell
dotnet run --project src\VRSimulator.Api\VRSimulator.Api.csproj
```

Zatim otvoriti:

```text
http://localhost:5000/api
```

Port moze biti drugaciji ako ga Visual Studio ili `launchSettings.json` dodele automatski.

## Prvi API tok

1. `GET /api/courses`
2. `POST /api/workers`
3. `POST /api/enrollments`
4. `POST /api/enrollments/{enrollmentId}/complete`
5. `GET /api/certificates`

Ako je rezultat kursa najmanje 80, backend automatski izdaje sertifikat koji vazi 12 meseci.
