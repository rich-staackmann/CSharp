using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using TimeTrackr.Models;

namespace TimeTrackr.Helpers
{
    public static class HtmlHelpers
    {
        //this helper is to find the time that elapsed for a specific task
        public static TimeSpan GetElapsedTime(DateTime start, DateTime end)
        {
            return end.Subtract(start);
        }

        //this helper will find the total elaspsed time for a collection of tasks
        public static TimeSpan GetElapsedTimeForCollection(IEnumerable<Task> tasks)
        {
            TimeSpan sum = new TimeSpan(0,0,0);
            foreach (Task task in tasks)
            {
                //System.Diagnostics.Debug.WriteLine(task.ID);
                sum = sum.Add(task.EndTime.Subtract(task.StartTime)); //timespan is a value type, so you must assign the return value of the ADD function to a variable
                //System.Diagnostics.Debug.WriteLine(sum);
            }

            return sum;
        }

    }
}