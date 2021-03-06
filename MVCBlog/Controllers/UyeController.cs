﻿using MVCBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCBlog.Controllers
{
    public class UyeController : Controller
    {
        private MVCBlogDb _context;

        public UyeController()
        {
            _context = new MVCBlogDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(int id)
        {
            var uye = _context.Uye.Where(u => u.Id == id).SingleOrDefault();

            if (Convert.ToInt32(Session["Id"]) != uye.Id)
            {
                return HttpNotFound();
            }

            return View(uye);

        }

        public ActionResult UyeProfil(int id)
        {
            var uye = _context.Uye.Where(u => u.Id == id).SingleOrDefault();


            return View(uye);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Uye uye, HttpPostedFileBase Foto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            else
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);

                    string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newFoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newFoto;
                }

                uye.YetkiId = 2;
                _context.Uye.Add(uye);
                _context.SaveChanges();

                Session["Id"] = uye.Id;
                Session["KullaniciAdi"] = uye.KullaniciAdi;



                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            var uye = _context.Uye.Where(u => u.Id == id).SingleOrDefault();

            if (Convert.ToInt32(Session["Id"]) != uye.Id)
                return HttpNotFound();

            return View(uye);
        }

        [HttpPost]
        public ActionResult Edit(int id, Uye uye, HttpPostedFileBase Foto)
        {
            var guncellenecekUye = _context.Uye.Where(u => u.Id == id).SingleOrDefault();

            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {


                if (Foto != null)
                {


                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(Foto.FileName);

                    string newFoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newFoto);
                    guncellenecekUye.Foto = "/Uploads/UyeFoto/" + newFoto;
                }

                guncellenecekUye.AdSoyad = uye.AdSoyad;
                guncellenecekUye.Email = uye.Email;
                guncellenecekUye.Sifre = uye.Sifre;
                guncellenecekUye.KullaniciAdi = uye.KullaniciAdi;

                _context.SaveChanges();

                Session["KullaniciAdi"] = uye.KullaniciAdi;

                return RedirectToAction("Index", "Home", new { id = guncellenecekUye.Id });
            }

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Uye uye)
        {
            var login = _context.Uye.Where(u => u.KullaniciAdi == uye.KullaniciAdi).SingleOrDefault();

            if (login.KullaniciAdi == uye.KullaniciAdi && login.Email == uye.Email && login.Sifre == uye.Sifre)
            {
                Session["Id"] = login.Id;
                Session["KullaniciAdi"] = login.KullaniciAdi;
                Session["YetkiId"] = login.YetkiId;
                ViewBag.Kullanici = login.AdSoyad;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Uyari = "Kullanıcı Adı, mail veya şifrenizi kontrol ediniz.";

                return View();
            }

        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}