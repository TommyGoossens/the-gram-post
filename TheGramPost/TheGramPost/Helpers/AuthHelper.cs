using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace TheGramPost.Helpers
{
    public class AuthHelper: IAuthHelper
    {
        private readonly HttpContextAccessor _context;

        public AuthHelper(IHttpContextAccessor context)
        {
            this._context = context as HttpContextAccessor;
        }


        public async Task<string> GetAuthToken()
        {
            try
            {
                return await _context.HttpContext.GetTokenAsync("access_token");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}