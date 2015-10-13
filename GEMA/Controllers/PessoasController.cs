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
using System.Web.Security;
using WebMatrix.WebData;

namespace GEMA.Controllers
{
     [Authorize]
    public class PessoasController : Controller
    {
        private Dao db = new Dao();

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            ViewBag.IdPapel = new SelectList(db.Papeis, "Id", "Papel");
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] Pessoas pessoas, string Senha)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Pega o papel
                    Papeis papel = db.Papeis.Find(int.Parse(Request["IdPapel"]));

                    switch (papel.Papel)
                    {
                        case "Jornalistas":
                            Jornalistas jornalista = new Jornalistas { Nome = pessoas.Nome, Papeis = papel };
                            db.Jornalistas.Add(jornalista);
                            break;
                        case "Gerentes":
                            Gerentes gerentes = new Gerentes { Nome = pessoas.Nome, Papeis = papel };
                            db.Gerentes.Add(gerentes);
                            break;
                        case "Revisores":
                            Revisores revisores = new Revisores { Nome = pessoas.Nome, Papeis = papel };
                            db.Revisores.Add(revisores);
                            break;
                        case "Publicadores":
                            Publicadores publicadores = new Publicadores { Nome = pessoas.Nome, Papeis = papel };
                            db.Publicadores.Add(publicadores);
                            break;
                    }

                    WebSecurity.CreateUserAndAccount(pessoas.Nome, Senha);
                    db.SaveChanges();
                    return RedirectToAction("Index");                   
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", AccountController.ErrorCodeToString(e.StatusCode));
                }
            }

            ViewBag.IdPapel = new SelectList(db.Papeis, "Id", "Papel");
            return View(pessoas);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pessoas);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }
            return View(pessoas);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoas pessoas = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoas);
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
