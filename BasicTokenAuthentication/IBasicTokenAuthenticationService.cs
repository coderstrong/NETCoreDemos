using System.Threading.Tasks;

namespace BasicTokenAuthentication
{
    public interface IBasicTokenAuthenticationService
    {
        Task<bool> IsValidTokenAsync(string token);
    }
}