using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
    }
}
