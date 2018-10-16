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
    public class AdminYorumController : Controller
    {
        private MVCBlogDb _context;

        public AdminYorumController()
        {
            _context = new MVCBlogDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index(int Page=1)
        {
            var yorumlar = _context.Yorum.OrderByDescending(y => y.Id).ToPagedList(Page, 4);

            return View(yorumlar);
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = _context.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = _context.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakaleId = new SelectList(_context.Makale, "Id", "Baslik", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(_context.Uye, "Id", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Icerik,UyeId,MakaleId,Tarih")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(yorum).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakaleId = new SelectList(_context.Makale, "Id", "Baslik", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(_context.Uye, "Id", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }

        // GET: AdminYorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = _context.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorum yorum = _context.Yorum.Find(id);
            _context.Yorum.Remove(yorum);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
