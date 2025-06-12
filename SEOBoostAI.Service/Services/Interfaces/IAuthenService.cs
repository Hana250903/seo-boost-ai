using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOBoostAI.Service.Services.Interfaces
{
    public interface IAuthenService
    {
        public Task<string> LoginWithGoogle(string credential);
        public Task<bool> LogOut(string refreshToken, int userId);
    }
}
