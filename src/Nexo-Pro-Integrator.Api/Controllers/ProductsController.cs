using System.Collections.Generic;
using System.Web.Http;
using Nexo_Pro_Integrator.Api.Models;

namespace Nexo_Pro_Integrator.Api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/products
        public IEnumerable<Product> Get()
        {
            var sfera = new Sfera();
            
            var products = sfera.GetProducts();
            
            return products;
        }
    }
}
