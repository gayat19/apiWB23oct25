using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserLoginResponse> ValidateCredentials(UserLoginRequest user);
        Task<bool> Register(CustomerRegisterRequest customer);
    }
}
