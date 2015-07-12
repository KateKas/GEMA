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
    public class SecoesController : Controller
    {
        private Dao db = new Dao();

        // GET: Secoes
        public ActionResult Index()
        {
            return View(db.Secoes.ToList());
        }

        // GET: Secoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secoes secoes = db.Secoes.Find(id);
            if (secoes == null)
            {
                return HttpNotFound();
            }
            return View(secoes);
        }

        // GET: Secoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,Secao")] Secoes secoes)
        {
            if (ModelState.IsValid)
            {
                db.Secoes.Add(secoes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secoes);
        }

        // GET: Secoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secoes secoes = db.Secoes.Find(id);
            if (secoes == null)
            {
                return HttpNotFound();
            }
            return View(secoes);
        }

        // POST: Secoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Secao")] Secoes secoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secoes);
        }

        // GET: Secoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secoes secoes = db.Secoes.Find(id);
            if (secoes == null)
            {
                return HttpNotFound();
            }
            return View(secoes);
        }

        // POST: Secoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Secoes secoes = db.Secoes.Find(id);
            db.Secoes.Remove(secoes);
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
