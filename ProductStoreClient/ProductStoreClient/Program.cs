using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace ProductStoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup http client
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58288/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
       
            //list all products
            HttpResponseMessage response = client.GetAsync("api/products").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result; //ReadAsAsync can parse json, xml, url encoded strings, and even custom formats
                foreach (var p in products)
                {
                    Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.Price, p.Category);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();

            // Get a product by ID
            response = client.GetAsync("api/products/1").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var product = response.Content.ReadAsAsync<Product>().Result;
                Console.WriteLine("{0}\t{1};\t{2}", product.Name, product.Price, product.Category);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();

            // Create a new product
            var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
            Uri gizmoUri = null;

            response = client.PostAsJsonAsync("api/products", gizmo).Result; //postAsJson will automatically format our object into json
            if (response.IsSuccessStatusCode)
            {
                gizmoUri = response.Headers.Location;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Update a product
            gizmo.Price = 99.9;
            response = client.PutAsJsonAsync(gizmoUri.PathAndQuery, gizmo).Result;
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

            // Delete a product
            response = client.DeleteAsync(gizmoUri).Result;
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);


            Console.Read();
        }
    }
}
