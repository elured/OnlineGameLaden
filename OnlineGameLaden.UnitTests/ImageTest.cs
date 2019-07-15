using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Entities;
using OnlineGameLaden.WebUI.Controllers;

namespace OnlineGameLaden.UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void CanRetrieveImageData()
        {
            // Arrange
            Game game = new Game
            {
                GameId = 2,
                Name = "Puzzle2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(i => i.Games).Returns(new List<Game>{
                new Game{
                        GameId = 1,
                        Name = "Puzzle1"
                },
                game,
                new Game
                {
                    GameId = 3,
                    Name = "Puzzle3"
                }
            }.AsQueryable());
            GameController controller = new GameController(mock.Object);

            // Action
            ActionResult result = controller.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(game.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void CannotRetrieveImageDataForInvalidID()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(i => i.Games).Returns(new List<Game>{
                new Game{
                        GameId = 1,
                        Name = "Puzzle1"
                },
                new Game
                {
                    GameId = 2,
                    Name = "Puzzle2"
                }
            }.AsQueryable);
            GameController controller = new GameController(mock.Object);

            // Action
            ActionResult result = controller.GetImage(10);

            // Assert
            Assert.IsNull(result);

        }
    }
}
