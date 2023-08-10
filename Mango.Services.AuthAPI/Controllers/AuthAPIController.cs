using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDTO _response;
        public AuthAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
                var errorMessage = await _authService.Register(model);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _response.IsScucess = false;
                    _response.Message = errorMessage;
                    return BadRequest(_response);
                }
                return Ok(_response);          
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _response.IsScucess = false;
                _response.Message = "UserName or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> Login([FromBody] RegistrationRequestDTO model)
        {
            var assignRoleSuccessfull = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessfull)
            {
                _response.IsScucess = false;
                _response.Message = "Error Encountered while Assigning roles";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
