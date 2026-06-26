using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Models;

public sealed record UserProfileResponse(
    Guid Id,
    Guid CompanyId,
    string CompanyName,
    string Email,
    string FirstName,
    string LastName,
    UserRole Role,
    DateTimeOffset CreatedAt);

