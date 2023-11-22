using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TechPrimeLab.Models;
using TechPrimeLab.Repositories.Interfaces;
using TechPrimeLab.ViewModel;

namespace TechPrimeLab.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDetails>> RegisterUser(UserVM request)
        {
            var result = await _authService.RegisterUser(request.user_name, request);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
[HttpPost("ForgotPassword")]
        public async Task<ActionResult<UserDetails>> ForgotPassword(UserVM request)
        {
            var result = await _authService.ForgotPassword(request);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
[HttpPost("Login")]
        public async Task<ActionResult<UserDetails>>  LoginUser(UserVM request)
        {
            var result = await _authService.Login(request);
      

           if (result.Success==false)
{
    return Ok(result); 
}
else
{
    return BadRequest( result.ErrorMessage ); 
}
        }
    }
}
