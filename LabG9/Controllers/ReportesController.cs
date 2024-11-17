using LabG9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabG9.Controllers
{
    public class ReportesController : Controller
    {

        private ApplicationDbContext context = new ApplicationDbContext();
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult VentasPorMes()
        {
            var reporte = context.Database.SqlQuery<VentasModelView>(
                "EXEC sp_Reportes_VentasPorMes"
            ).ToList();

            return View(reporte);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ProductosMasVendidos()
        {
            var reporte = context.Database.SqlQuery<ProductoVendido>(
                "EXEC sp_Reportes_ProductosMasVendidos"
            ).ToList();

            return View(reporte);
        }
    }
}

