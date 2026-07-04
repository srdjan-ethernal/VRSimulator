namespace VRSimulator.Api.Domain;

public sealed record Enrollment(
    Guid Id,
    Guid WorkerId,
    Guid CourseId,
    EnrollmentStatus Status,
    DateTimeOffset EnrolledAt,
    DateTimeOffset? CompletedAt,
    int? Score,
    int? DurationMinutes);

