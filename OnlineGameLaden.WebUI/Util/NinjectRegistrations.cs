using Moq;
using Ninject.Modules;
using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Concrete;
using OnlineGameLaden.Domain.Entities;
using OnlineGameLaden.WebUI.Util.Abstract;
using OnlineGameLaden.WebUI.Util.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OnlineGameLaden.WebUI.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //Mock<IGameRepository> mock = new Mock<IGameRepository>();
            //mock.Setup(m => m.Games).Returns(new List<Game>
            //    {
            //        new Game { Name = "SimCity", Price = 1499 },
            //        new Game { Name = "TITANFALL", Price=2299 },
            //        new Game { Name = "Battlefield 4", Price=899.4M }
            //    });


            Bind<IGameRepository>().To<EFGameRepository>();

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