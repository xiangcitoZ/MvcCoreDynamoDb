using Microsoft.AspNetCore.Mvc;
using MvcCoreDynamoDb.Models;
using MvcCoreDynamoDb.Services;

namespace MvcCoreDynamoDb.Controllers
{
    public class CochesController : Controller
    {
        private ServiceDynamoDb service;

        public CochesController(ServiceDynamoDb service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Coche> coches = await this.service.GetCochesAsync();
            return View(coches);
        }

        public async Task<IActionResult> Details(int id)
        {
            Coche car = await this.service.FindCocheAsync(id);
            return View(car);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteCocheAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Coche car, string incluir)
        {
            if(incluir == null)
            {
                car.Motor = null;
            }

            await this.service.CreateCocheAsync(car);
            return RedirectToAction("Index");
        }



    }
}
