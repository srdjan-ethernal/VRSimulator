# Safety Sim

Profesionalni visejezicni sajt za VR simulacije industrijske bezbednosti.

Trenutni scenariji:

- VR simulacija za protivpozarnu zastitu
- VR simulacija za upravljanje radioaktivnim materijalima
- VR simulacija za upravljanje hemijskim otpadom
- VR simulacija za upravljanje gradjevinskim otpadom
- VR simulacija za upravljanje elektronskim otpadom
- VR simulacija za upravljanje biomedicinskim otpadom

Sajt je staticki i moze se objaviti preko GitHub Pages, Netlify, Cloudflare Pages ili bilo kog statickog hostinga.

Stranice:

- Pocetna: `index.html`
- Cene: `pricing.html`
- Sertifikati: `certificates.html`

## Backend

Backend je u folderu `src/VRSimulator.Api`.

Tehnologije:

- .NET 6
- ASP.NET Core Minimal API
- Microsoft runtime stack

Pokretanje:

```powershell
dotnet run --project src\VRSimulator.Api\VRSimulator.Api.csproj
```

Pocetne rute:

- `GET /api/health`
- `GET /api/scenarios`
- `GET /api/courses`
- `GET /api/workers`
- `POST /api/workers`
- `POST /api/enrollments`
- `POST /api/enrollments/{enrollmentId}/complete`
- `GET /api/certificates`

Prva verzija koristi in-memory skladiste. Sledeci korak je SQL Server + Entity Framework Core.
