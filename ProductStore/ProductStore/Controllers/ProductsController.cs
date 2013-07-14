using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    public class ProductsController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        //GET
        // ../api/products/
        public IEnumerable<Product> GetAllProducts() //the Get in the following method names implies a GET request 
        {
            return repository.GetAll();
        }
        
        //GET
        // ../api/products/{id}
        public Product GetProduct(int id) //webapi automatically converts the query strind ID param to an int
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //GET
        // ../api/products?category={category}
        //webapi will try to match a query string key => value to a method parameter
        //in this case it is category
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        // POST
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item); //http 1.1 states that when a resource is created you should return with 201
            //CreateResponse automatically serializes the Product object into the body of an http header
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //PUT
        //browsers dont send put requests but you can send the programatically thru .NET or AJAX
        //alternatively you can tunnel the request thru a post
        public void PutProduct(int id, Product product) //the product will be deserialized from the request body
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //DELETE
        //browsers dont send delete requests but you can send them programatically thru .NET or AJAX
        //alternatively you can tunnel the request thru a post
        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
