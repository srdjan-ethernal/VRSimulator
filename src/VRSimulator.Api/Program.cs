using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;
using VRSimulator.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITrainingRepository, InMemoryTrainingRepository>();
builder.Services.AddSingleton<IAuthService, InMemoryAuthService>();
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

app.MapGet("/api/companies", (IAuthService authService) =>
{
    return Results.Ok(authService.GetCompanies());
});

app.MapGet("/api/scenarios", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetScenarios());
});

app.MapGet("/api/courses", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetCourses());
});

app.MapGet("/api/workers", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetWorkers());
});

app.MapPost("/api/workers", (CreateWorkerRequest request, ITrainingRepository repository) =>
{
    if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
    {
        return Results.BadRequest(new ProblemResponse("Ime i prezime radnika su obavezni."));
    }

    var worker = repository.CreateWorker(request);
    return Results.Created($"/api/workers/{worker.Id}", worker);
});

app.MapPost("/api/enrollments", (CreateEnrollmentRequest request, ITrainingRepository repository) =>
{
    var result = repository.CreateEnrollment(request);
    return result.Match(
        enrollment => Results.Created($"/api/enrollments/{enrollment.Id}", enrollment),
        error => Results.BadRequest(new ProblemResponse(error)));
});

app.MapGet("/api/enrollments", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetEnrollments());
});

app.MapPost("/api/enrollments/{enrollmentId:guid}/complete", (
    Guid enrollmentId,
    CompleteTrainingRequest request,
    ITrainingRepository repository) =>
{
    if (request.Score < 0 || request.Score > 100)
    {
        return Results.BadRequest(new ProblemResponse("Rezultat mora biti izmedju 0 i 100."));
    }

    var result = repository.CompleteEnrollment(enrollmentId, request);
    return result.Match(
        completion => Results.Ok(completion),
        error => Results.BadRequest(new ProblemResponse(error)));
});

app.MapGet("/api/certificates", (ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetCertificates());
});

app.MapGet("/api/workers/{workerId:guid}/certificates", (Guid workerId, ITrainingRepository repository) =>
{
    return Results.Ok(repository.GetCertificatesForWorker(workerId));
});

app.MapGet("/api/certificates/{certificateId:guid}", (Guid certificateId, ITrainingRepository repository) =>
{
    var certificate = repository.GetCertificate(certificateId);
    return certificate is null ? Results.NotFound() : Results.Ok(certificate);
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
