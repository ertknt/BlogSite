using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVCBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace MVCBlog.Controllers
{
    public class AdminMakaleController : Controller
    {
        MVCBlogDb db = new MVCBlogDb();

        public ActionResult Index(int Page=1)
        {
            var makale = db.Makale.OrderBy(m =>m.Id).ToPagedList(Page, 4);

            return View(makale);
        }

        public ActionResult Details(int id)
        {

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "Isim");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Makale makale, string etiketler, HttpPostedFileBase Foto)
        {
            try
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);

                    string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newFoto);
                    makale.Foto = "/Uploads/MakaleFoto/" + newFoto;
                }

                if (etiketler != null)
                {
                    string[] etiketDizisi = etiketler.Split(',');
                    foreach (var item in etiketDizisi)
                    {
                        var yeniEtiket = new Etiket { İsim = item };
                        db.Etiket.Add(yeniEtiket);
                        makale.Etiket.Add(yeniEtiket);
                    }
                }

                makale.Okunma = 0;
                makale.UyeId = Convert.ToInt32(Session["Id"]);
                db.Makale.Add(makale);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(makale);
            }
        }

        public ActionResult Edit(int id)
        {
            var makale = db.Makale.Where(m => m.Id == id).SingleOrDefault();

            if (makale == null)
                return HttpNotFound();

            ViewBag.KategoriId = new SelectList(db.Kategori, "Id", "Isim", makale.KategoriId);

            return View(makale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Makale makale)
        {
            try
            {
                var guncellenecekMakale = db.Makale.Where(m => m.Id == id).SingleOrDefault();

                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(guncellenecekMakale.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(guncellenecekMakale.Foto));
                    }

                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);

                    string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newFoto);
                    guncellenecekMakale.Foto = "/Uploads/MakaleFoto/" + newFoto;
                }

                guncellenecekMakale.Baslik = makale.Baslik;
                guncellenecekMakale.Icerik = makale.Icerik;
                guncellenecekMakale.KategoriId = makale.KategoriId;

                db.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var makale = db.Makale.Where(m => m.Id == id).SingleOrDefault();

            if (makale == null)
                return HttpNotFound();

            return View(makale);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var makale = db.Makale.Where(m => m.Id == id).SingleOrDefault();

            try
            {
                
                if (makale == null)
                    return HttpNotFound();

                if (System.IO.File.Exists(Server.MapPath(makale.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(makale.Foto));
                }

                foreach (var item in makale.Yorum.ToList())
                {
                    db.Yorum.Remove(item);
                }

                foreach (var item in makale.Etiket.ToList())
                {
                    db.Etiket.Remove(item);
                }

                db.Makale.Remove(makale);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(makale);
            }
        }
    }
}
