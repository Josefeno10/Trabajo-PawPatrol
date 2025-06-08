using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidosComida.Models;

namespace PedidosComida.Controllers
{
    public class tblCarritoesController : Controller
    {
        private db_aba1b3_pedidoscomidaEntities db = new db_aba1b3_pedidoscomidaEntities();

        // GET: tblCarritoes
        public ActionResult Index()
        {
            var tblCarrito = db.tblCarrito.Include(t => t.tblPedido).Include(t => t.tblProducto);
            return View(tblCarrito.ToList());
        }

        // GET: tblCarritoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrito tblCarrito = db.tblCarrito.Find(id);
            if (tblCarrito == null)
            {
                return HttpNotFound();
            }
            return View(tblCarrito);
        }

        // GET: tblCarritoes/Create
        public ActionResult Create()
        {
            ViewBag.ID_Pedido = new SelectList(db.tblPedido, "ID_Pedido", "NombreCliente");
            ViewBag.ID_Producto = new SelectList(db.tblProducto, "ID_Producto", "Nombre");
            return View();
        }

        // POST: tblCarritoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Detalle,ID_Pedido,ID_Producto,Cantidad,Subtotal")] tblCarrito tblCarrito)
        {
            if (ModelState.IsValid)
            {
                db.tblCarrito.Add(tblCarrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Pedido = new SelectList(db.tblPedido, "ID_Pedido", "NombreCliente", tblCarrito.ID_Pedido);
            ViewBag.ID_Producto = new SelectList(db.tblProducto, "ID_Producto", "Nombre", tblCarrito.ID_Producto);
            return View(tblCarrito);
        }

        // GET: tblCarritoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrito tblCarrito = db.tblCarrito.Find(id);
            if (tblCarrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Pedido = new SelectList(db.tblPedido, "ID_Pedido", "NombreCliente", tblCarrito.ID_Pedido);
            ViewBag.ID_Producto = new SelectList(db.tblProducto, "ID_Producto", "Nombre", tblCarrito.ID_Producto);
            return View(tblCarrito);
        }

        // POST: tblCarritoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Detalle,ID_Pedido,ID_Producto,Cantidad,Subtotal")] tblCarrito tblCarrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCarrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Pedido = new SelectList(db.tblPedido, "ID_Pedido", "NombreCliente", tblCarrito.ID_Pedido);
            ViewBag.ID_Producto = new SelectList(db.tblProducto, "ID_Producto", "Nombre", tblCarrito.ID_Producto);
            return View(tblCarrito);
        }

        // GET: tblCarritoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrito tblCarrito = db.tblCarrito.Find(id);
            if (tblCarrito == null)
            {
                return HttpNotFound();
            }
            return View(tblCarrito);
        }

        // POST: tblCarritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCarrito tblCarrito = db.tblCarrito.Find(id);
            db.tblCarrito.Remove(tblCarrito);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
