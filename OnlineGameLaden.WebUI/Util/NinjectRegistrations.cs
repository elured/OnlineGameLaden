using Moq;
using Ninject.Modules;
using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Concrete;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Util.Abstract;
using RubiksCubeStore.WebUI.Util.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RubiksCubeStore.WebUI.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ICubeRepository>().To<EFCubeRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}