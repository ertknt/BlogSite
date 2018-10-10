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
        private MVCBlogDb db = new MVCBlogDb();


        public ActionResult Index(int Page=1)
        {
            var yorumlar = db.Yorum.OrderByDescending(y => y.Id).ToPagedList(Page, 4);

            return View(yorumlar);
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
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
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "Id", "Baslik", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(db.Uye, "Id", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Icerik,UyeId,MakaleId,Tarih")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "Id", "Baslik", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(db.Uye, "Id", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }

        // GET: AdminYorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
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
            Yorum yorum = db.Yorum.Find(id);
            db.Yorum.Remove(yorum);
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
