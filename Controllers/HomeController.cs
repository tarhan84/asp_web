using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSense.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var db = new SmartSenseDB();
            var bilgiler = db.Iletisims.Where(i => i.Adres != null).FirstOrDefault();
            var h1 = db.Contents.Where(i => i.Baslik == "b1").FirstOrDefault();
            var h2 = db.Contents.Where(i => i.Baslik == "b2").FirstOrDefault();
            var hakkimizda = db.Contents.Where(i => i.Baslik == "hakkimizda").FirstOrDefault();
            ViewBag.adres = bilgiler.Adres;
            ViewBag.telefon = bilgiler.Telefon;
            ViewBag.eposta = bilgiler.Eposta;
            ViewBag.b1 = h1.Icerik;
            ViewBag.b2 = h2.Icerik;
            ViewBag.hakkimizda = hakkimizda.Icerik;
            return View();
        }

        public IActionResult Iletisim()
        {
            var db = new SmartSenseDB();
            var bilgiler = db.Iletisims.Where(i=>i.Adres!=null).FirstOrDefault();
            ViewBag.adres = bilgiler.Adres;
            ViewBag.telefon = bilgiler.Telefon;
            ViewBag.eposta = bilgiler.Eposta;
            return View();
        }

        [HttpPost]
        public IActionResult Index(MesajModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new SmartSenseDB();
                var message = new Mesajlar();
                message.Email = model.Email;
                message.Konu = model.Konu;
                message.Mesaj = model.Mesaj;
                message.Tarih = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                db.Mesajlars.Add(message);
                db.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
