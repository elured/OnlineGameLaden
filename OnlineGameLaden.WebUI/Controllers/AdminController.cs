using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameLaden.WebUI.Controllers
{
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
        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games.FirstOrDefault(i => i.GameId == gameId);
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProdukt(game);
                TempData["message"] = $"Die Änderungen im Produkt {game.Name} wurden gespeichert";
                return RedirectToAction("Index");
            }
            else
                //etwas ist schief gegangen
                return View(game);
        }
    }
}
