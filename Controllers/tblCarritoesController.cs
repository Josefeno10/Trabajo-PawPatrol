// Controlador: tblCarritoesController.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PedidosComida.Models;

namespace PedidosComida.Controllers
{
    public class tblCarritoesController : Controller
    {
        private db_aba1b3_pedidoscomidaEntities db = new db_aba1b3_pedidoscomidaEntities();

        // GET: tblCarritoes
        [Authorize]
        public ActionResult Index()
        {
            string cliente = User.Identity.Name;
            ViewBag.NombreCliente = cliente;

            var pedido = db.tblPedido.FirstOrDefault(p => p.NombreCliente == cliente && p.Estado == "EnProceso");

            if (pedido == null)
            {
                ViewBag.Mensaje = "Tu carrito está vacío.";
                return View(new List<tblCarrito>());
            }

            var carrito = db.tblCarrito
                .Where(c => c.ID_Pedido == pedido.ID_Pedido)
                .Include("tblProducto")
                .ToList();

            ViewBag.ID_Pedido = pedido.ID_Pedido;
            return View(carrito);
        }

        // POST: ConfirmarPedido desde la vista del carrito
        [HttpPost]
        [Authorize]
        public ActionResult ConfirmarPedido(int idPedido, string direccion, string telefono)
        {
            var pedido = db.tblPedido.Find(idPedido);
            if (pedido == null || pedido.Estado != "EnProceso")
                return RedirectToAction("Index");

            pedido.Direccion = direccion;
            pedido.Telefono = telefono;
            var carrito = db.tblCarrito.Where(c => c.ID_Pedido == idPedido).ToList();
            pedido.Total = carrito.Sum(c => c.Subtotal);
            pedido.Estado = "Confirmado";

            db.SaveChanges();

            TempData["Mensaje"] = "Pedido confirmado correctamente.";
            return RedirectToAction("Index", "tblProductoes");
        }

        // Eliminar producto del carrito
        [Authorize]
        public ActionResult EliminarDelCarrito(int id)
        {
            var item = db.tblCarrito.Find(id);
            if (item != null)
            {
                db.tblCarrito.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Agregar producto al carrito
        [Authorize]
        public ActionResult AgregarAlCarrito(int idProducto)
        {
            var producto = db.tblProducto.Find(idProducto);
            if (producto == null)
                return HttpNotFound();

            string nombreCliente = User.Identity.Name;
            var pedido = db.tblPedido.FirstOrDefault(p => p.NombreCliente == nombreCliente && p.Estado == "EnProceso");

            if (pedido == null)
            {
                pedido = new tblPedido
                {
                    NombreCliente = nombreCliente,
                    Direccion = "Por confirmar",
                    Telefono = "0000000000",
                    FechaHora = DateTime.Now,
                    Total = 0,
                    Estado = "EnProceso"
                };
                db.tblPedido.Add(pedido);
                db.SaveChanges();
            }

            var nuevoItem = new tblCarrito
            {
                ID_Producto = producto.ID_Producto,
                Cantidad = 1,
                Subtotal = (decimal)producto.Precio,
                ID_Pedido = pedido.ID_Pedido
            };

            db.tblCarrito.Add(nuevoItem);
            db.SaveChanges();

            TempData["Mensaje"] = "Producto agregado al carrito correctamente.";
            return RedirectToAction("Index", "tblCarritoes");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
