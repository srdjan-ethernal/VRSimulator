namespace VRSimulator.Api.Models;

public sealed record CompanyResponse(
    Guid Id,
    string Name,
    DateTimeOffset CreatedAt);

