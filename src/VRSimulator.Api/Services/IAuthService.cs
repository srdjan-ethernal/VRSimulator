using VRSimulator.Api.Models;

namespace VRSimulator.Api.Services;

public interface IAuthService
{
    Result<AuthResponse> Register(RegisterUserRequest request);

    Result<AuthResponse> Login(LoginRequest request);

    Result<UserProfileResponse> GetCurrentUser(string accessToken);

    IReadOnlyCollection<CompanyResponse> GetCompanies();

    Result<CompanyResponse> GetCompany(Guid companyId);
}
