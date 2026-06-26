using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;

namespace VRSimulator.Api.Services;

public interface ITrainingRepository
{
    IReadOnlyCollection<TrainingScenario> GetScenarios();

    IReadOnlyCollection<Course> GetCourses();

    IReadOnlyCollection<Worker> GetWorkers();

    Worker CreateWorker(CreateWorkerRequest request);

    IReadOnlyCollection<Enrollment> GetEnrollments();

    Result<Enrollment> CreateEnrollment(CreateEnrollmentRequest request);

    Result<EnrollmentCompletionResponse> CompleteEnrollment(Guid enrollmentId, CompleteTrainingRequest request);

    IReadOnlyCollection<Certificate> GetCertificates();

    IReadOnlyCollection<Certificate> GetCertificatesForWorker(Guid workerId);

    Certificate? GetCertificate(Guid certificateId);
}

