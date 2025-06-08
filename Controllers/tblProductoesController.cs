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
    public class tblProductoesController : Controller
    {
        private db_aba1b3_pedidoscomidaEntities db = new db_aba1b3_pedidoscomidaEntities();

        // GET: tblProductoes
        public ActionResult Index()
        {
            return View(db.tblProducto.ToList());
        }

        // GET: tblProductoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // GET: tblProductoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblProductoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Producto,Nombre,Precio,Categoria,ImagenURL")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                db.tblProducto.Add(tblProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProducto);
        }

        // GET: tblProductoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // POST: tblProductoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Producto,Nombre,Precio,Categoria,ImagenURL")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProducto);
        }

        // GET: tblProductoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // POST: tblProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProducto tblProducto = db.tblProducto.Find(id);
            db.tblProducto.Remove(tblProducto);
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
