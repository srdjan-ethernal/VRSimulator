using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;

namespace VRSimulator.Api.Services;

public sealed class InMemoryTrainingRepository : ITrainingRepository
{
    private readonly object _lock = new();
    private readonly List<TrainingScenario> _scenarios;
    private readonly List<Course> _courses;
    private readonly List<Worker> _workers = new();
    private readonly List<Enrollment> _enrollments = new();
    private readonly List<Certificate> _certificates = new();

    public InMemoryTrainingRepository()
    {
        _scenarios = SeedData.CreateScenarios();
        _courses = SeedData.CreateCourses(_scenarios);
        SeedDemoWorker();
    }

    public IReadOnlyCollection<TrainingScenario> GetScenarios() => _scenarios;

    public IReadOnlyCollection<Course> GetCourses() => _courses;

    public IReadOnlyCollection<Worker> GetWorkers()
    {
        lock (_lock)
        {
            return _workers.ToList();
        }
    }

    public Worker CreateWorker(CreateWorkerRequest request)
    {
        var worker = new Worker(
            Guid.NewGuid(),
            request.FirstName.Trim(),
            request.LastName.Trim(),
            request.EmployeeNumber.Trim(),
            request.Department.Trim(),
            request.Company.Trim(),
            DateTimeOffset.UtcNow);

        lock (_lock)
        {
            _workers.Add(worker);
        }

        return worker;
    }

    public IReadOnlyCollection<Enrollment> GetEnrollments()
    {
        lock (_lock)
        {
            return _enrollments.ToList();
        }
    }

    public Result<Enrollment> CreateEnrollment(CreateEnrollmentRequest request)
    {
        lock (_lock)
        {
            if (_workers.All(worker => worker.Id != request.WorkerId))
            {
                return Result<Enrollment>.Failure("Radnik nije pronadjen.");
            }

            if (_courses.All(course => course.Id != request.CourseId))
            {
                return Result<Enrollment>.Failure("Kurs nije pronadjen.");
            }

            var existingOpenEnrollment = _enrollments.Any(enrollment =>
                enrollment.WorkerId == request.WorkerId &&
                enrollment.CourseId == request.CourseId &&
                enrollment.Status is EnrollmentStatus.Enrolled or EnrollmentStatus.InProgress);

            if (existingOpenEnrollment)
            {
                return Result<Enrollment>.Failure("Radnik vec ima aktivan upis na ovaj kurs.");
            }

            var enrollment = new Enrollment(
                Guid.NewGuid(),
                request.WorkerId,
                request.CourseId,
                EnrollmentStatus.Enrolled,
                DateTimeOffset.UtcNow,
                null,
                null,
                null);

            _enrollments.Add(enrollment);
            return Result<Enrollment>.Success(enrollment);
        }
    }

    public Result<EnrollmentCompletionResponse> CompleteEnrollment(Guid enrollmentId, CompleteTrainingRequest request)
    {
        lock (_lock)
        {
            var enrollmentIndex = _enrollments.FindIndex(enrollment => enrollment.Id == enrollmentId);
            if (enrollmentIndex < 0)
            {
                return Result<EnrollmentCompletionResponse>.Failure("Upis na kurs nije pronadjen.");
            }

            var enrollment = _enrollments[enrollmentIndex];
            var course = _courses.Single(course => course.Id == enrollment.CourseId);
            var passed = request.Score >= course.PassScore;

            var completedEnrollment = enrollment with
            {
                Status = passed ? EnrollmentStatus.Passed : EnrollmentStatus.Failed,
                CompletedAt = DateTimeOffset.UtcNow,
                Score = request.Score,
                DurationMinutes = request.DurationMinutes
            };

            _enrollments[enrollmentIndex] = completedEnrollment;

            Certificate? certificate = null;
            if (passed)
            {
                certificate = new Certificate(
                    Guid.NewGuid(),
                    CreateCertificateNumber(course.Code),
                    enrollment.WorkerId,
                    enrollment.CourseId,
                    DateTimeOffset.UtcNow,
                    DateTimeOffset.UtcNow.AddMonths(course.ValidityMonths),
                    CertificateStatus.Active);

                _certificates.Add(certificate);
            }

            return Result<EnrollmentCompletionResponse>.Success(new EnrollmentCompletionResponse(
                completedEnrollment,
                certificate));
        }
    }

    public IReadOnlyCollection<Certificate> GetCertificates()
    {
        lock (_lock)
        {
            return _certificates.ToList();
        }
    }

    public IReadOnlyCollection<Certificate> GetCertificatesForWorker(Guid workerId)
    {
        lock (_lock)
        {
            return _certificates
                .Where(certificate => certificate.WorkerId == workerId)
                .ToList();
        }
    }

    public Certificate? GetCertificate(Guid certificateId)
    {
        lock (_lock)
        {
            return _certificates.SingleOrDefault(certificate => certificate.Id == certificateId);
        }
    }

    private void SeedDemoWorker()
    {
        var worker = new Worker(
            Guid.Parse("1a77fa98-3c51-45cb-86e7-092d20d631e9"),
            "Pera",
            "Peric",
            "EMP-001",
            "Bezbednost",
            "Demo Kompanija",
            DateTimeOffset.UtcNow);

        _workers.Add(worker);
    }

    private static string CreateCertificateNumber(string courseCode)
    {
        return $"SS-{courseCode.ToUpperInvariant()}-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}";
    }
}

