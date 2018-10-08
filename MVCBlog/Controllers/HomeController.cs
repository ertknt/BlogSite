using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace MVCBlog.Controllers
{
    public class HomeController : Controller
    {
        MVCBlogDb db = new MVCBlogDb();

        // GET: Home
        public ActionResult Index(int Page=1)
        {
            var makaleler = db.Makale.OrderByDescending(m => m.Id).ToPagedList(Page, 4);

            return View(makaleler);
        }

        public ActionResult KategoriMakale(int id)
        {
            var makaleler = db.Makale.Where(m => m.KategoriId == id).ToList();

            if (makaleler == null)
                return HttpNotFound();


            return View(makaleler);
        }

        public ActionResult MakaleDetay(int id)
        {
            var makale = db.Makale.Where(m => m.Id == id).SingleOrDefault();

            if (makale == null)
                return HttpNotFound();



            return View(makale);
        }

        public ActionResult KategoriPartial()
        {
            var kategori = db.Kategori.ToList();

            return View(kategori);
        }

        public JsonResult YorumYap(string yorum, int makaleId)
        {
            var UyeId = Session["Id"];

            if (yorum != null)
            {
                db.Yorum.Add(new Yorum
                {
                    UyeId = Convert.ToInt32(UyeId),
                    MakaleId = makaleId,
                    Icerik = yorum,
                    Tarih = DateTime.Now
                });

                db.SaveChanges();
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var UyeId = Session["Id"];

            var yorum = db.Yorum.Where(y => y.Id == id).SingleOrDefault();
            var makale = db.Makale.Where(m => m.Id == yorum.MakaleId).SingleOrDefault();

            if (yorum.UyeId == Convert.ToInt32(UyeId))
            {
                db.Yorum.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay", "Home", new { id = makale.Id });

            }

            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult OkunmaArtir(int MakaleId)
        {
            var makale = db.Makale.Where(m => m.Id == MakaleId).SingleOrDefault();

            makale.Okunma += 1;
            db.SaveChanges();

            return View();
        }
    }
}