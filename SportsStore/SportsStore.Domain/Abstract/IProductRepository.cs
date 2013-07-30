using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    //in order to keep our domain model separate from our persistence logic,
    //we define the repository pattern
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
