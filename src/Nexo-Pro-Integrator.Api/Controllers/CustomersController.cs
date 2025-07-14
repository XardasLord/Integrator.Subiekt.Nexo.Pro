using System.Collections.Generic;
using System.Web.Http;
using Nexo_Pro_Integrator.Api.Models;

namespace Nexo_Pro_Integrator.Api.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/customers
        public IEnumerable<string> Get()
        {
            var sfera = new Sfera();
            
            var customers = sfera.GetCustomers();
            
            return customers;
        }
    }
}
