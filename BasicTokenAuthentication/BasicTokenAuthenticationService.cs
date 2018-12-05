using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BasicTokenAuthentication
{
    public class BasicTokenAuthenticationService : IBasicTokenAuthenticationService
    {
        private IConfiguration Configuration;
        public BasicTokenAuthenticationService(IConfiguration configuration){
            Configuration = configuration;
        }
        public Task<bool> IsValidTokenAsync(string token)
        {
            if (token == Configuration["BasicToken"])
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }

}