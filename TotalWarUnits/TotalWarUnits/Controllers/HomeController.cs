using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using TotalWarUnits.Models;

namespace TotalWarUnits.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          /*  var fileName = string.Format("{0}\\Content\\Shogun2unitlist.xls", Directory.GetCurrentDirectory());
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [Melee Infantry$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "Melee Infantry");

            //DataTable data = ds.Tables["Melee Infantry"];
            var data = ds.Tables["Melee Infantry"].AsEnumerable();*/
            var data = UnitModel.GetExcelData();
            return View("UnitIndex", data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
