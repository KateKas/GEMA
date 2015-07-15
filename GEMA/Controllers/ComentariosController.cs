using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GEMA.DAO.Contexto;
using GEMA.Models;

namespace GEMA.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private Dao db = new Dao();

        // GET: Comentarios
        public ActionResult Index()
        {
            return View(db.Comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comentario")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                comentarios.DataComentario = DateTime.Now;
                comentarios.Pessoas = db.Pessoas.Where(w => w.Nome == User.Identity.Name).First();

                db.Comentarios.Add(comentarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comentarios);
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comentario,DataComentario")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comentarios);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentarios);
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

        public ActionResult Comentarios(int Id)
        {
            return PartialView(db.Comentarios.Where(w => w.Materias.Id == Id).OrderByDescending(o=> o.DataComentario).ToList());
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Comentarios([Bind(Include = "Comentario")] Comentarios comentarios, int Id)
        {
            //Comentarios comentarios = new Models.Comentarios ();
            Materias materias = db.Materias.Find(Id);

            if (ModelState.IsValid)
            {                
                comentarios.DataComentario = DateTime.Now;
                comentarios.Pessoas = db.Pessoas.Where(w => w.Nome == User.Identity.Name).First();
                comentarios.Materias = materias;
                
                db.Comentarios.Add(comentarios);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return RedirectToAction("Details", "Materias", new { id = Id });
        }

    }
}
