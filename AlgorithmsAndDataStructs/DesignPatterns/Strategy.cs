using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Strategy
    {
    }
    //a made up model class to represent a shipping order
    public class Order
    {
        //blah blah blah, order details
    }
    //----------------------------------------------------------------------------------------
    //This class will use an algorithm to return the cost of shipping some order.
    //Before applying the strategy pattern, this class took an Order object in its ctor,
    //then a switch statement parsed the order and called a different Calculate() method
    //all of this happened inside of this one class....not good
    public class ShippingCostCalculatorService
    {
        readonly IShippingCostStrategy strategy;

        public ShippingCostCalculatorService(IShippingCostStrategy strat)
        {
            strategy = strat;
        }

        public double CalculateShippingCost(Order order)
        {
            return strategy.Calculate(order);
        }
    }
    //----------------------------------------------------------------------------------------
    //our strategy interface, we can have multiple concrete strategies
    public interface IShippingCostStrategy
    {
        double Calculate(Order order);
    }
    //----------------------------------------------------------------------------------------
    //Our concrete strategies
    //We can keep creating new strategies and we never need to modify the CostCalculator class
    public class FedExShippingStrategy : IShippingCostStrategy
    {
        //obviously this method would be more complicated
        //but for learning purposes it just returns a hardcoded number
        public double Calculate(Order order)
        {
            return 5.00d;
        }
    }

    public class UPSShippingStrategy : IShippingCostStrategy
    {
        public double Calculate(Order order)
        {
            return 3.00d;
        }
    }

    public class USPSShippingStrategy : IShippingCostStrategy
    {
        public double Calculate(Order order)
        {
            return 4.25d;
        }
    }
}
