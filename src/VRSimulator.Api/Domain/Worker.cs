namespace VRSimulator.Api.Domain;

public sealed record Worker(
    Guid Id,
    Guid CompanyId,
    string FirstName,
    string LastName,
    string EmployeeNumber,
    string Department,
    DateTimeOffset CreatedAt);

