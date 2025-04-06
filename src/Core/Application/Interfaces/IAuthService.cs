using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<AuthResponse?>> LoginAsync(LoginRequest model);
    }
}
