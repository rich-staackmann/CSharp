using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using Moq;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    //we are using ninject to create a custom controller factory
    //In other example we just added a DI container, but this is used to mix things up
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        //the bindings are where we put our actual DI bindings for use by the app
        private void AddBindings()
        {
            //this binding is for our EF repository
            //of course with dependency injection we are free to change our method of persistence
            //and all we need to do is change this line here
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}