using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    //the FormsAuthentication class requires us to call 2 static members
    //static members can't be mocked which makes unit testing difficult
    //we will separate the authentication controller from our auth class with this interface
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
