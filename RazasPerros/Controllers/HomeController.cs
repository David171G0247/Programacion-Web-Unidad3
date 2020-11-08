using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazasPerros.Models;
using RazasPerros.Models.ViewModels;
using RazasPerros.Repositories;
using System.Linq;

namespace RazasPerros.Controllers
{
    public class HomeController : Controller
    {
        perrosContext context;
        public HomeController(perrosContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index(string id)
        {
            RazasRepository repos = new RazasRepository();
            IndexViewModel vm = new IndexViewModel();
            vm.Razas = id == null ? repos.GetRazas() : repos.GetRazasByLetraInicial(id);
            vm.LetrasIniciales = repos.GetLetrasIniciales();
            return View(vm);
        }
        [Route("Raza/{id}")]
        public IActionResult InfoPerros(string id)
        {
            RazasRepository repos = new RazasRepository();
            InfoPerroViewModel vm = new InfoPerroViewModel();
            vm.Raza = repos.GetRazaByNombre(id);
            if (vm.Raza == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                vm.OtrasRazas = repos.Get4RandomRazasExcept(id);
                return View(vm);
            }
        }
        [Route("RazasPorPais")]
        public IActionResult RazasPorPais()
        {
            RazasRepository repos = new RazasRepository();
            RazasPorPaisViewModel vm = new RazasPorPaisViewModel();
            var paises = context.Paises.Include(x => x.Razas).OrderBy(x => x.Nombre)
                .Select(x => new RazasPorPaisViewModel { Pais=x.Nombre, Razas= x.Razas });
            return View(paises);
        }
    }
}