using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Controllers;
using RubiksCubeStore.WebUI.HtmlHelpers;
using RubiksCubeStore.WebUI.Models;

namespace RubiksCubeStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            // Организация (arrange)
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube>
            {
                new Cube { CubeId = 1, Name = "Cube1"},
                new Cube { CubeId = 2, Name = "Cube2"},
                new Cube { CubeId = 3, Name = "Cube3"},
                new Cube { CubeId = 4, Name = "Cube4"},
                new Cube { CubeId = 5, Name = "Cube5"}
            });
            CubeController controller = new CubeController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            CubesListViewModel result = (CubesListViewModel)controller.List(null, 2).Model;

            // Утверждение
            List<Cube> cubes = result.Cubes.ToList();
            Assert.IsTrue(cubes.Count == 2);
            Assert.AreEqual(cubes[0].Name, "Cube4");
            Assert.AreEqual(cubes[1].Name, "Cube5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {

            // Arrange 
            HtmlHelper myHelper = null;
            // Erstellen vom PagingInfoObjekt
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube>
            {
                new Cube { CubeId = 1, Name = "Cube1"},
                new Cube { CubeId = 2, Name = "Cube2"},
                new Cube { CubeId = 3, Name = "Cube3"},
                new Cube { CubeId = 4, Name = "Cube4"},
                new Cube { CubeId = 5, Name = "Cube5"}
            });
            CubeController controller = new CubeController(mock.Object);
            controller.pageSize = 3;

            // Act
            CubesListViewModel result = (CubesListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Cubes()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube>
            {
                new Cube { CubeId = 1, Name = "Cube1", Category="Cat1"},
                new Cube { CubeId = 2, Name = "Cube2", Category="Cat2"},
                new Cube { CubeId = 3, Name = "Cube3", Category="Cat1"},
                new Cube { CubeId = 4, Name = "Cube4", Category="Cat2"},
                new Cube { CubeId = 5, Name = "Cube5", Category="Cat3"}
            });
            CubeController controller = new CubeController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Cube> result = ((CubesListViewModel)controller.List("Cat2", 1).Model)
                .Cubes.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Cube2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Cube4" && result[1].Category == "Cat2");
        }
        [TestMethod]
        public void CanCreateCategories()
        {
            // Arrange - Erstellen neuen Repository
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube> {
                new Cube { CubeId = 1, Name = "Cube1", Category="Rubik's Cube Variations"},
                new Cube { CubeId = 2, Name = "Cube2", Category="Rubik's Cube Variations"},
                new Cube { CubeId = 3, Name = "Cube3", Category="Sonstige"},
                new Cube { CubeId = 4, Name = "Cube4", Category="Rubik's Cube"},
            });

            NavController target = new NavController(mock.Object);

            // Action
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Assert
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Rubik's Cube");
            Assert.AreEqual(results[1], "Rubik's Cube Variations");
            Assert.AreEqual(results[2], "Sonstige");
        }

        [TestMethod]
        public void IndicatesSelectedCategory()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new Cube[] {
                new Cube { CubeId = 1, Name = "Cube1", Category="Rubik's Cube Variations"},
                new Cube { CubeId = 2, Name = "Cube2", Category="Sonstige"}
            });

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Sonstige";

            // Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void GenerateCategorySpecificCubeCount()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(m => m.Cubes).Returns(new List<Cube>
            {
                new Cube { CubeId = 1, Name = "Cube1", Category="Cat1"},
                new Cube { CubeId = 2, Name = "Cube2", Category="Cat2"},
                new Cube { CubeId = 3, Name = "Cube3", Category="Cat1"},
                new Cube { CubeId = 4, Name = "Cube4", Category="Cat2"},
                new Cube { CubeId = 5, Name = "Cube5", Category="Cat3"}
            });
            CubeController controller = new CubeController(mock.Object);
            controller.pageSize = 3;

            // Action
            int res1 = ((CubesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((CubesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((CubesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((CubesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
