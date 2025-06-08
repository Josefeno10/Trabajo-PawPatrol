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
    public class AdminController : Controller
    {
        private db_aba1b3_pedidoscomidaEntities db = new db_aba1b3_pedidoscomidaEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.tblPedido.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPedido tblPedido = db.tblPedido.Find(id);
            if (tblPedido == null)
            {
                return HttpNotFound();
            }
            return View(tblPedido);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Pedido,NombreCliente,Direccion,Telefono,FechaHora,Total,Estado")] tblPedido tblPedido)

        {
            if (ModelState.IsValid)
            {
                db.tblPedido.Add(tblPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblPedido);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPedido tblPedido = db.tblPedido.Find(id);
            if (tblPedido == null)
            {
                return HttpNotFound();
            }
            return View(tblPedido);
        }

        // POST: Admin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Pedido,NombreCliente,Direccion,Telefono,FechaHora,Total,Estado")] tblPedido tblPedido)

        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblPedido);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPedido tblPedido = db.tblPedido.Find(id);
            if (tblPedido == null)
            {
                return HttpNotFound();
            }
            return View(tblPedido);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPedido tblPedido = db.tblPedido.Find(id);
            db.tblPedido.Remove(tblPedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CambiarEstado(int id)
        {
            var pedido = db.tblPedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }

            pedido.Estado = "Entregado";
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
