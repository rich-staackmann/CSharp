using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace TimeTrackr.Helpers
{
    public static class HtmlHelpers
    {
        public static TimeSpan GetElapsedTime(DateTime start, DateTime end)
        {
            return end.Subtract(start);
        }
    }
}