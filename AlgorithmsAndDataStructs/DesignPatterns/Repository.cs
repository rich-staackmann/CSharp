using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //a random model we are working with
    public class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
    //----------------------------------------------------------------------------------------
    //our Repository interface, we can have different types of concrete implementations
    //our repository should be a template, but to keep it simple it will just work with our model
    public interface IRepository
    {
        void Add(Employee e);
        void Remove(Employee e);
        IEnumerable<Employee> Find(Func<Employee, bool> predicate);
    }

    //concrete repository
    public class Repository : IRepository
    {
        //here, we are storing data in memory
        private LinkedList<Employee> list;

        public Repository()
        {
            list = new LinkedList<Employee>();
        }
        public void Add(Employee e)
        {
            list.AddFirst(e);
        }

        public void Remove(Employee e)
        {
            list.Remove(e);
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            IEnumerable<Employee> temp = list.Where(predicate);
            return temp.ToList();
        }
    }
}
