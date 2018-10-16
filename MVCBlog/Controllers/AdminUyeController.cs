using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;
using PagedList;

namespace MVCBlog.Controllers
{
    public class AdminUyeController : Controller
    {
        private MVCBlogDb _context;

        public AdminUyeController()
        {
            _context = new MVCBlogDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: AdminUye
        public ActionResult Index(int Page=1)
        {
            var uyeler = _context.Uye.OrderByDescending(u => u.Id).ToPagedList(Page,4);

            return View(uyeler);
        }

        // GET: AdminUye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Uye uye = _context.Uye.Find(id);

            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: AdminUye/Create
        public ActionResult Create()
        {
            ViewBag.YetkiId = new SelectList(_context.Yetki, "Id", "Yetki1");
            return View();
        }

        // POST: AdminUye/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KullaniciAdi,Email,Sifre,AdSoyad,Foto,YetkiId")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                _context.Uye.Add(uye);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiId = new SelectList(_context.Yetki, "Id", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // GET: AdminUye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = _context.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiId = new SelectList(_context.Yetki, "Id", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // POST: AdminUye/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KullaniciAdi,Email,Sifre,AdSoyad,Foto,YetkiId")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(uye).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiId = new SelectList(_context.Yetki, "Id", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // GET: AdminUye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = _context.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: AdminUye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = _context.Uye.Find(id);
            _context.Uye.Remove(uye);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
