namespace VRSimulator.Api.Domain;

public sealed record Company(
    Guid Id,
    string Name,
    DateTimeOffset CreatedAt);

