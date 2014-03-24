using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//                                                                          \\
//                                                                          \\
// The Command pattern file also has an example for factory built objects    \\        
// LoadFactory() method is important, located in main() function            \\
//                                                                          \\
namespace DesignPatterns
{
    //here is our interface for our objects
    public interface IAuto
    {
        string Name { get; }
        void TurnOff();
        void TurnOn();
    }
    //----------------------------------------------------------------------------------------
    //The following classes are the concrete implementations
    public class BMW335Xi : IAuto
    {
        public string Name
        {
            get { return "BMW335Xi"; }
        }

        public void TurnOff()
        {
            Console.WriteLine("The {0}'s engine is off", Name);
        }

        public void TurnOn()
        {
            Console.WriteLine("The {0}'s engine is on", Name);
        }
    }
    public class AudiTTS : IAuto
    {
        public string Name
        {
            get { return "AudiTTS"; }
        }

        public void TurnOff()
        {
            Console.WriteLine("The {0}'s engine is off", Name);
        }

        public void TurnOn()
        {
            Console.WriteLine("The {0}'s engine is on", Name);
        }
    }
    //----------------------------------------------------------------------------------------
    //Factory classes
    public interface Factory
    {
        IAuto CreateAutomobile();
    }
    //concrete factory
    public class AudiTTSFactory : Factory
    {
        public IAuto CreateAutomobile()
        {
            return new AudiTTS();
        }
    }

}
