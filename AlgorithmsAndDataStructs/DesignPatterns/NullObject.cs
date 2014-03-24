using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // We are using the IAuto interface from the Factory file  \\

    //Our null object
    //often the Null Object is created as a singleton in some abstract base class
    //we can then just set references to the null object through the singleton
    public class NullAuto : IAuto
    {
        public string Name
        {
            get { return string.Empty; }
        }

        public void TurnOff()
        {
        }

        public void TurnOn()
        {
        }
    }

    class NullObject
    {
    }
}
