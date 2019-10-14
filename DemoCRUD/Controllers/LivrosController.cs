using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoCRUD.AcessoDados;
using DemoCRUD.Models;

namespace DemoCRUD.Controllers
{
    public class LivrosController : Controller
    {
        private LivroContexto db = new LivroContexto();

        // GET: Livros
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(Livro livro, int current = 1, int rowCount = 10) // pagina e quantidade de registro por pag
        {
            // obterá: sort[Titulo] || sort[Autor] || sort[AnoEdicao] || sort[Valor]
            string chav = Request.Form.AllKeys.Where(k => k.StartsWith("sort")).FirstOrDefault();
            // cariavel que recebe a ordenação do formulario
            string ordenacao = Request[chav];
            string campo = chav.Replace("sort[", string.Empty).Replace("]", string.Empty);
            var livros = db.Livros.Include(l => l.Genero);

            int total = livros.Count();

            // caso o titulo não seja nulo nem contenha valores em branco
            if (!String.IsNullOrWhiteSpace(livro.Titulo))
            {
                // retorna a lista de livros que contenha o titulo passado
                livros = livros.Where(l => l.Titulo.Contains(livro.Titulo));
            }
            if (!String.IsNullOrWhiteSpace(livro.Autor))
            {
                // retorna a lista de livros que contenha o autor informado
                livros = livros.Where(l => l.Autor.Contains(livro.Autor));
            }
            if (livro.AnoEdicao != 0)
            {
                // retorna a lista de livros que contenha o ano informado
                livros = livros.Where(l => l.AnoEdicao.ToString().Contains(livro.AnoEdicao.ToString()));
            }
            if (livro.Valor != decimal.Zero)
            {
                // retorna a lista de livros que contenha o valor informado
                livros = livros.Where(l => l.Valor.ToString().Contains(livro.Valor.ToString()));
            }
            // variavel auxioliar com o campo e a ordenacao
            string campoOrdenacao = string.Format("{0} {1}", campo, ordenacao);
            // usando Linq

            // Skip((pag-1)*regis): pagina - 1 x registros por pagina
            // dessa forma toda a cada pagina pularemos os registros que já estão na pagina anterior
            var livrosPaginados = livros.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount); // utilizando linq.dynamic
            // como foi mudado o nome da view criada é necessário passar como parametro aqui
            // é nessário pasasr o formato correto que o bootgrid pede na documentação
            return Json(new {
                rows = livrosPaginados.ToList(),
                current = current, rowCount = rowCount,
                total=total
            },
                JsonRequestBehavior.AllowGet);
        }

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome");
            return View();
        }

        // POST: Livros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Autor,AnoEdicao,Valor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // POST: Livros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Autor,AnoEdicao,Valor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", livro.GeneroId);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
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
