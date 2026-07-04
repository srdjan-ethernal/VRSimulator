using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Models;

public sealed record CreateCompanyUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    UserRole Role);

