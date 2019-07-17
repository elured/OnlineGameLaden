using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using RubiksCubeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubiksCubeStore.WebUI.Controllers
{
    public class CubeController : Controller
    {
        private ICubeRepository repository;
        public int pageSize = 4;

        public CubeController(ICubeRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            var first = repository.Cubes.FirstOrDefault();

            CubesListViewModel model = new CubesListViewModel
            {
                Cubes = repository.Cubes
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(cube => cube.CubeId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                repository.Cubes.Count() :
                repository.Cubes.Where(cube => cube.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int cubeId)
        {
            Cube cube = repository.Cubes.FirstOrDefault(i => i.CubeId == cubeId);
            if (cube != null)
                return File(cube.ImageData, cube.ImageMimeType);
            else
                return null;
        }
    }
}