namespace VRSimulator.Api.Models;

public sealed record LoginRequest(
    string Email,
    string Password);

