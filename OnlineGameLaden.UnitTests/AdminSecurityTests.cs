using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineGameLaden.WebUI.Controllers;
using OnlineGameLaden.WebUI.Models;
using OnlineGameLaden.WebUI.Util.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineGameLaden.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void CanLoginWithValidCredentials()
        {
            // Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(i => i.Authenticate("admin", "12345")).Returns(true);
            LoginModel model = new LoginModel { UserName = "admin", Password = "12345" };
            AccountController account = new AccountController(mock.Object);

            // Action
             AccountController target = new AccountController(mock.Object);
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }
        [TestMethod]
        public void CannotLoginWithInvalidCredentials()
        {
            // Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(i => i.Authenticate("badUser", "badPass")).Returns(false);
            LoginModel model = new LoginModel { UserName = "badUser", Password = "badPass" };
            AccountController target = new AccountController(mock.Object);

            // Action
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

    }
}
