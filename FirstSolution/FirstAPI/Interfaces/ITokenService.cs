using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(UserLoginResponse user);
    }
}
