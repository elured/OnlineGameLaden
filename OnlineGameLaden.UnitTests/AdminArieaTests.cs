using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Entities;
using OnlineGameLaden.WebUI.Controllers;
using System.Linq;

namespace OnlineGameLaden.UnitTests
{
    /// <summary>
    /// Summary description for AdminArieaTests
    /// </summary>
    [TestClass]
    public class AdminArieaTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game1"},
                new Game { GameId = 2, Name = "Game2"},
                new Game { GameId = 3, Name = "Game3"},
                new Game { GameId = 4, Name = "Game4"},
                new Game { GameId = 5, Name = "Game5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Action
            List<Game> result = ((IEnumerable<Game>)controller.Index().
                ViewData.Model).ToList();

            // Assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Game1", result[0].Name);
            Assert.AreEqual("Game2", result[1].Name);
            Assert.AreEqual("Game3", result[2].Name);
        }

        [TestMethod]
        public void CanEditGame()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(i => i.Games).Returns(new List<Game>{
                new Game { GameId = 1, Name = "Game1" },
                new Game { GameId = 2, Name = "Game2" },
                new Game { GameId = 3, Name = "Game3" },
                new Game { GameId = 4, Name = "Game4" },
                new Game { GameId = 5, Name = "Game5" }
            });
            AdminController controller = new AdminController(mock.Object);

            // Action
            Game game1 = controller.Edit(1).ViewData.Model as Game;
            Game game2 = controller.Edit(2).ViewData.Model as Game;
            Game game3 = controller.Edit(3).ViewData.Model as Game;

            // Assert
            Assert.AreEqual(1, game1.GameId);
            Assert.AreEqual(2, game2.GameId);
            Assert.AreEqual(3, game3.GameId);

        }

        [TestMethod]
        public void CannotEditNonexistentGame()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(i => i.Games).Returns(new List<Game> {
                new Game{GameId = 1, Name = "Game1"},
                new Game{GameId = 2, Name = "Game2"},
                new Game{GameId = 3, Name = "Game3"},
                new Game{GameId = 4, Name = "Game4"},
                new Game{GameId = 5, Name = "Game5"},
            });
            AdminController controller = new AdminController(mock.Object);

            // Action
            Game result = controller.Edit(6).ViewData.Model as Game;
            // Assert

            Assert.IsNull(result);

        }

        // Arrange
        // Action
        // Assert
    }
}
