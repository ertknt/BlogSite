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
        private MVCBlogDb _context;

        public HomeController()
        {
            _context = new MVCBlogDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Home
        public ActionResult Index(int Page = 1)
        {
            var makaleler = _context.Makale.OrderByDescending(m => m.Id).ToPagedList(Page, 4);

            return View(makaleler);
        }

        public ActionResult KategoriMakale(int id)
        {
            var makaleler = _context.Makale.Where(m => m.KategoriId == id).ToList();

            if (makaleler == null)
                return HttpNotFound();


            return View(makaleler);
        }

        public ActionResult MakaleDetay(int id)
        {
            var makale = _context.Makale.Where(m => m.Id == id).SingleOrDefault();

            if (makale == null)
                return HttpNotFound();



            return View(makale);
        }

        public ActionResult KategoriPartial()
        {
            var kategori = _context.Kategori.ToList();

            return View(kategori);
        }

        public ActionResult BlogArama(string aranan = null)
        {
            var arananMakale = _context.Makale.Where(m => m.Baslik.Contains(aranan)).OrderByDescending(m =>m.Id).ToList();

            return View(arananMakale);
        }

        public ActionResult Son5Yorum()
        {
            var yorumlar = _context.Yorum.OrderByDescending(y => y.Id).Take(5);

            return View(yorumlar);
        }

        public ActionResult PopulerMakaleler()
        {
            var makaleler = _context.Makale.OrderByDescending(m => m.Okunma).ToList();

            return View(makaleler);
        }

        public JsonResult YorumYap(string yorum, int makaleId)
        {
            var UyeId = Session["Id"];

            if (yorum != null)
            {
                _context.Yorum.Add(new Yorum
                {
                    UyeId = Convert.ToInt32(UyeId),
                    MakaleId = makaleId,
                    Icerik = yorum,
                    Tarih = DateTime.Now
                });

                _context.SaveChanges();
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var UyeId = Session["Id"];

            var yorum = _context.Yorum.Where(y => y.Id == id).SingleOrDefault();
            var makale = _context.Makale.Where(m => m.Id == yorum.MakaleId).SingleOrDefault();

            if (yorum.UyeId == Convert.ToInt32(UyeId))
            {
                _context.Yorum.Remove(yorum);
                _context.SaveChanges();
                return RedirectToAction("MakaleDetay", "Home", new { id = makale.Id });

            }

            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult OkunmaArtir(int MakaleId)
        {
            var makale = _context.Makale.Where(m => m.Id == MakaleId).SingleOrDefault();

            makale.Okunma += 1;
            _context.SaveChanges();

            return View();
        }
    }
}