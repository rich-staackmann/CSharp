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
        //this is our EF context
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

        //
        //GET: /TimeInterval/TasksByCategory
        //this is the function for inputting a category to search for tracked tasks

        [HttpGet, ActionName("TasksByCategory")]
        public ActionResult GetTasksByCategory()
        {
            List<TimeInterval> intervals = new List<TimeInterval>(db.TimeIntervals.Where(p => p.IntervalOwner.UserName == User.Identity.Name).ToList());
            List<string> categories = new List<string>();

            foreach (TimeInterval m in intervals)
            {
                if (m.Category != null)
                {
                    categories.Add(m.Category);
                }
            }
            categories = categories.Distinct().ToList();
            ViewBag.SelectList = new SelectList(categories);
            return View();
        }

        //
        //POST: /TimeInterval/TasksByCategory
        //this return list of all the tracked tasks that match the posted category type

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

        //
        //GET: /Timeinterval/TasksByDate
        //this will find all task the were created in a specific time frame (ie two weeks)

        [HttpGet, ActionName("TasksByDate")]
        public ActionResult GetTasksByDate()
        {
            return View();
        }

        //
        //POST: /timeitnerval/tasksbydate
        //this wil display all the tasks that happen in a certain time frame

        [HttpPost, ActionName("TasksByDate")]
        public ActionResult GetTasksByDate(DateTime StartDate, DateTime? EndDate = null)
        {
            if (!EndDate.HasValue)
            {
                EndDate = DateTime.Today;
            }

            IEnumerable<TimeInterval> intervals = db.TimeIntervals.Where(p => p.StartTime.CompareTo(StartDate) >= 0 && p.IntervalOwner.UserName == User.Identity.Name);

            if (intervals == null)
            {
                return View();
            }

            return View("DisplayTasksByDate", intervals);
        }


        //our method to free up resources and release db connections
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}