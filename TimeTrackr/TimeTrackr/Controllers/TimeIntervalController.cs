using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTrackr.Models;

namespace TimeTrackr.Controllers
{
    [Authorize]
    public class TimeIntervalController : Controller
    {
        private EFDbContext db = new EFDbContext();
        //
        // GET: /TimeInterval/

        public ActionResult Index()
        {
            return View(db.TimeIntervals.Where(p => p.IntervalOwner.UserName == User.Identity.Name));
        }

        //
        // GET: /TimeInterval/Details/5

        public ActionResult Details(int id = 0)
        {
            TimeInterval timeinterval = db.TimeIntervals.Find(id);
            //we check if the timeInterval is null or if the interval belongs to another user
            if (timeinterval == null || timeinterval.IntervalOwner.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(timeinterval);
        }

        //
        // GET: /TimeInterval/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeInterval/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeInterval timeinterval)
        {
            timeinterval.IntervalOwner = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).First();

            if (ModelState.IsValid)
            {
                db.TimeIntervals.Add(timeinterval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeinterval);
        }

        //
        // GET: /TimeInterval/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeInterval timeinterval = db.TimeIntervals.Find(id);

            if (timeinterval == null || timeinterval.IntervalOwner.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(timeinterval);
        }

        //
        // POST: /TimeInterval/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TimeInterval timeinterval)
        {
            if (ModelState.IsValid )
            {
                db.Entry(timeinterval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeinterval);
        }

        //
        // GET: /TimeInterval/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeInterval timeinterval = db.TimeIntervals.Find(id);
            if (timeinterval == null || timeinterval.IntervalOwner.UserName != User.Identity.Name )
            {
                return HttpNotFound();
            }
            return View(timeinterval);
        }

        //
        // POST: /TimeInterval/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeInterval timeinterval = db.TimeIntervals.Find(id);
            db.TimeIntervals.Remove(timeinterval);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("TasksByCategory")]
        public ActionResult GetTasksByCategory()
        {
            return View();
        }

        [HttpPost, ActionName("TasksByCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult GetTasksByCategory(string category)
        {
            IEnumerable<TimeInterval> intervals = db.TimeIntervals.Where(p => p.Category == category && p.IntervalOwner.UserName == User.Identity.Name);
            if (intervals == null)
            {
                return View();
            }
            return View("DisplayTasksByCategory", intervals);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}