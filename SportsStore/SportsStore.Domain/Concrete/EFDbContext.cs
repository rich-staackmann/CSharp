using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    //this is our Entity Framework code first db context
    //the context is used to define tables in our database based on classes in the app
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
