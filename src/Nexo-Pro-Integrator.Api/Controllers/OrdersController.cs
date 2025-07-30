using System.Collections.Generic;
using System.Web.Http;
using Nexo_Pro_Integrator.Api.Models;

namespace Nexo_Pro_Integrator.Api.Controllers
{
    public class OrdersController : ApiController
    {
        // GET api/orders
        public IEnumerable<Order> Get()
        {
            var sfera = new Sfera();
            
            var products = sfera.GetOrders();
            
            return products;
        }
    }
}
