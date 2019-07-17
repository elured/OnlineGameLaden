using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubiksCubeStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ICubeRepository repository;

        public AdminController(ICubeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Cubes);
        }

        public ViewResult Create()
        {
            return View("Edit", new Cube());
        }

        public ViewResult Edit(int cubeId)
        {
            Cube cube = repository.Cubes.FirstOrDefault(i => i.CubeId == cubeId);
            return View(cube);
        }

        [HttpPost]
        public ActionResult Edit(Cube cube, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    cube.ImageMimeType = image.ContentType;
                    cube.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(cube.ImageData, 0, image.ContentLength);
                }
                repository.SaveProdukt(cube);
                TempData["message"] = $"Die Änderungen im Produkt {cube.Name} wurden gespeichert";
                return RedirectToAction("Index");
            }
            else
                //etwas ist schief gegangen
                return View(cube);
        }

        [HttpPost]
        public ActionResult Delete(int cubeId)
        {
            Cube cubeToDel = repository.DeleteProdukt(cubeId);
            if(cubeToDel != null)
            {
                TempData["message"] = $"Das Speil \"{cubeToDel.Name}\" wurde gelöscht";
            }
            return RedirectToAction("Index");
        }
    }
}
