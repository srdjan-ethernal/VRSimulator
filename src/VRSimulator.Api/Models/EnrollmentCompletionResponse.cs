using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Models;

public sealed record EnrollmentCompletionResponse(
    Enrollment Enrollment,
    Certificate? Certificate);

