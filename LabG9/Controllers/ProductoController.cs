using LabG9.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabG9.Controllers
{
    public class ProductoController : Controller
    {

        private ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public ActionResult Catalogo()
        {
            ViewBag.Categorias = context.Categorias.ToList();
            var productosDisponibles = context.Productos.Where(p => p.Estado == true).ToList();
            return View(productosDisponibles);
        }

        [HttpGet]
        public ActionResult FiltrarXCategoria(int? IdCategoria)
        {
            ViewBag.Categorias = context.Categorias.ToList();

            if (IdCategoria == null)
            {
                ModelState.AddModelError("", "Seleccione una Categoría");
                return RedirectToAction("Catalogo");
            }

            var productosDisponibles = context.Productos.Where(p => p.IdCategoria == IdCategoria && p.Estado == true).ToList();
            return View(productosDisponibles);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult GestionProductos()
        {
            ViewBag.Categorias=context.Categorias.ToList();
            var productos=context.Productos.ToList();
            return View(productos);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.Categorias = context.Categorias.ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Crear(Producto producto)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            if (ModelState.IsValid)
            {
                context.Productos.Add(producto);
                context.SaveChanges();
                return RedirectToAction("GestionProductos");
            }
            return View(producto);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var producto = context.Productos.Find(id);
            if (producto == null)
                return HttpNotFound();
            return View(producto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            if (ModelState.IsValid)
            {
                context.Entry(producto).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("GestionProductos");
            }
            return View(producto);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var producto=context.Productos.SingleOrDefault(r=>r.IdProducto==id);
            if (producto == null)
                return HttpNotFound();
            return View(producto);  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmacion(int? id)
        {
            var producto = context.Productos.Find(id);
            context.Productos.Remove(producto);
            context.SaveChanges();
            return RedirectToAction("GestionProductos");

        }


    }
    }
