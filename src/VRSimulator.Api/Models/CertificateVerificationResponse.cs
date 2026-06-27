using VRSimulator.Api.Domain;

namespace VRSimulator.Api.Models;

public sealed record CertificateVerificationResponse(
    string CertificateNumber,
    string CourseTitleSr,
    string CourseTitleEn,
    DateTimeOffset IssuedAt,
    DateTimeOffset ValidUntil,
    CertificateStatus Status);
