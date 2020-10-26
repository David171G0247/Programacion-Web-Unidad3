using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitStore.Models;
using FruitStore.Repositories;

namespace FruitStore.Controllers
{
    public class CategoriasController : Controller
    {
        [Route("Categorias")]
        public IActionResult Index()
        {
            fruteriashopContext context = new fruteriashopContext();
            Repositories.Repository<Categorias> repos = new Repositories.Repository<Categorias>(context);

            return View(repos.GetAll().Where(x=>x.Eliminado==false).OrderBy(x=>x.Nombre));
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            c.Eliminado = false;
            try 
            { 
            fruteriashopContext context = new fruteriashopContext();
            CategoriasRepository repos = new CategoriasRepository(context);
            repos.insert(c);
            return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }
        }
        public IActionResult Editar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);
                if(categoria==null)
                {
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
        }
        [HttpPost]
        public IActionResult Editar(Categorias vm)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var original = repos.Get(vm.Id);

                    if (original != null)
                    {
                        original.Nombre = vm.Nombre;
                        repos.update(original);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
        public IActionResult Eliminar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);
                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(categoria);
            }
        }
        [HttpPost]
        public IActionResult Eliminar(Categorias c)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoria = repos.Get(c.Id);

                    categoria.Eliminado = true;

                    repos.update(categoria);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }
        }
    }
}