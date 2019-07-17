using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCubeStore.Domain.Entities;
using System.Linq;
using RubiksCubeStore.Domain.Abstract;
using Moq;
using RubiksCubeStore.WebUI.Controllers;
using System.Web.Mvc;

namespace RubiksCubeStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            // Arrange
            Cube cube1 = new Cube { CubeId = 1, Name = "Cube1" };
            Cube cube2 = new Cube { CubeId = 2, Name = "Cube2" };

            // Arrange
            Cart cart = new Cart();

            // Action
            cart.AddItem(cube1, 1);
            cart.AddItem(cube2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Cube, cube1);
            Assert.AreEqual(results[1].Cube, cube2);
        }

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange
            Cube cube1 = new Cube { CubeId = 1, Name = "Cube1" };
            Cube cube2 = new Cube { CubeId = 2, Name = "Cube2" };

            // Arrange
            Cart cart = new Cart();

            // Действие
            cart.AddItem(cube1, 1);
            cart.AddItem(cube2, 1);
            cart.AddItem(cube1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Cube.CubeId).ToList();

            // Assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void CanRemoveLine()
        {
            // Arrange
            Cube cube1 = new Cube { CubeId = 1, Name = "Cube1" };
            Cube cube2 = new Cube { CubeId = 2, Name = "Cube2" };
            Cube cube3 = new Cube { CubeId = 3, Name = "Cube3" };

            // Arrange
            Cart cart = new Cart();

            // Action
            cart.AddItem(cube1, 1);
            cart.AddItem(cube2, 4);
            cart.AddItem(cube3, 2);
            cart.AddItem(cube2, 1);

            // Action
            cart.RemoveLine(cube2);

            // Assert
            Assert.AreEqual(cart.Lines.Where(c => c.Cube == cube2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            // Arrange
            Cube cube1 = new Cube { CubeId = 1, Name = "Cube1", Price = 100 };
            Cube cube2 = new Cube { CubeId = 2, Name = "Cube2", Price = 55 };

            // Arrange
            Cart cart = new Cart();

            // Action
            cart.AddItem(cube1, 1);
            cart.AddItem(cube2, 1);
            cart.AddItem(cube1, 5);
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void CanClearContents()
        {
            // Arrange
            Cube cube1 = new Cube { CubeId = 1, Name = "Cube1", Price = 100 };
            Cube cube2 = new Cube { CubeId = 2, Name = "Cube2", Price = 55 };

            // Arrange
            Cart cart = new Cart();

            // Action
            cart.AddItem(cube1, 1);
            cart.AddItem(cube2, 1);
            cart.AddItem(cube1, 5);
            cart.Clear();

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        /// <summary>
        /// Проверяем добавление в корзину
        /// </summary>
        [TestMethod]
        public void CanAddToCart()
        {
            // Организация - создание имитированного хранилища
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube> {
        new Cube {CubeId = 1, Name = "Cube1", Category = "Cat1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, null);

            // Действие - добавить игру в корзину
            controller.AddToCart(cart, 1, null);

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Cube.CubeId, 1);
        }

        /// <summary>
        /// После добавления игры в корзину, должно быть перенаправление на страницу корзины
        /// </summary>
        [TestMethod]
        public void AddingCubeToCartGoesToCartScreen()
        {
            // Организация - создание имитированного хранилища
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube> {
        new Cube {CubeId = 1, Name = "Cube1", Category = "Cat1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, null);

            // Действие - добавить игру в корзину
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            // Утверждение
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        // Проверяем URL
        [TestMethod]
        public void CanViewCartContents()
        {
            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController target = new CartController(null, null);

            // Действие - вызов метода действия Index()
            CartIndexViewModel result
                = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Утверждение
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
        [TestMethod]
        public void CannotCheckoutEmptyCart()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация - создание пустой корзины
            Cart cart = new Cart();

            // Организация - создание деталей о доставке
            ShippingDetails shippingDetails = new ShippingDetails();

            // Организация - создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Действие
            ViewResult result = controller.Checkout(cart, shippingDetails);

            // Утверждение — проверка, что заказ не был передан обработчику 
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            // Утверждение — проверка, что метод вернул стандартное представление 
            Assert.AreEqual("", result.ViewName);

            // Утверждение - проверка, что-представлению передана неверная модель
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация — создание корзины с элементом
            Cart cart = new Cart();
            cart.AddItem(new Cube(), 1);

            // Организация — создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Организация — добавление ошибки в модель
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка перехода к оплате
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Утверждение - проверка, что заказ не передается обработчику
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            // Утверждение - проверка, что метод вернул стандартное представление
            Assert.AreEqual("", result.ViewName);

            // Утверждение - проверка, что-представлению передана неверная модель
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CanCheckoutAndSubmitOrder()
        {
            // Организация - создание имитированного обработчика заказов
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Организация — создание корзины с элементом
            Cart cart = new Cart();
            cart.AddItem(new Cube(), 1);

            // Организация — создание контроллера
            CartController controller = new CartController(null, mock.Object);

            // Действие - попытка перехода к оплате
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Утверждение - проверка, что заказ передан обработчику
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            // Утверждение - проверка, что метод возвращает представление 
            Assert.AreEqual("Completed", result.ViewName);

            // Утверждение - проверка, что представлению передается допустимая модель
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
