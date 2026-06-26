# VRSimulator.Api

ASP.NET Core backend za evidenciju VR obuka radnika.

## Tehnologije

- .NET 6
- ASP.NET Core Minimal API
- Microsoft hosting/runtime stack
- Privremeno in-memory skladiste za razvoj

Planirana produkciona baza: SQL Server uz Entity Framework Core.

## Multi-tenant pravilo

Korisnik uvek pripada jednoj kompaniji.

Podaci koji su vezani za kompaniju:

- radnici
- upisi na kurseve
- rezultati obuke
- sertifikati

Ove rute zahtevaju `Authorization: Bearer <accessToken>` i automatski koriste `companyId` iz tokena. Klijent ne salje `companyId` u zahtevima za radnike ili upise.

Globalni podaci:

- scenariji
- kursevi

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

1. `POST /api/auth/register`
2. `POST /api/auth/login`
3. `GET /api/auth/me`
4. `GET /api/courses`
5. `POST /api/workers`
6. `POST /api/enrollments`
7. `POST /api/enrollments/{enrollmentId}/complete`
8. `GET /api/certificates`

Ako je rezultat kursa najmanje 80, backend automatski izdaje sertifikat koji vazi 12 meseci.

## Auth rute

### Registracija

`POST /api/auth/register`

```json
{
  "email": "admin@safetysim.test",
  "password": "TestPass123",
  "firstName": "Srdjan",
  "lastName": "Admin",
  "companyName": "Safety Sim Demo"
}
```

Registracija kreira korisnika i kompaniju ako kompanija jos ne postoji. Prvi korisnik dobija ulogu `CompanyAdmin`.

### Login

`POST /api/auth/login`

```json
{
  "email": "admin@safetysim.test",
  "password": "TestPass123"
}
```

Odgovor sadrzi privremeni `accessToken`. Za proveru profila koristi se:

```text
Authorization: Bearer <accessToken>
```

`GET /api/auth/me`

### Kreiranje radnika u tenant-u

`POST /api/workers`

Header:

```text
Authorization: Bearer <accessToken>
```

Body:

```json
{
  "firstName": "Pera",
  "lastName": "Peric",
  "employeeNumber": "A-001",
  "department": "Bezbednost"
}
```

Kompanija radnika se uzima iz tokena ulogovanog korisnika.
