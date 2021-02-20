using CypherHash.Models;
using CypherHash.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CypherHash.Controllers
{
    public class HomeController : Controller
    {
        RepositoryHash repo;
        public HomeController(RepositoryHash repo) { this.repo = repo; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
          return View();
        }
        [HttpPost]
        public IActionResult Registrar
            (String name,String user,String pswd)
        {
            this.repo.InsertUser(name, user, pswd);
            ViewData["MENSAJE"] = "Datos almacenados";
            return RedirectToAction("Credentials","Home");
        }

        [HttpGet]
        public IActionResult Credentials()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Credentials(String user, String pswd)
        {
            Usuario usuario = this.repo.UserLogin(user, pswd);
            if (usuario == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password no válidos";
            }
            else
            {
                ViewData["MENSAJE"] =
                "Credenciales correctas, Sr/Sra " + usuario.name;
            }
            return View();
        }
    }
}
