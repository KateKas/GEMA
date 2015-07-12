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
    public class JornalistasController : Controller
    {
        private Dao db = new Dao();

        // GET: Jornalistas
        public ActionResult Index()
        {
            return View(db.Jornalistas.ToList());
        }

        // GET: Jornalistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornalistas jornalistas = (Jornalistas)db.Jornalistas.Find(id);
            if (jornalistas == null)
            {
                return HttpNotFound();
            }
            return View(jornalistas);
        }

        // GET: Jornalistas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jornalistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] Jornalistas jornalistas)
        {
            if (ModelState.IsValid)
            {
                db.Jornalistas.Add(jornalistas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jornalistas);
        }

        // GET: Jornalistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornalistas jornalistas = (Jornalistas)db.Jornalistas.Find(id);
            if (jornalistas == null)
            {
                return HttpNotFound();
            }
            return View(jornalistas);
        }

        // POST: Jornalistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] Jornalistas jornalistas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jornalistas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jornalistas);
        }

        // GET: Jornalistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornalistas jornalistas = (Jornalistas)db.Jornalistas.Find(id);
            if (jornalistas == null)
            {
                return HttpNotFound();
            }
            return View(jornalistas);
        }

        // POST: Jornalistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jornalistas jornalistas = (Jornalistas)db.Jornalistas.Find(id);
            db.Pessoas.Remove(jornalistas);
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
