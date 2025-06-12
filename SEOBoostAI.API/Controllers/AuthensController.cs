using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Service.Services;
using SEOBoostAI.Service.Services.Interfaces;
using SEOBoostAI.Service.Ultils;

namespace SEOBoostAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthensController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        private readonly ICurrentUserService _currentUserService;

        public AuthensController(IAuthenService authenService, ICurrentUserService currentUserService)
        {
            _authenService = authenService;
            _currentUserService = currentUserService;
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            try
            {
                var accessToken = await _authenService.LoginWithGoogle(credential);
                return Ok(new { AccessToken = accessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> LogOut(string refreshToken)
        {
            try
            {
                var user = _currentUserService.GetUserId();

                var result = await _authenService.LogOut(refreshToken, user);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
