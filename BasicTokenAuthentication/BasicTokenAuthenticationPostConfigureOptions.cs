
using System;
using Microsoft.Extensions.Options;

namespace BasicTokenAuthentication
{
    public class BasicTokenAuthenticationPostConfigureOptions : IPostConfigureOptions<BasicTokenAuthenticationOptions>
    {
        public void PostConfigure(string name, BasicTokenAuthenticationOptions options)
        {
            if(string.IsNullOrEmpty(options.Realm)){
                throw new InvalidOperationException("Realm must be provided in options");
            }
        }
    }
}
