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
using GEMA.FMK;
using GEMA.FMK.Security;

namespace GEMA.Controllers
{
    [Authorize]
    public class MateriasController : Controller
    {
        private Dao db = new Dao();        

        // GET: Materias
        public ActionResult Index()
        {
            string papel = db.Pessoas.Where(w => w.Nome == User.Identity.Name).Select(s => s.Papeis.Papel).First();
            ViewBag.Papel = papel;
            
            switch (papel)
            {
                case "Jornalistas":
                    return View(db.Materias.Where(w => w.Jornalistas.Nome == User.Identity.Name).OrderByDescending(o => o.DataMateria).ToList());

                case "Gerentes":
                    return View(db.Materias.Where(w => w.Gerentes.Nome == User.Identity.Name).OrderByDescending(o => o.DataMateria).ToList());

                case "Revisores":
                    return View(db.Materias.Where(w => w.Condicao == 1 || w.Condicao == 2).OrderByDescending(o => o.DataMateria).ToList());

                case "Publicadores":
                    return View(db.Materias.Where(w => w.Condicao == 3).OrderByDescending(o => o.DataMateria).ToList());

                default:
                    return View();
            }           
        }

        [HttpPost]
        public ActionResult Index(string FiltroPeriodo, string FiltroRecente)
        {
            string papel = db.Pessoas.Where(w => w.Nome == User.Identity.Name).Select(s => s.Papeis.Papel).First();
            ViewBag.Papel = papel;

            DateTime periodo = DateTime.Today.AddYears(-10);
            int recentes = 100;

            if (!String.IsNullOrEmpty(FiltroPeriodo))
                periodo =  DateTime.Today.AddDays(-int.Parse(FiltroPeriodo));

            if (!String.IsNullOrEmpty(FiltroRecente))
                recentes = int.Parse(FiltroRecente);

            switch (papel)
            {
                case "Jornalistas":
                    return View(db.Materias.Where(w => w.Jornalistas.Nome == User.Identity.Name && w.DataMateria >= periodo).OrderByDescending(o => o.DataMateria).Take(recentes).ToList());

                case "Gerentes":
                    return View(db.Materias.Where(w => w.Gerentes.Nome == User.Identity.Name && w.DataMateria >= periodo).OrderByDescending(o => o.DataMateria).Take(recentes).ToList());

                case "Revisores":
                    return View(db.Materias.Where(w => w.Condicao == 1 || w.Condicao == 2 && w.DataMateria >= periodo).OrderByDescending(o => o.DataMateria).Take(recentes).ToList());

                case "Publicadores":
                    return View(db.Materias.Where(w => w.Condicao == 3 && w.DataMateria >= periodo).OrderByDescending(o => o.DataMateria).Take(recentes).ToList());

                default:
                    return View();
            }         
        }

        // GET: Materias/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Papel = db.Pessoas.Where(w => w.Nome == User.Identity.Name).Select(s => s.Papeis.Papel).First();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materias materias = db.Materias.Find(id);
            if (materias == null)
            {
                return HttpNotFound();
            }
            return View(materias);
        }

        // GET: Materias/Create   
        [PerfilFiltro(Roles = "Jornalistas")]
        public ActionResult Create()
        {
            ViewBag.IdSecao = new SelectList(db.Secoes, "Id", "Secao");
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [PerfilFiltro(Roles = "Jornalistas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Materia")] Materias materias)
        {
            if (ModelState.IsValid && IsValid(materias))
            {
                materias.DataMateria = DateTime.Now;
                materias.Condicao = (int)Condicao.Proposta;
                materias.Jornalistas = db.Jornalistas.Where(w => w.Nome == User.Identity.Name).First();

                //Pega a seção
                Secoes Secao = db.Secoes.Find(int.Parse(Request["IdSecao"]));

                materias.Secoes = Secao;
                materias.Gerentes = Secao.Gerentes;
                //Roles.IsUserInRole("Jornalista")

                db.Materias.Add(materias);

                //Cria o evento
                Eventos evento = new Eventos { DataEvento = DateTime.Now, Evento = "Envio incial", Materias = materias, Pessoas = materias.Jornalistas };
                db.Eventos.Add(evento);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSecao = new SelectList(db.Secoes, "Id", "Secao");
            return View(materias);
        }

        // GET: Materias/Edit/5
        [PerfilFiltro(Roles = "Jornalistas, Revisores, Publicadores")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materias materias = db.Materias.Find(id);
            if (materias == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdSecao = new SelectList(db.Secoes, "Id", "Secao", materias.Secoes.Id);
            return View(materias);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [PerfilFiltro(Roles = "Jornalistas, Revisores, Publicadores")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,DataMateria,Materia,Condicao")] Materias materias, string btnSalvar)
        {
            if (ModelState.IsValid)
            {
                string papel = db.Pessoas.Where(w => w.Nome == User.Identity.Name).Select(s => s.Papeis.Papel).First();

                //Criar o evento
                Eventos evento = new Eventos { DataEvento = DateTime.Now,  Materias = materias,
                                               Pessoas = db.Pessoas.Where(w => w.Nome == User.Identity.Name).First() };

                if (papel == "Revisores")
                {
                    materias.Revisores = db.Revisores.Where(w => w.Nome == User.Identity.Name).First();
                }
                else if (papel == "Publicadores")
                {
                    materias.Publicadores = db.Publicadores.Where(w => w.Nome == User.Identity.Name).First();
                }

                if (btnSalvar == "Aprovar")
                {
                    materias.Condicao = 3;
                    evento.Evento = "Aprovação";
                }
                else if (btnSalvar == "Arquivar")
                {
                    materias.Condicao = 4;
                    evento.Evento = "Arquivamento";
                }
                else if (btnSalvar == "Publicar")
                {
                    materias.Condicao = 5;
                    evento.Evento = "Publicação";
                }
                else
                {
                    materias.Condicao = 2;
                    evento.Evento = "Revisão";
                }

                db.Entry(materias).State = EntityState.Modified;
               
                db.Eventos.Add(evento);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSecao = new SelectList(db.Secoes, "Id", "Secao", materias.Secoes.Id);
            return View(materias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsValid(Materias materia)
        {
            if (string.IsNullOrEmpty(Request["IdSecao"]))
            {
                ModelState.AddModelError("", "O campo seção é obrigatório.");
                return false;
            }

            return true;
        }

        //private static string CondicaoStatus(Condicao Status)
        //{
        //    // See http://go.microsoft.com/fwlink/?LinkID=177550 for
        //    // a full list of status codes.
        //    switch (Status)
        //    {
        //        case Condicao.Aprovada:
        //            return "Aprovada";

        //        case Condicao.Arquivada:
        //            return "Arquivada";

        //        case Condicao.EmRevisao:
        //            return "Em revisão";

        //        case Condicao.Proposta:
        //            return "Proposta";

        //        case Condicao.Publicada:
        //            return "Publicada";

        //        default:
        //            return "NoStatus";
        //    }
        //}
    }
}
