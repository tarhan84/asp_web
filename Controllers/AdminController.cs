using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using SmartSense;

namespace SmartSense.Controllers
{
    [Authorize (Roles = "root")]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            var db = new SmartSenseDB();
            var h1 = db.Contents.Where(i => i.Baslik == "b1").FirstOrDefault();
            var h2 = db.Contents.Where(i => i.Baslik == "b2").FirstOrDefault();
            var hakkimizda = db.Contents.Where(i => i.Baslik == "hakkimizda").FirstOrDefault();
            ViewBag.b1 = h1.Icerik;
            ViewBag.b2 = h2.Icerik;
            ViewBag.hakkimizda = hakkimizda.Icerik;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string b1,string b2, string hakkimizda)
        {
            var db = new SmartSenseDB();
            var h1 = db.Contents.Where(i => i.Baslik == "b1").FirstOrDefault();
            var h2 = db.Contents.Where(i => i.Baslik == "b2").FirstOrDefault();
            var hakkimizda1 = db.Contents.Where(i => i.Baslik == "hakkimizda").FirstOrDefault();
            if (b1 != null && b2 != null && hakkimizda != null)
            {
                h1.Icerik = b1;
                h2.Icerik = b2;
                hakkimizda1.Icerik = hakkimizda;
                db.SaveChanges();
            }
            ViewBag.b1 = h1.Icerik;
            ViewBag.b2 = h2.Icerik;
            ViewBag.hakkimizda = hakkimizda1.Icerik;
            return View();
        }

        public IActionResult Iletisim()
        {
            var db = new SmartSenseDB();
            var bilgiler = db.Iletisims.Where(i => i.Adres != null).FirstOrDefault();
            return View(bilgiler);
        }

        [HttpPost]
        public IActionResult Iletisim(Iletisim model)
        {
            var db = new SmartSenseDB();
            var bilgiler = db.Iletisims.Where(i => i.Adres != null).FirstOrDefault();
            bilgiler.Adres = model.Adres;
            bilgiler.Telefon = model.Telefon;
            bilgiler.Eposta = model.Eposta;
            
            db.SaveChanges();
            return View(model);
        }

        public IActionResult Mesajlar()
        {
            var db = new SmartSenseDB();
            var mesajlar = db.Mesajlars.OrderBy(i=>i.Okundu);
            var unread = db.Mesajlars.Where(i => i.Okundu == 0).ToList();
            int count = unread.Count();
            if (mesajlar != null)
            {
                ViewBag.count = count;
                ViewBag.mesajlar = mesajlar;
            }
            return View();
        }

        public IActionResult mesajSil(int id)
        {
            var db = new SmartSenseDB();
            var result = db.Mesajlars.Where(i => i.Id == id).FirstOrDefault();
            if (result != null)
            {
                db.Mesajlars.Remove(result);
                db.SaveChanges();
            }
            return RedirectToAction("Mesajlar", "Admin");
        }

        public IActionResult mesajOku(int id)
        {
            var db = new SmartSenseDB();
            var mesaj = db.Mesajlars.Where(i => i.Id == id).FirstOrDefault();
            if (mesaj != null)
            {
                mesaj.Okundu = 1;
            }
            ViewBag.mesaj = mesaj.Mesaj;
            ViewBag.konu = mesaj.Konu;
            ViewBag.gonderen = mesaj.Email;
            ViewBag.tarih = mesaj.Tarih;
            db.SaveChanges();
            return View();
        }

        public IActionResult mesajGonder(string eposta,string mesaj,string konu)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("smartsenseinfo21@gmail.com", "asdf2121");
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("smartsenseinfo21@gmail.com", "SMARTSENSE");
            ePosta.To.Add(eposta);
            ePosta.IsBodyHtml = true;
            ePosta.Body = mesaj;
            ePosta.Subject = konu;
            smtp.Send(ePosta);
            ViewBag.mesajj = "mesajınız başarıyla iletildi";
            return RedirectToAction("mesajlar", "Admin");
        }
    }
}
