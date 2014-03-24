using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace DesignPatterns
{
    //our basic service locator
    public class ServiceLocator
    {
        private static readonly Hashtable services = new Hashtable();

        public static void AddService<T>(T t)
        {
            services.Add(typeof(T).Name, t);
        }

        public static void AddService<T>(string name, T t)
        {
            services.Add(name, t);
        }

        public static T GetService<T>()
        {
            return (T)services[typeof(T).Name];
        }

        public static T GetService<T>(string serviceName)
        {
            return (T)services[serviceName];
        }
        //example of reading from a config file to create a service
        public static void RegisterServiceFromSettings(string serviceName)
        {
            //var loggerEntry = ConfigurationManager.AppSettings[serviceName];
            //var loggingObject = Assembly.GetExecutingAssembly().CreateInstance(loggerEntry);
            //ServiceLocator.AddService(serviceName, loggingObject);
        }
    }
    //----------------------------------------------------------------------------------------
    //these are the classes for our service
    //basic stubs
    public interface ILog
    {
        void Log(string txt);
    }
    public class LoggingService : ILog
    {
        public void Log(string txt)
        {
            Console.WriteLine(txt);
        }
    }
}
