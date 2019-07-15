using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameLaden.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games.FirstOrDefault(i => i.GameId == gameId);
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }
                repository.SaveProdukt(game);
                TempData["message"] = $"Die Änderungen im Produkt {game.Name} wurden gespeichert";
                return RedirectToAction("Index");
            }
            else
                //etwas ist schief gegangen
                return View(game);
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game gameToDel = repository.DeleteProdukt(gameId);
            if(gameToDel != null)
            {
                TempData["message"] = $"Das Speil \"{gameToDel.Name}\" wurde gelöscht";
            }
            return RedirectToAction("Index");
        }
    }
}
