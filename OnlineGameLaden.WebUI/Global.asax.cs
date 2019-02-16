using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Concrete;
using OnlineGameLaden.Domain.Entities;
using OnlineGameLaden.WebUI.Util;
using OnlineGameLaden.WebUI.Util.Binders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineGameLaden.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));


        }
    }
}
