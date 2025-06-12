using SEOBoostAI.Repository.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IAuthenService
    {
        /// <summary>
        /// Authenticates a user using a Google credential and returns an authentication token or session identifier.
        /// </summary>
        /// <param name="credential">The Google credential string obtained from the client.</param>
        /// <summary>
/// Authenticates a user using a Google credential and returns the authentication result.
/// </summary>
/// <param name="credential">The Google credential string to authenticate with.</param>
/// <returns>A task that resolves to a <see cref="ResultModel"/> containing authentication details.</returns>
        public Task<ResultModel> LoginWithGoogle(string credential);
        /// <summary>
        /// Asynchronously logs out a user by invalidating the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be invalidated.</param>
        /// <param name="userId">The ID of the user to log out.</param>
        /// <summary>
/// Asynchronously logs out a user by invalidating the specified refresh token.
/// </summary>
/// <param name="refreshToken">The refresh token to be invalidated.</param>
/// <param name="userId">The ID of the user to log out.</param>
/// <returns>True if the logout operation succeeds; otherwise, false.</returns>
        public Task<bool> LogOut(string refreshToken, int userId);
    }
}
