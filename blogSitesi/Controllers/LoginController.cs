using blogSitesi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace blogSitesi.Controllers
{
    public class LoginController : Controller
    {
        private readonly BlogContext _db;
        private readonly IWebHostEnvironment _env;
        Yazar yazar = new Yazar();
        Guid uid = new Guid();
        public LoginController(BlogContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            yazar = new Yazar();

            return View();
        }
        [HttpPost]
        public IActionResult Index(GirisYapVm vm)
        {
            var userBilgileri = _db.Yazarlar.Where(x => x.Email == vm.Email).FirstOrDefault();
            if (userBilgileri == null)
            {
                if (ModelState.IsValid)
                {
                    yazar.YazarId = Guid.NewGuid();
                    yazar.Email = vm.Email;
                    uid = yazar.YazarId;
                    _db.Add(yazar);
                    _db.SaveChanges();
                    mailAtmaIslemi();
                    return View("UyeAktivasyon");
                }
            }
            else
            {
                yazar.Email = userBilgileri.Email;
                yazar.YazarId = userBilgileri.YazarId;
                uid = userBilgileri.YazarId;
                mailAtmaIslemi();
                return View("UyeAktivasyon");
            }
            return View();
        }

        //private string resimKaydet(IFormFile resim)
        //{
        //    string resimAdi = Guid.NewGuid() + PathString.GetExtension(resim.FileName);
        //    string kaydetmeYolu = PathString.Combine(_env.WebRootPath, "images", resimAdi);

        //    using (FileStream fs = new FileStream(kaydetmeYolu, FileMode.Create))
        //    {
        //        resim.CopyTo(fs);
        //    }

        //    return resimAdi;
        //}

        public IActionResult UyeAktivasyon()
        {
            //email adresine aktivasyon maili yollayacağız.
            return View();
        }
        //public IActionResult UyeGirisiEmailBox(Guid userid)
        //{
        //    userid = uid;
        //    var kullanici = _db.Yazarlar.FirstOrDefault(a => a.YazarId == userid);
        //    return View();
        //}
        public IActionResult UyeGirisi(Guid userid)
        {
            Yazar yazar = _db.Yazarlar.FirstOrDefault(a=>a.YazarId==userid);
            UyeGirisiViewModel vm = new UyeGirisiViewModel();
            if (yazar != null)
            {
                vm.YazarId = yazar.YazarId;
                vm.Email = yazar.Email;
                vm.Ad = yazar.Ad;
                vm.Soyad = yazar.Soyad;
                vm.AktifMi = true;
                vm.ProfilFotoYolu = yazar.ProfilFotoYolu;
                vm.IsEmailVerificated = yazar.IsEmailVerificated;
                vm.Bio = yazar.Bio;
                vm.KullaniciAdi = yazar.Email.Split('@')[0];
                vm.IlkLogin = yazar.IlkLogin;
                HttpContext.Session.SetString("KullaniciEmail", vm.Email);
                return View(vm);
            }
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult UyeGirisi(UyeGirisiViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var yazarBilgileri = _db.Yazarlar.Find(vm.YazarId);
                yazarBilgileri.Ad = vm.Ad;
                yazarBilgileri.Soyad = vm.Soyad;
                yazarBilgileri.KullaniciAdi = vm.KullaniciAdi;
                yazarBilgileri.AktifMi = true;
                yazarBilgileri.IlkLogin = false;
                yazarBilgileri.Bio = vm.Bio;
                yazarBilgileri.Email = vm.Email;
                yazarBilgileri.IsEmailVerificated = true;
                string resimAdi = ResimKaydet(vm.ProfilFoto);

                yazarBilgileri.ProfilFotoYolu = resimAdi;
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Home", vm); 
        }

        private string ResimKaydet(IFormFile resim)
        {
            string resimAdi = Guid.NewGuid() + Path.GetExtension(resim.FileName);

            string kaydetmeYolu = Path.Combine(_env.WebRootPath, "images", resimAdi);

            using (FileStream fs = new FileStream(kaydetmeYolu, FileMode.Create))
            {
                resim.CopyTo(fs);
            }

            return resimAdi;
        }

        private void resimSil(string resimYolu)
        {
            string silmeYolu = Path.Combine(_env.WebRootPath, "images", resimYolu);

            if(System.IO.File.Exists(silmeYolu))
            {
                System.IO.File.Delete(silmeYolu);
            }
        }

        public IActionResult Profil(string email)  
        {
            Yazar user = _db.Yazarlar.Where(x => x.Email == email).FirstOrDefault();
            UyeGirisiViewModel vm = new UyeGirisiViewModel();
            vm.YazarId = user.YazarId;
            vm.Email = user.Email;
            vm.Ad = user.Ad;
            vm.Soyad = user.Soyad;
            vm.KullaniciAdi = user.KullaniciAdi;
            vm.Bio = user.Bio;
            vm.ProfilFotoYolu = user.ProfilFotoYolu;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Profil(UyeGirisiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userEdit = _db.Yazarlar.Find(vm.YazarId);
                userEdit.YazarId = vm.YazarId;
                userEdit.Ad = vm.Ad;
                userEdit.Soyad = vm.Soyad;
                userEdit.Email = vm.Email;
                userEdit.KullaniciAdi = vm.KullaniciAdi;
                userEdit.Bio = vm.Bio;
                if(vm.ProfilFoto != null)
                { 
                if (!string.IsNullOrEmpty(vm.ProfilFotoYolu))
                {
                    resimSil(vm.ProfilFotoYolu);
                }
                userEdit.ProfilFotoYolu = ResimKaydet(vm.ProfilFoto);
           
            }
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
            }
            return View(vm);
        }
        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("KullaniciEmail");
            return RedirectToAction("Index", "Home");
        }
        private void mailAtmaIslemi()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;


            smtp.UseDefaultCredentials = false;
            //gönderici mail adresini authenticate et,doğrula,mail'de oturum aç
            smtp.Credentials = new NetworkCredential("smtpdenemeboost6@gmail.com", "cs001993");

            smtp.EnableSsl = true; //gmailin smtp sunucusuna bağlanacağımız protokol .. https..//smtp.gmail.com http mi https mi?

            smtp.Timeout = 10000; // 10 saniye boyunca iletmeyi deniyor.

            smtp.DeliveryFormat = (SmtpDeliveryFormat)SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("smtpdenemeboost6@gmail.com", "BilgeAdam Boost Ekibi");

            mail.To.Add(yazar.Email);

            mail.Subject = "Üye girişi için link";
            mail.Body = "<h1 style=\"color:blue; font-size:20px;\">Merhaba</h1><br/><h1>Blog sitemize giriş yapmak için <a href='http://localhost:30117/login/UyeGirisi?userid=" +uid+"'>buraya</a> tıklayın</h1><hr/><h2 style=\"background-color:red;\">İyi eğlenceler...</h2>";

             mail.IsBodyHtml = true;

            mail.SubjectEncoding = System.Text.Encoding.UTF8; // türkçe karakterleri desteklesin diye
            mail.BodyEncoding = System.Text.Encoding.UTF8;  // türkçe karakterleri desteklesin diye

            //mail.Attachments.Add(new Attachment("C://Users/cemse/Desktop/10255979872306.jpg"));

            try
            {
                smtp.Send(mail);
                mail.Dispose();
                ViewBag.Mesaj = "Mail gönderildi!";

            }
            catch (Exception ex)
            {

                ViewBag.Mesaj = "Hata oluştu : " + ex.ToString();
                throw;
            }
        }
    }
}
