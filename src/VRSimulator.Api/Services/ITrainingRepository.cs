using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;

namespace VRSimulator.Api.Services;

public interface ITrainingRepository
{
    IReadOnlyCollection<TrainingScenario> GetScenarios();

    IReadOnlyCollection<Course> GetCourses();

    IReadOnlyCollection<Worker> GetWorkers(Guid companyId);

    Worker CreateWorker(Guid companyId, CreateWorkerRequest request);

    IReadOnlyCollection<Enrollment> GetEnrollments(Guid companyId);

    Result<Enrollment> CreateEnrollment(Guid companyId, CreateEnrollmentRequest request);

    Result<EnrollmentCompletionResponse> CompleteEnrollment(Guid companyId, Guid enrollmentId, CompleteTrainingRequest request);

    IReadOnlyCollection<Certificate> GetCertificates(Guid companyId);

    IReadOnlyCollection<Certificate> GetCertificatesForWorker(Guid companyId, Guid workerId);

    Certificate? GetCertificate(Guid companyId, Guid certificateId);
}

