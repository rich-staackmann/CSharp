using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTrackr.Models;

namespace TimeTrackr.Helpers
{
    public static class ModelHelpers
    {
        //this will make sure that the a valid range of dates is specified, ie the end date isnt before the start date for a time span
        public static bool Validate_Time(DateTime start, DateTime? end)
        {
            if (!end.HasValue)
            {
                end = DateTime.Today;
            }
            return (end > start) ? false : true;
        }
    }
}