using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class AdminKategoriController : Controller
    {
        private MVCBlogDb _context;

        public AdminKategoriController()
        {
            _context = new MVCBlogDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: AdminKategori
        public ActionResult Index()
        {
            return View(_context.Kategori.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = _context.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Isim")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Kategori.Add(kategori);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategori);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = _context.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Isim")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(kategori).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = _context.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = _context.Kategori.Find(id);
            _context.Kategori.Remove(kategori);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
