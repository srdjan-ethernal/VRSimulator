using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;
using VRSimulator.Api.Persistence;
using VRSimulator.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TrainingDatabase")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=VRSimulatorTraining;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<TrainingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ITrainingRepository, EfTrainingRepository>();
builder.Services.AddScoped<IAuthService, EfAuthService>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "VR Simulator Training API",
    version = "0.1.0",
    documentation = "/api"
}));

app.MapGet("/api", () => Results.Ok(new
{
    name = "VR Simulator Training API",
    tenancy = "Company-scoped data is resolved from the Bearer token.",
    endpoints = new[]
    {
        "GET /api/health",
        "POST /api/auth/register",
        "POST /api/auth/login",
        "GET /api/auth/me",
        "GET /api/companies",
        "GET /api/scenarios",
        "GET /api/courses",
        "GET /api/workers",
        "POST /api/workers",
        "POST /api/enrollments",
        "GET /api/enrollments",
        "POST /api/enrollments/{enrollmentId}/complete",
        "GET /api/certificates",
        "GET /api/workers/{workerId}/certificates",
        "GET /api/certificates/{certificateId}"
    }
}));

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    timestampUtc = DateTimeOffset.UtcNow
}));

app.MapPost("/api/auth/register", (RegisterUserRequest request, IAuthService authService) =>
{
    var result = authService.Register(request);
    return result.Match(
        auth => Results.Created($"/api/users/{auth.User.Id}", auth),
        error => Results.BadRequest(new ProblemResponse(error)));
});

app.MapPost("/api/auth/login", (LoginRequest request, IAuthService authService) =>
{
    var result = authService.Login(request);
    return result.Match(
        auth => Results.Ok(auth),
        error => Results.BadRequest(new ProblemResponse(error)));
});

app.MapGet("/api/auth/me", (HttpRequest request, IAuthService authService) =>
{
    var token = GetBearerToken(request);
    if (string.IsNullOrWhiteSpace(token))
    {
        return Results.Unauthorized();
    }

    var result = authService.GetCurrentUser(token);
    return result.Match(
        user => Results.Ok(user),
        _ => Results.Unauthorized());
});

app.MapGet("/api/companies", (HttpRequest request, IAuthService authService) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user => authService.GetCompany(user.CompanyId).Match(
            company => Results.Ok(new[] { company }),
            error => Results.BadRequest(new ProblemResponse(error))),
        _ => Results.Unauthorized());
});

app.MapGet("/api/scenarios", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetScenarios());
});

app.MapGet("/api/courses", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetCourses());
});

app.MapGet("/api/workers", (HttpRequest request, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user => Results.Ok(repository.GetWorkers(user.CompanyId)),
        _ => Results.Unauthorized());
});

app.MapPost("/api/workers", (CreateWorkerRequest request, HttpRequest httpRequest, IAuthService authService, ITrainingRepository repository) =>
{
    if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
    {
        return Results.BadRequest(new ProblemResponse("Ime i prezime radnika su obavezni."));
    }

    if (string.IsNullOrWhiteSpace(request.EmployeeNumber))
    {
        return Results.BadRequest(new ProblemResponse("Broj zaposlenog je obavezan."));
    }

    var currentUser = ResolveCurrentUser(httpRequest, authService);
    return currentUser.Match(
        user =>
        {
            var result = repository.CreateWorker(user.CompanyId, request);
            return result.Match(
                worker => Results.Created($"/api/workers/{worker.Id}", worker),
                error => Results.BadRequest(new ProblemResponse(error)));
        },
        _ => Results.Unauthorized());
});

app.MapPost("/api/enrollments", (CreateEnrollmentRequest request, HttpRequest httpRequest, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(httpRequest, authService);
    return currentUser.Match(
        user =>
        {
            var result = repository.CreateEnrollment(user.CompanyId, request);
            return result.Match(
                enrollment => Results.Created($"/api/enrollments/{enrollment.Id}", enrollment),
                error => Results.BadRequest(new ProblemResponse(error)));
        },
        _ => Results.Unauthorized());
});

app.MapGet("/api/enrollments", (HttpRequest request, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user => Results.Ok(repository.GetEnrollments(user.CompanyId)),
        _ => Results.Unauthorized());
});

app.MapPost("/api/enrollments/{enrollmentId:guid}/complete", (
    Guid enrollmentId,
    CompleteTrainingRequest request,
    HttpRequest httpRequest,
    IAuthService authService,
    ITrainingRepository repository) =>
{
    if (request.Score < 0 || request.Score > 100)
    {
        return Results.BadRequest(new ProblemResponse("Rezultat mora biti izmedju 0 i 100."));
    }

    var currentUser = ResolveCurrentUser(httpRequest, authService);
    return currentUser.Match(
        user =>
        {
            var result = repository.CompleteEnrollment(user.CompanyId, enrollmentId, request);
            return result.Match(
                completion => Results.Ok(completion),
                error => Results.BadRequest(new ProblemResponse(error)));
        },
        _ => Results.Unauthorized());
});

app.MapGet("/api/certificates", (HttpRequest request, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user => Results.Ok(repository.GetCertificates(user.CompanyId)),
        _ => Results.Unauthorized());
});

app.MapGet("/api/workers/{workerId:guid}/certificates", (Guid workerId, HttpRequest request, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user => Results.Ok(repository.GetCertificatesForWorker(user.CompanyId, workerId)),
        _ => Results.Unauthorized());
});

app.MapGet("/api/certificates/{certificateId:guid}", (Guid certificateId, HttpRequest request, IAuthService authService, ITrainingRepository repository) =>
{
    var currentUser = ResolveCurrentUser(request, authService);
    return currentUser.Match(
        user =>
        {
            var certificate = repository.GetCertificate(user.CompanyId, certificateId);
            return certificate is null ? Results.NotFound() : Results.Ok(certificate);
        },
        _ => Results.Unauthorized());
});

app.Run();

static string? GetBearerToken(HttpRequest request)
{
    var authorization = request.Headers.Authorization.ToString();
    const string bearerPrefix = "Bearer ";

    if (authorization.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
    {
        return authorization[bearerPrefix.Length..].Trim();
    }

    return null;
}

static Result<UserProfileResponse> ResolveCurrentUser(HttpRequest request, IAuthService authService)
{
    var token = GetBearerToken(request);
    return string.IsNullOrWhiteSpace(token)
        ? Result<UserProfileResponse>.Failure("Token nije poslat.")
        : authService.GetCurrentUser(token);
}
