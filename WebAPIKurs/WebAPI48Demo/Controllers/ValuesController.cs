using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI48Demo.Controllers
{
    public class ValuesController : ApiController
    {
        //Kernfrage -> Wie mache ich alte Implmenemtierungen (DbContext, Logging, Authorization-Schemas)
        //in den Controller


        // In ASP.NET Core -> IOC + Seperation of Concernce 

        // IOC Container in 4.8 -> Ninject, Autofac, StructureMap, Windsor-IOC 


        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
