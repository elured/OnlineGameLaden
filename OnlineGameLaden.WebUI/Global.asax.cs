using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Concrete;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Util;
using RubiksCubeStore.WebUI.Util.Binders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RubiksCubeStore.WebUI
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
