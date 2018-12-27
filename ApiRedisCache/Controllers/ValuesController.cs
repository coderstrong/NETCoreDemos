using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ApiRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(IDistributedCache distributedCache, ILogger<ValuesController> logger){
            this._distributedCache = distributedCache;
            this._logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var cachekey ="test";
            string data = _distributedCache.GetString(cachekey);

            var option = new DistributedCacheEntryOptions();
            option.SetSlidingExpiration(TimeSpan.FromSeconds(10));

            if(!string.IsNullOrWhiteSpace(data)){
                return "Fetched from cache : " + data;
            }
            else
            {
                data = "Redis ok";
                _distributedCache.SetString(cachekey, data, option);
                return "Added to cache : " + data;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
