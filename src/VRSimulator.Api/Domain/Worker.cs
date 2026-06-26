namespace VRSimulator.Api.Domain;

public sealed record Worker(
    Guid Id,
    string FirstName,
    string LastName,
    string EmployeeNumber,
    string Department,
    string Company,
    DateTimeOffset CreatedAt);

