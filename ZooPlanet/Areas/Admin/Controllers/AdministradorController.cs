using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ZooPlanet.Models;
using ZooPlanet.Models.ViewModels;
using ZooPlanet.Repositories;

namespace ZooPlanet.Controllers
{
    [Area("Admin")]
    public class AdministradorController : Controller
    {
        animalesContext context;
        public IWebHostEnvironment Environment { get; set; }
        public AdministradorController(IWebHostEnvironment env, animalesContext ctx)
        {
            Environment = env;
            context = ctx;
        }

        public IActionResult Index()
        {
            EspeciesRepository er = new EspeciesRepository(context);
            return View(er.GetAll());
        }
        public IActionResult Agregar()
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            ClasesRepository cr = new ClasesRepository(context);
            vm.Clases = cr.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(EspeciesViewModel vm)
        {
            try
            {
                ClasesRepository cr = new ClasesRepository(context);
                vm.Clases = cr.GetAll();
                EspeciesRepository er = new EspeciesRepository(context);
                er.Insert(vm.Especie);
                return RedirectToAction("Index","Administrador");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository cr = new ClasesRepository(context);
                vm.Clases = cr.GetAll();
                return View(vm);
            }

        }
        public IActionResult Editar(int id)
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository er = new EspeciesRepository(context);
            vm.Especie = er.GetById(id);
            if (vm.Especie == null)
            {
                return RedirectToAction("Index", "Administrador");
            }
            ClasesRepository cr = new ClasesRepository(context);
            vm.Clases = cr.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Editar(EspeciesViewModel vm)
        {
            try
            {
                ClasesRepository cr = new ClasesRepository(context);
                vm.Clases = cr.GetAll();
                EspeciesRepository er = new EspeciesRepository(context);
                var e = er.GetById(vm.Especie.Id);
                if (e != null)
                {
                    e.Especie = vm.Especie.Especie;
                    e.IdClase = vm.Especie.IdClase;
                    e.Habitat = vm.Especie.Habitat;
                    e.Peso = vm.Especie.Peso;
                    e.Tamaño = vm.Especie.Tamaño;
                    e.Observaciones = vm.Especie.Observaciones;
                    er.Update(e);
                }
                return RedirectToAction("Index", "Administrador");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository cr = new ClasesRepository(context);
                vm.Clases = cr.GetAll();
                return View(vm);
            }
        }
        public IActionResult Eliminar(int id)
        {
            EspeciesRepository er = new EspeciesRepository(context);
            var e = er.GetEspecieById(id);
            if (e != null)
            {
                return View(e);
            }
            else
                return RedirectToAction("Index", "Administrador");
        }

        [HttpPost]
        public IActionResult Eliminar(Especies e)
        {
            EspeciesRepository er = new EspeciesRepository(context);
            var especie = er.GetEspecieById(e.Id);
            if (especie != null)    
            {
                er.Delete(especie);
                return RedirectToAction("Index", "Administrador");
            }
            else
            {
                ModelState.AddModelError("", "Esta especie no existe o ya ha sido eliminado");
                return View(e);
            }
        }
        public IActionResult Imagen(int id)
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository repos = new EspeciesRepository(context);
            vm.Especie = repos.GetById(id);
            if (System.IO.File.Exists(Environment.WebRootPath + "/especies/" + vm.Especie.Id + ".jpg"))
            {
                vm.Imagen = vm.Especie.Id + ".jpg";
            }
            else
            {
                vm.Imagen = "nophoto.jpg";
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Imagen(EspeciesViewModel vm)
        {
            try
            {
                if (vm.Archivo == null)
                {
                    ModelState.AddModelError("", "Seleccione una imagen de la especie.");
                    return View(vm);
                }
                else if (vm.Archivo.ContentType != "image/jpeg" || vm.Archivo.Length > 1024 * 1024 * 2)
                {
                    ModelState.AddModelError("", "Debe seleccionar un archivo tipo .jpg menor de 2MB.");
                    return View(vm);
                }
                if (vm.Archivo != null)
                {
                    FileStream fs = new FileStream
                        (Environment.WebRootPath + "/especies/" + vm.Especie.Id + ".jpg", FileMode.Create);
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index", "Administrador");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}