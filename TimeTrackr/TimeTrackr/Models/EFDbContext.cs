using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TimeTrackr.Models
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TimeInterval> TimeIntervals { get; set; }
    }
}