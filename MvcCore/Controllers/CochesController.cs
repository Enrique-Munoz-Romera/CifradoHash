using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CochesController : Controller
    {
       // RepositoryCochesSql repo;
        RepositoryCochesMySql repo;

        public CochesController(RepositoryCochesMySql repo)//RepositoryCochesSql repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }
        [HttpGet]
        public IActionResult DetallesCoches(int id)
        {
            Coche coche = this.repo.GetCocheId(id);
            return View(coche);
        }
        [HttpGet]
        public IActionResult EditarCoche(int id)
        {
            Coche car = this.repo.GetCocheId(id);
            return View(car);
        }
        [HttpPost]
        public IActionResult EditarCoche(Coche car)
        {
            this.repo.EditarCoche( car.id, car.marca, car.modelo, car.conductor, car.imagen);
            return RedirectToAction("DetallesCoches","Coches",new { id = car.id});
        }
        [HttpGet]
        public IActionResult CrearCoche()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearCoche(string marca,string modelo,string conductor,string imagen)
        {
             this.repo.Insertarcoche(marca, modelo, conductor, imagen);
         
            return RedirectToAction("Index", "Coches");
        }
        [HttpGet]
        public IActionResult EliminarCoche(int id)
        {
            Coche car = this.repo.GetCocheId(id);
            return View(car);
        }
        [HttpPost]
        public IActionResult EliminarCoche(int id,string marca)
        {
            //this.repo.EliminarCocheId(id);
            this.repo.DeleteCar(id, marca);
            return RedirectToAction("Index", "Coches");
        }
    }
}
