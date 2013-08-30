using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TimeTrackr.Models;

namespace TimeTrackr.DAL
{   
    //we are following the repository pattern in the app
    //this will make it easier to unit test
    public class TaskRepository : ITaskRepository
    {
        //normally this is private, but it is a workaround for the fact that my userprofile model doesnt also have a repository
        //i dont know enough about how MVC implements its account controller, so i dont want to mess that up by making a userprofile repository
        //so, in order to access that table's data, this must be public
        private EFDbContext context = new EFDbContext();

        public EFDbContext GetContext()
        {
            return context;
        }

        public IQueryable<Task> Tasks
        {
	        get { return context.Tasks; }
        }

        public Task GetTaskByID(int taskId)
        {
            return context.Tasks.Find(taskId);
        }

        public void InsertTask(Task task, string userName)
        {
            task.UserProfile = context.UserProfiles.Where(u => u.UserName == userName).First();
 	        context.Tasks.Add(task);
        }

        public void DeleteTask(int taskID)
        {
            Task t = context.Tasks.Find(taskID);
 	        context.Tasks.Remove(t);
        }

        public void UpdateTask(Task Task)
        {
            //if the task doesn't exist, we will create it
            if (Task.ID == 0)
            {
                context.Tasks.Add(Task);
            }
            else
            {
                Task dbEntry = context.Tasks.Find(Task.ID);
                if (dbEntry != null)
                {
                    dbEntry.StartTime = Task.StartTime;
                    dbEntry.EndTime = Task.EndTime;
                    dbEntry.Category = Task.Category;
                    dbEntry.Description = Task.Description;
                }
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}