using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TotalWarUnits.Models;

namespace TotalWarUnits.Controllers
{
    public class UnitsController : Controller
    {
        private IQueryable<UnitModel> data = UnitModel.GetExcelData();
        
        //
        // GET: /Units/
        public ActionResult Index()
        {
            return View(data);
        }

        //
        // GET: /Units/Compare
        public ViewResult Compare(int order)
        {
            var unit = data.Where(u => u.Order == order);
            ViewBag.units = data.Where(u => u.Order != order);
            ViewBag.originalunit = order; //could also be tempData

            return View(unit);
        }
        [ActionName("FinalCompare")]
        public ViewResult Compare(int originalUnit, int order)
        {
            var units = data.Where(u => (u.Order == order) || (u.Order == originalUnit));

            return View("FinalCompare", units);
        }
        
    }
}
