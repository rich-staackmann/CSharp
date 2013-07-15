using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/ 
        public ActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name; //viewbag is a dynamic object that will just hold data to pass to our views
            ViewBag.NumTimes = numTimes;
            //strongly typed views are preferred to using the viewbag to pass data
            return View();
        }
    }
}
