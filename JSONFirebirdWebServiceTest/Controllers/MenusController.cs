using JSONFirebirdWebServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("api")]
    public class MenusController : ApiController
    {
        

        [Route("Menus")]
        [HttpGet]
        // GET: api/Menus
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Menus/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Menus
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Menus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Menus/5
        public void Delete(int id)
        {
        }
    }
}
