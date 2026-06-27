using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Models;

public sealed record WorkerPortalResponse(
    Worker Worker,
    IReadOnlyCollection<Course> Courses,
    IReadOnlyCollection<Enrollment> Enrollments,
    IReadOnlyCollection<Certificate> Certificates);
