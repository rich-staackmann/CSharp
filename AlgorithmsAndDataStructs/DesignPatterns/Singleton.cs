using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //simple singleton, not threadsafe
    public class Singleton
    {
        private static Singleton _instance; //GLOBAL STATE

        //ctor is private so only we can call it
        private Singleton() { }

        //this method creates an instance the first time
        //and returns the instance every time after
        public static Singleton Instance()
        {
            if (_instance == null)
                _instance = new Singleton();
            return _instance;
        }

        public void DoSomething() { } //imagine this is a method people want to use
    }

    //threadsafe and lazy loading singleton
    public class LazySingleton
    {
        private LazySingleton() { }

        public static LazySingleton Instance()
        {
            return Nested.instance;
        }

        private class Nested
        {
            static Nested() { } //forces the .NET runtime to use lazy initialization

            //whenever this is first referenced, we initialize it
            //because it is static and readonly, this is the only time we will create and instance
            internal static readonly LazySingleton instance = new LazySingleton(); 
        }
    }
}

//notes
//===================================
//If your class has no variables/properties or state it needs to hold then you can just declare it 
//a static class. This way it can never be instantiated.(i.e. System.Math)

//You can try to keep a global state variable that tracks how many instances you have created.
//However, in threaded programs you can have two threads execute the same bit of code and end up 
//with two instances of your singleton. We have to add a mutex to protect instance creation

//simplest c# implementation
//====================================================
//private static readonly singleton instance = new singleton();
//
//private singleton()
//{}
//
//public static singleton CreateInstance()
//        {get {return instance;}}

//a static readonly variable forces the CLR to take care of the initializing, locking, and memory
//management for you. No more mutex or anyother issues. However, you now have an instance created when
//he program starts up, not just when you ask for one. You can create an empty static constructor to
//force the CLR to be lazy and only create an instance just before it is needed