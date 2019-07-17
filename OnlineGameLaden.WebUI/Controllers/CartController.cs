using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubiksCubeStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICubeRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(ICubeRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int cubeId, string returnUrl)
        {
            Cube cube = repository.Cubes
                .FirstOrDefault(g => g.CubeId == cubeId);

            if (cube != null)
            {
                cart.AddItem(cube, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int cubeId, string returnUrl)
        {
            Cube cube = repository.Cubes
                .FirstOrDefault(g => g.CubeId == cubeId);

            if (cube != null)
            {
                cart.RemoveLine(cube);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ihr Warenkorb ist derzeit leer!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}