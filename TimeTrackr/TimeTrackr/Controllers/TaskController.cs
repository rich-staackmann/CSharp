using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTrackr.Models;
using TimeTrackr.DAL;
using TimeTrackr.Helpers;

namespace TimeTrackr.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        //this is our EF context
        //private EFDbContext db = new EFDbContext();

        //we now use the repository pattern
        private ITaskRepository taskRepo;
        
        //we inject our repository here, using a ninject IoC container
        public TaskController(ITaskRepository taskParam)
        {
            this.taskRepo = taskParam;
        }

        //
        // GET: /Task/

        public ViewResult Index()
        {
            return View(taskRepo.Tasks.Where(p => p.UserProfile.UserName == User.Identity.Name).OrderByDescending(p => p.StartTime));
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = taskRepo.GetTaskByID(id);
            //we check if the timeInterval is null or if the interval belongs to another user
            if (task == null || task.UserProfile.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            if (ModelHelpers.Validate_Time(task.StartTime, task.EndTime))
            {
                ModelState.AddModelError("Error.", "The end time cannot be earlier than the start time.");
                return View();
            }
            if (ModelState.IsValid)
            {
                taskRepo.InsertTask(task, User.Identity.Name);
                taskRepo.Save();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = taskRepo.GetTaskByID(id);

            if (task == null || task.UserProfile.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid )
            {
                taskRepo.UpdateTask(task);
                taskRepo.Save();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = taskRepo.GetTaskByID(id);
            if (task == null || task.UserProfile.UserName != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = taskRepo.GetTaskByID(id);
            taskRepo.DeleteTask(id);
            taskRepo.Save();

            return RedirectToAction("Index");
        }

        //
        //GET: /Task/TasksByCategory
        //this is the function for inputting a category to search for tracked tasks

        [HttpGet]
        public ViewResult TasksByCategory()
        {
            //first we find a list of every task associated with the current user
            List<Task> tasks = new List<Task>(taskRepo.Tasks.Where(p => p.UserProfile.UserName == User.Identity.Name).ToList());
            List<string> categories = new List<string>();

            //we loop through all the tasks, and add the categories to a list
            foreach (Task m in tasks)
            {
                if (m.Category != null)
                {
                    categories.Add(m.Category);
                }
            }
            categories = categories.Distinct().ToList(); //make sure the list is distinct
            ViewBag.SelectList = new SelectList(categories); //this is the dropdown list our view will use
            return View();
        }

        //
        //POST: /Task/TasksByCategory
        //this returns a list of all the tracked tasks that match the posted category type

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult TasksByCategory(string category = null)
        {
            if(string.IsNullOrEmpty(category))
            {
                return View("TasksByCategory");
            }
            else
            {
                IEnumerable<Task> tasks = taskRepo.Tasks.Where(p => p.Category == category && p.UserProfile.UserName == User.Identity.Name);
                if (tasks == null)
                {
                    return View("TasksByCategory");
                }
                return View("DisplayTasksByCategory", tasks); 
            }
        }

        //
        //GET: /task/TasksByDate
        //this will find all task the were created in a specific time frame (ie two weeks)

        [HttpGet]
        public ViewResult TasksByDate()
        {
            return View();
        }

        //
        //POST: /task/tasksbydate
        //this wil display all the tasks that happen in a certain time frame

        [HttpPost]
        public ViewResult TasksByDate(DateTime StartDate, DateTime? EndDate = null)
        {
            if (!EndDate.HasValue) //if no end date, then set it to current date
            {
                EndDate = DateTime.Today;
            }

            if(ModelHelpers.Validate_Time(StartDate, EndDate))
            {
                ViewBag.errors = "The end time cannot be earlier than the start time";
                return View("TasksByDate");
            }
            IEnumerable<Task> tasks = taskRepo.Tasks.Where(p => p.StartTime >= StartDate && p.EndTime <= EndDate && p.UserProfile.UserName == User.Identity.Name);

            if (tasks == null)
            {
                return View("TasksByDate");
            }

            return View("DisplayTasksByDate", tasks);
        }
        
     
        //our method to free up resources and release db connections
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}