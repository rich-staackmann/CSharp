using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            //decorator pattern
            Pizza largePizza = new LargePizza(); //the decorator inherits from pizza, so we declare this largePizza as pizza
                  largePizza = new Cheese(largePizza);
                  largePizza = new Ham(largePizza);
                  largePizza = new Peppers(largePizza);

            Console.WriteLine(largePizza.GetDescription());
            Console.WriteLine("{0:C2}", largePizza.CalculateCost());
            Console.WriteLine();

            //bridge pattern
            List<IManuscript> documents = new List<IManuscript>();
            var formatter = new BackwordsFormatter();
            var fancyFormatter = new FancyFormatter();
            var faq = new FAQ(fancyFormatter);
                faq.Title = "The bridge pattern faq";
                faq.Questions.Add("What is it?", "A design patter");
                faq.Questions.Add("When do we use it?", "decoupling");
            var book = new Book(formatter);
                book.Title = "Lots of patterns";
                book.Author = "Joe";
                book.Text = "Blah blah blah...";
            var paper = new TermPaper(formatter);
                paper.Class = "Design Patterns";
                paper.Student = "Joe Noob";
                paper.Text = "Blah blah blah ...";
                paper.References = "GOF";
            
            documents.Add(faq);
            documents.Add(book);
            documents.Add(paper);
            foreach (var doc in documents)
                doc.Print();
            Console.WriteLine();

            //facade pattern
            Facade tempFacade = new Facade();
            var localTemp = tempFacade.GetTemperature(60047);
            Console.WriteLine("The current temp is {0}F/{1}C in {2}, {3}", 
                localTemp.Fahrenheit.ToString(), 
                localTemp.Celsius.ToString(), 
                localTemp.City, 
                localTemp.State);
            Console.WriteLine();

            //Factory pattern
            Factory factory = LoadFactory();
            IAuto car = factory.CreateAutomobile();
            car.TurnOn();
            car.TurnOff();
            Console.WriteLine();

            //Null Object pattern
            IAuto nullCar = new NullAuto();
            nullCar.TurnOn();
            nullCar.TurnOff();
            Console.WriteLine();

            //Singleton pattern
            Singleton.Instance().DoSomething();
            var obj = Singleton.Instance();
            obj.DoSomething();

            //Service Locator Pattern
            ServiceLocator.AddService<ILog>("logger", new LoggingService());
            ILog log = ServiceLocator.GetService<ILog>("logger");
            log.Log("This is a logging test string!");
            Console.WriteLine();

            Console.ReadLine(); //leave console open
        }
        
        //this method uses reflection to create a factory
        //we have hard coded the factory that we want in the method
        //often the factory is supplied by an app settings file
        static Factory LoadFactory()
        {
            string factoryName = "DesignPatterns.AudiTTSFactory";
            return Assembly.GetExecutingAssembly().CreateInstance(factoryName) as Factory;
        }
    }
}
