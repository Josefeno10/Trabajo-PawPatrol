using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblProducto producto = db.tblProducto.Find(id);
            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        // GET: tblProductoes/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblProductoes/Create
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(tblProducto tblProducto)
        {
            // Procesar imagen si fue subida
            if (tblProducto.ImagenArchivo != null && tblProducto.ImagenArchivo.ContentLength > 0)
            {
                // Generar nombre único para la imagen
                var fileName = Path.GetFileNameWithoutExtension(tblProducto.ImagenArchivo.FileName);
                var extension = Path.GetExtension(tblProducto.ImagenArchivo.FileName);
                var uniqueName = fileName + "_" + Guid.NewGuid().ToString().Substring(0, 6) + extension;

                // Ruta completa en servidor
                var path = Path.Combine(Server.MapPath("~/Images/"), uniqueName);

                // Guardar imagen en carpeta /Images
                tblProducto.ImagenArchivo.SaveAs(path);

                // Guardar la ruta relativa en base de datos
                tblProducto.ImagenURL = "/Images/" + uniqueName;
            }

            if (ModelState.IsValid)
            {
                db.tblProducto.Add(tblProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProducto);
        }

        // GET: tblProductoes/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblProducto producto = db.tblProducto.Find(id);
            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        // POST: tblProductoes/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblProducto tblProducto)
        {
            // Procesar nueva imagen si fue subida
            if (tblProducto.ImagenArchivo != null && tblProducto.ImagenArchivo.ContentLength > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(tblProducto.ImagenArchivo.FileName);
                var extension = Path.GetExtension(tblProducto.ImagenArchivo.FileName);
                var uniqueName = fileName + "_" + Guid.NewGuid().ToString().Substring(0, 6) + extension;

                var path = Path.Combine(Server.MapPath("~/Images/"), uniqueName);
                tblProducto.ImagenArchivo.SaveAs(path);
                tblProducto.ImagenURL = "/Images/" + uniqueName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(tblProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProducto);
        }

        // GET: tblProductoes/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblProducto producto = db.tblProducto.Find(id);
            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        // POST: tblProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProducto producto = db.tblProducto.Find(id);
            db.tblProducto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
