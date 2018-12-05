using Microsoft.AspNetCore.Authentication;

namespace BasicTokenAuthentication
{
    public class BasicTokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm {get;set;}
    }
}
