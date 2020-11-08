using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RazasPerros.Models;
using RazasPerros.Models.ViewModels;
using RazasPerros.Repositories;

namespace RazasPerros.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdministradorController : Controller
    {
        public IWebHostEnvironment Environment { get; set; }
        public AdministradorController(IWebHostEnvironment env)
        {
            Environment = env;
        }
        public IActionResult Index(string id)
        {
            sistem14_razasContext context = new sistem14_razasContext();
            RazasRepository repos = new RazasRepository(context);
            InfoPerroViewModel vm = new InfoPerroViewModel();
            vm.Razas = repos.GetRazas();
            return View(vm);
        }
        public IActionResult Agregar()
        {
            InfoPerroViewModel vm = new InfoPerroViewModel();
            sistem14_razasContext context = new sistem14_razasContext();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(InfoPerroViewModel vm)
        {
            sistem14_razasContext context = new sistem14_razasContext(); ;
            try
            {
                RazasRepository repos = new RazasRepository(context);
                repos.Insert(vm.Raza);
                return RedirectToAction("Index", "Administrador");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
        public IActionResult Editar(int id)
        {
            sistem14_razasContext context = new sistem14_razasContext();
            InfoPerroViewModel vm = new InfoPerroViewModel();
            RazasRepository repos = new RazasRepository(context);
            vm.Raza = repos.GetById(id);
            if (vm.Raza == null)
            {
                return RedirectToAction("Index", "Administrador");
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Editar(InfoPerroViewModel vm)
        {
            sistem14_razasContext context = new sistem14_razasContext();
            try
            {
                RazasRepository repos = new RazasRepository(context);
                var objeto = repos.GetById(vm.Raza.Id);
                if (objeto != null)
                {
                    objeto.Nombre = vm.Raza.Nombre;
                    objeto.Descripcion = vm.Raza.Descripcion;
                    objeto.OtrosNombres = vm.Raza.OtrosNombres;
                    objeto.IdPais = vm.Raza.IdPais;
                    objeto.PesoMin = vm.Raza.PesoMin;
                    objeto.PesoMax = vm.Raza.PesoMax;
                    objeto.AlturaMin = vm.Raza.AlturaMin;
                    objeto.AlturaMax = vm.Raza.AlturaMax;
                    objeto.EsperanzaVida = vm.Raza.EsperanzaVida;
                    repos.Update(objeto);
                }
                return RedirectToAction("Index", "Administrador");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
        public IActionResult Eliminar(int id)
        {
            using (sistem14_razasContext context = new sistem14_razasContext())
            {
                RazasRepository repos = new RazasRepository(context);
                var objeto = repos.GetById(id);
                if (objeto != null)
                {
                    return View(objeto);
                }
                else
                    return RedirectToAction("Index", "Administrador");
            }
        }
        [HttpPost]
        public IActionResult Eliminar(Razas ra)
        {
            using (sistem14_razasContext context = new sistem14_razasContext())
            {
                RazasRepository repos = new RazasRepository(context);
                var raza = repos.GetById(ra.Id);
                if (raza != null)
                {
                    repos.Delete(raza);
                    return RedirectToAction("Index", "Administrador");
                }
                else
                {
                    ModelState.AddModelError("", "La raza no existe o ya ha sido eliminada.");
                    return View(ra);
                }
            }
        }
        public IActionResult Imagen(int id)
        {
            sistem14_razasContext context = new sistem14_razasContext();
            InfoPerroViewModel vm = new InfoPerroViewModel();
            RazasRepository repos = new RazasRepository(context);
            vm.Raza = repos.GetById(id);
            if (System.IO.File.Exists(Environment.WebRootPath + "/especies/" + vm.Raza.Id + ".jpg"))
            {
                vm.Imagen = vm.Raza.Id + ".jpg";
            }
            else
            {
                vm.Imagen = "nophoto.jpg";
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Imagen(InfoPerroViewModel vm)
        {
            try
            {
                if (vm.Archivo == null)
                {
                    ModelState.AddModelError("", "Seleccione una imagen de la especie.");
                    return View(vm);
                }
                else
                {
                    if (vm.Archivo.ContentType != "image/jpeg" || vm.Archivo.Length > 1024 * 1024 * 2)
                    {
                        ModelState.AddModelError("", "Debe seleccionar un archivo tipo .jpg menor de 2MB.");
                        return View(vm);
                    }
                }
                if (vm.Archivo != null)
                {
                    FileStream fs = new FileStream
                        (Environment.WebRootPath + "/especies/" + vm.Raza.Id + ".jpg", FileMode.Create);
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