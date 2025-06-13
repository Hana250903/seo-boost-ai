using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOBoostAI.Repository.ModelExtensions;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthensController"/> with the specified authentication and current user services.
        /// </summary>
        public AuthensController(IAuthenService authenService, ICurrentUserService currentUserService)
        {
            _authenService = authenService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Authenticates a user using a Google credential and returns an access token.
        /// </summary>
        /// <param name="credential">The Google authentication credential received from the client.</param>
        /// <returns>An HTTP 200 response with the access token if authentication succeeds; otherwise, a 400 Bad Request with an error message.</returns>
        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            try
            {
                var result = await _authenService.LoginWithGoogle(credential);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Logs out the current user by invalidating the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be invalidated.</param>
        /// <returns>An HTTP 200 response with the logout result, or HTTP 400 if an error occurs.</returns>
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
                return BadRequest(ex);
            }

        }
    }
}
