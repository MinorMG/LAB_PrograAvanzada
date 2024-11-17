using LabG9.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabG9.Controllers
{
    public class FacturaController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [Authorize]
        [HttpGet]

        public ActionResult HistorialPedido()
        {
            var clienteId = User.Identity.GetUserId();
            var cliente = context.Users.FirstOrDefault(u => u.Id == clienteId);
            ViewBag.Nombre = cliente.nombreCompleto;

            var Facturas = context.Facturas.Where(c => c.ClienteId == clienteId).ToList();

            return View(Facturas);
        }
        [Authorize]
        [HttpGet]
        public ActionResult DetalleFactura(int id)
        {
            var factura = context.Facturas.FirstOrDefault(f => f.IdFactura == id);


            var detallesFactura = context.DetalleFacturas
                                          .Where(d => d.IdFactura == id)
                                          .Include(d => d.Producto)
                                          .ToList();


            ViewBag.Factura = factura;

            return View(detallesFactura);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Facturas()
        {
            var facturas = context.Facturas.ToList();
            return View(facturas);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var factura = context.Facturas.Find(id);
            if (factura == null)
                return HttpNotFound();
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Factura factura)
        {
            if (ModelState.IsValid)
            {
                context.Entry(factura).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Facturas");
            }
            return View(factura);
        }
    }
}