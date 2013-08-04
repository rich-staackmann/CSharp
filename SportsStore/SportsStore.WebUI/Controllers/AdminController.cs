using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    //authorize allows anyone who has authenticated to access this controller
    //for our simple app we haved hardcoded one user in so this is fine
    //obviously a real app wouldnt harcode users and would have different levels of access
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        //ActionResult is the parent class of viewResult, redirect, fileresult, etc
        //since we return either a view or a redirect, we can use ActionResult
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                //the viewbag CANNOT hold data for a redirect, the viewbag is emptied at the end of a single http request
                //tempdata will is restricted to a single user session, and persists until it is read
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        //we wired our saveProduct method to create a new product if the ID doesnt already exist
        public ViewResult Create()
        {
            //it is perfectly legal to return a view associated with another action
            //however, by default our form on the edit page will postback to the action
            //that rendered it. This is why we specify the action and controller in the BeginFor() method
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
