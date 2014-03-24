using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //base component class
    public abstract class Pizza
    {
        public string Description { get; set; }
        public abstract string GetDescription();
        public abstract double CalculateCost();
    }

    //following 3 classes are concrete implementation of Pizza
    public class SmallPizza : Pizza
    {
        public SmallPizza() { Description = "Small Pizza"; }
        public override string GetDescription() { return Description; }
        public override double CalculateCost() { return 3.00; }
    }
    public class MediumPizza : Pizza
    {
        public MediumPizza() { Description = "Medium Pizza"; }
        public override string GetDescription() { return Description; }
        public override double CalculateCost() { return 6.00; }
    }
    public class LargePizza : Pizza
    {
        public LargePizza() { Description = "Large Pizza"; }
        public override string GetDescription() { return Description; }
        public override double CalculateCost() { return 9.00; }
    }
    //----------------------------------------------------------------------------------------
    //decorator base class
    public class Decorator : Pizza
    {
        protected Pizza _pizza; //the object we wrap, that will be decorated

        public Decorator(Pizza p) { _pizza = p; }

        public override string GetDescription()
        {
            return _pizza.Description;
        }

        public override double CalculateCost()
        {
            return _pizza.CalculateCost();
        }
    }
    //----------------------------------------------------------------------------------------
    //following classes are the concrete decorators
    public class Cheese : Decorator
    {
        public Cheese(Pizza p) : base(p) { Description = "Cheese"; }

        public override double CalculateCost()
        {
            return _pizza.CalculateCost() + 1.25;
        }

        public override string GetDescription()
        {
            return String.Format("{0}, {1}", _pizza.GetDescription(), this.Description);
        }
    }

    public class Ham : Decorator
    {
        public Ham(Pizza p) : base(p) { Description = "Ham"; }

        public override double CalculateCost()
        {
            return _pizza.CalculateCost() + 1.00;
        }

        public override string GetDescription()
        {
            return String.Format("{0}, {1}", _pizza.GetDescription(), this.Description);
        }
    }

    public class Peppers : Decorator
    {
        public Peppers(Pizza p) : base(p) { Description = "Peppers"; }

        public override double CalculateCost()
        {
            return _pizza.CalculateCost() + 2.00;
        }

        public override string GetDescription()
        {
            return String.Format("{0}, {1}", _pizza.GetDescription(), this.Description);
        }
    }
}
