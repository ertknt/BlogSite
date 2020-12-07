using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class AdminController : Controller
    {
        MVCBlogDb db = new MVCBlogDb();

        //test commit
        public ActionResult Index()
        {
            ViewBag.MakaleSayisi = db.Makale.Count();
            ViewBag.YorumSayisi = db.Yorum.Count();
            ViewBag.KategoriSayisi = db.Kategori.Count();
            ViewBag.UyeSayisi = db.Uye.Count();

            return View();
        }
    }
}