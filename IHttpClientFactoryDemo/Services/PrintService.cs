
using Microsoft.Extensions.Logging;

namespace IHttpClientFactoryDemo.Services
{
    public class PrintService
    {
        public ILifetimeTransient lifetimeTransient {get;}
        public ILifetimeScope lifetimeScope {get;}
        public PrintService(ILifetimeTransient lifetimeTransient, ILifetimeScope lifetimeScope, ILogger<PrintService> logger){
            this.lifetimeTransient = lifetimeTransient;
            this.lifetimeScope = lifetimeScope;
            logger.LogDebug(lifetimeTransient.GetGuid() +"- lifetimeTransient inservice", null);
            logger.LogDebug(lifetimeScope.GetGuid() +"- lifetimeScope inservice", null);
        }
    }
}
