---
title: Safety Sim
colorFrom: green
colorTo: blue
sdk: docker
app_port: 7860
---

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
- Entity Framework Core 6
- SQL Server provider
- PostgreSQL provider za opcionu Neon demo varijantu
- Microsoft runtime stack

Pokretanje:

```powershell
dotnet run --project src\VRSimulator.Api\VRSimulator.Api.csproj
```

Pocetne rute:

- `GET /api/health`
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/me`
- `GET /api/users`
- `POST /api/users`
- `GET /api/companies`
- `GET /api/scenarios`
- `GET /api/courses`
- `GET /api/workers`
- `POST /api/workers`
- `POST /api/enrollments`
- `POST /api/enrollments/{enrollmentId}/complete`
- `GET /api/certificates`

Korisnik se registruje uz kompaniju. Radnici, upisi i sertifikati se citaju i menjaju samo u okviru kompanije ulogovanog korisnika preko Bearer tokena. Backend podrazumevano koristi SQL Server kroz Entity Framework Core, a za demo hosting podrzan je Azure SQL preko konfiguracije. Sledeci korak je ASP.NET Core Identity ili Microsoft Entra ID prema tipu korisnika.

Za lokalnu bazu instalirati SQL Server Express LocalDB ili podesiti `ConnectionStrings:TrainingDatabase` na postojeci SQL Server. Migracije su u `src/VRSimulator.Api/Persistence/Migrations`.

## Demo deployment: Hugging Face + Azure SQL

Repo sadrzi `Dockerfile` za Hugging Face Docker Space. Docker build objavljuje ASP.NET Core API i staticki frontend zajedno, na portu `7860`.

Za Azure SQL demo bazu podesiti Hugging Face Space secrets:

```text
ConnectionStrings__TrainingDatabase=Server=tcp:nswd.database.windows.net,1433;Initial Catalog=free-sql-db-3793918;Persist Security Info=False;User ID=CloudSA1ff17985;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
Database__Provider=SqlServer
Database__EnsureCreated=true
Cors__AllowAnyOrigin=true
```

`Database__EnsureCreated=true` je namenjen demo okruzenju: API pri startu kreira tabele i pocetni katalog kurseva ako baza jos nije inicijalizovana.

Frontend automatski koristi isti domen kao API kada je otvoren preko javnog HTTPS domena, a lokalno ostaje na `http://localhost:5222`.

Deployment tok:

1. Napraviti Azure SQL bazu.
2. Kopirati ADO.NET connection string u Hugging Face secret `ConnectionStrings__TrainingDatabase`.
3. Napraviti Hugging Face Space sa SDK tipom `Docker`.
4. Pushovati repo u Space.
5. Otvoriti Space URL i testirati registraciju kompanije, dodavanje radnika, dodelu obuke i izdavanje sertifikata.
