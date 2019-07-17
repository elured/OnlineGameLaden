using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Controllers;

namespace RubiksCubeStore.UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void CanRetrieveImageData()
        {
            // Arrange
            Cube cube = new Cube
            {
                CubeId = 2,
                Name = "Puzzle2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(i => i.Cubes).Returns(new List<Cube>{
                new Cube{
                        CubeId = 1,
                        Name = "Puzzle1"
                },
                cube,
                new Cube
                {
                    CubeId = 3,
                    Name = "Puzzle3"
                }
            }.AsQueryable());
            CubeController controller = new CubeController(mock.Object);

            // Action
            ActionResult result = controller.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(cube.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void CannotRetrieveImageDataForInvalidID()
        {
            // Arrange
            Mock<ICubeRepository> mock = new Mock<ICubeRepository>();
            mock.Setup(i => i.Cubes).Returns(new List<Cube>{
                new Cube{
                        CubeId = 1,
                        Name = "Puzzle1"
                },
                new Cube
                {
                    CubeId = 2,
                    Name = "Puzzle2"
                }
            }.AsQueryable);
            CubeController controller = new CubeController(mock.Object);

            // Action
            ActionResult result = controller.GetImage(10);

            // Assert
            Assert.IsNull(result);

        }
    }
}
