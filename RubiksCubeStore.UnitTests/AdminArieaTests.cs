using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace RubiksCubeStore.UnitTests
{
    /// <summary>
    /// Summary description for AdminArieaTests
    /// </summary>
    [TestClass]
    public class AdminArieaTests
    {
        [TestMethod]
        public void IndexContainsAllCubes()
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

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Action
            List<Cube> result = ((IEnumerable<Cube>)controller.Index().
                ViewData.Model).ToList();

            // Assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Cube1", result[0].Name);
            Assert.AreEqual("Cube2", result[1].Name);
            Assert.AreEqual("Cube3", result[2].Name);
        }

        [TestMethod]
        public void CanEditCube()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(i => i.Cubes).Returns(new List<Cube>{
                new Cube { CubeId = 1, Name = "Cube1" },
                new Cube { CubeId = 2, Name = "Cube2" },
                new Cube { CubeId = 3, Name = "Cube3" },
                new Cube { CubeId = 4, Name = "Cube4" },
                new Cube { CubeId = 5, Name = "Cube5" }
            });
            AdminController controller = new AdminController(mock.Object);

            // Action
            Cube cube1 = controller.Edit(1).ViewData.Model as Cube;
            Cube cube2 = controller.Edit(2).ViewData.Model as Cube;
            Cube cube3 = controller.Edit(3).ViewData.Model as Cube;

            // Assert
            Assert.AreEqual(1, cube1.CubeId);
            Assert.AreEqual(2, cube2.CubeId);
            Assert.AreEqual(3, cube3.CubeId);

        }

        [TestMethod]
        public void CannotEditNonexistentCube()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(i => i.Cubes).Returns(new List<Cube> {
                new Cube{CubeId = 1, Name = "Cube1"},
                new Cube{CubeId = 2, Name = "Cube2"},
                new Cube{CubeId = 3, Name = "Cube3"},
                new Cube{CubeId = 4, Name = "Cube4"},
                new Cube{CubeId = 5, Name = "Cube5"},
            });
            AdminController controller = new AdminController(mock.Object);

            // Action
            Cube result = controller.Edit(6).ViewData.Model as Cube;
            // Assert

            Assert.IsNull(result);

        }

        [TestMethod]
        public void CanSaveValidChanges()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            AdminController controller = new AdminController(mock.Object);
            Cube cube = new Cube { Name = "Test" };

            // Action
            ActionResult result = controller.Edit(cube);

            // Assert 
            //Hier pruefen wir, ob repository aufgerufen wird
            mock.Verify(m => m.SaveProdukt(cube));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            AdminController controller = new AdminController(mock.Object);
            Cube cube = new Cube { Name = "Test" };
            controller.ModelState.AddModelError("error", "error");

            // Action
            ActionResult result = controller.Edit(cube);

            // Assert
            //Hier pruefen wir, ob repository NICHT aufgerufen wird
            mock.Verify(m => m.SaveProdukt(It.IsAny<Cube>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void CanDeleteValidCubes()
        {
            // Arrange - Erstellen vom Objekt
            Cube cube = new Cube { CubeId = 2, Name = "cube2" };

            // Arrange - Erstellen vom mockRepository
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(i => i.Cubes).Returns(new List<Cube> {
                    new Cube { CubeId = 1, Name = "cube1"},
                    new Cube { CubeId = 2, Name = "cube2"},
                    new Cube { CubeId = 3, Name = "cube3"},
                    new Cube { CubeId = 4, Name = "cube4"},
                    new Cube { CubeId = 5, Name = "cube5"}
            });

            // Arrange - Erstellen vom mock Repository
            AdminController controller = new AdminController(mock.Object);

            // Action - hier löschen wir das Spiel
            controller.Delete(cube.CubeId);

            // Assert
            mock.Verify(m => m.DeleteProdukt(cube.CubeId));
        }

        // Arrange
        // Action
        // Assert
    }
}
