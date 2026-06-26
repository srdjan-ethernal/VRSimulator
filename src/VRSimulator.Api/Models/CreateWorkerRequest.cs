namespace VRSimulator.Api.Models;

public sealed record CreateWorkerRequest(
    string FirstName,
    string LastName,
    string EmployeeNumber,
    string Department,
    string Company);

