using LabG9.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LabG9.Controllers
{
    public class CarritoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult VerCarrito()
        {
            var carrito = Session["Carrito"] as List<DetalleFactura> ?? new List<DetalleFactura>();
            return View(carrito);
        }

        [HttpPost]
        public ActionResult AgregarAlCarrito(int idProducto)
        {
            var producto = context.Productos.Find(idProducto);
            if (producto == null)
                return HttpNotFound();

            var carrito = Session["Carrito"] as List<DetalleFactura> ?? new List<DetalleFactura>();

            var item = carrito.FirstOrDefault(p => p.IdProducto == idProducto);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                var nuevoDetalle = new DetalleFactura
                {
                    IdProducto = producto.IdProducto,
                    Producto = producto,
                    Cantidad = 1,
                    PrecioUnitario = producto.Precio
                };

                carrito.Add(nuevoDetalle);
            }

            Session["Carrito"] = carrito;
            return RedirectToAction("Catalogo", "Producto");
        }

        [HttpPost]
        public ActionResult EliminarDelCarrito(int idProducto)
        {
            var carrito = Session["Carrito"] as List<DetalleFactura> ?? new List<DetalleFactura>();
            var item = carrito.FirstOrDefault(p => p.IdProducto == idProducto);

            if (item != null)
            {
                carrito.Remove(item);
            }

            Session["Carrito"] = carrito;
            return RedirectToAction("VerCarrito");
        }
        [Authorize]
        [HttpGet]
        public ActionResult SeleccionarMetodoPago()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult SeleccionarMetodoPago(string metodoPago)
        {
            if (string.IsNullOrEmpty(metodoPago))
            {
                ModelState.AddModelError("", "Seleccione un método de pago.");
                return View();
            }

            Session["MetodoPago"] = metodoPago;
            return RedirectToAction("PreConfirmarCompra");
        }

        [Authorize]
        [HttpGet]
        public ActionResult PreConfirmarCompra()
        {
            var carrito = Session["Carrito"] as List<DetalleFactura>;

            if (carrito == null || !carrito.Any())
                return RedirectToAction("VerCarrito");

            var clienteId = User.Identity.GetUserId();
            var cliente = context.Users.FirstOrDefault(u => u.Id == clienteId);

            if (cliente == null)
                return HttpNotFound(); 

            ViewBag.Carrito = carrito;
            ViewBag.DireccionEntrega = cliente.direccionEntrega;
            ViewBag.MetodoPago = Session["MetodoPago"]?.ToString();

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult ConfirmarCompra()
        {
            var carrito = Session["Carrito"] as List<DetalleFactura>;

            if (carrito == null || !carrito.Any())
                return RedirectToAction("VerCarrito");

            var clienteId = User.Identity.GetUserId();
            var cliente = context.Users.FirstOrDefault(u => u.Id == clienteId);
            var metodoPago = Session["MetodoPago"].ToString();

            var factura = new Factura
            {
                ClienteId = clienteId,
                FechaPedido = DateTime.Now,
                DireccionEntrega = cliente.direccionEntrega,
                MetodoPago = metodoPago,
                Estado = "Pendiente"
            };

            context.Facturas.Add(factura);
            context.SaveChanges(); 


            foreach (var detalle in carrito)
            {
                detalle.IdFactura = factura.IdFactura; 
                context.DetalleFacturas.Add(detalle);
            }


            foreach (var detalle in carrito)
            {
                var producto = context.Productos.Find(detalle.IdProducto);
                if (producto != null)
                {
                    producto.cantidad -= detalle.Cantidad;
                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
            }

            context.SaveChanges(); 

            Session["Carrito"] = null;

            return RedirectToAction("ResumenCompra", new { id = factura.IdFactura });
        }


        [Authorize]
        [HttpGet]
        public ActionResult ResumenCompra(int id)
        {
            var clienteId = User.Identity.GetUserId();
            var cliente = context.Users.FirstOrDefault(u => u.Id == clienteId);
            ViewBag.Nombre = cliente.nombreCompleto;
            var factura = context.Facturas
                .Include("DetalleFactura.Producto")
                .FirstOrDefault(f => f.IdFactura == id);

            if (factura == null)
                return HttpNotFound();

            return View(factura);
        }

    }
}
