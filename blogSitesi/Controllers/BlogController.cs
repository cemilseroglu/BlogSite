using blogSitesi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace blogSitesi.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _db;
        private readonly IWebHostEnvironment _env;
        private static Guid uid;
        private static string yazarKullaniciAdi;
        public BlogController(BlogContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index(int? kategoriId)
        {
            BlogIndexViewModel vm = new BlogIndexViewModel();
            vm.Kategoriler = _db.Kategoriler.OrderBy(a => a.KategoriAdi).ToList();
            if(kategoriId == null)
                vm.Makaleler = _db.Makale.ToList();
            else
            {
                vm.Makaleler = _db.Makale.Where(a => a.KategoriId == kategoriId).ToList();
            }
            vm.KategoriId = kategoriId;


            return View(vm);
        }

        public IActionResult BlogYaz(string email)
        {
            kategorileriGetir();
            Yazar y = _db.Yazarlar.Where(x => x.Email == email).FirstOrDefault();
            YeniBlogViewModel vm = new YeniBlogViewModel();

            if (y != null)
            {
                uid = y.YazarId;
                yazarKullaniciAdi = y.KullaniciAdi;
            }
            else
                NotFound();
            return View();
        }

        private void kategorileriGetir()
        {
            ViewBag.Kategoriler = _db.Kategoriler.Select(
                a => new SelectListItem(a.KategoriAdi,a.KategoriId.ToString())
                ).ToList();
        }

        [HttpPost]
        public IActionResult BlogYaz(YeniBlogViewModel vm)
        {
            if(vm.Resim != null)
            {
                if(ModelState.IsValid)
                {
                    
                    string resimAdi = resimKaydet(vm.Resim);  
                    Makale m = new Makale();
                    m.YazarId = uid;
                    m.YazarKullaniciAdi = yazarKullaniciAdi;
                    m.Baslik = vm.Baslik;
                    m.OlusturulmaTarihi = DateTime.Now;
                    m.Icerik = vm.Icerik;
                    m.KategoriId = vm.KategoriId;
                    m.ResimYolu = resimAdi;

                    _db.Add(m);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            kategorileriGetir();
            return View();
        }

        private string resimKaydet(IFormFile resim)
        {
            string resimAdi = Guid.NewGuid() + Path.GetExtension(resim.FileName);

            string kaydetmeYolu = Path.Combine(_env.WebRootPath, "blogimages", resimAdi);

            using (FileStream fs = new FileStream(kaydetmeYolu,FileMode.Create))
            {
                resim.CopyTo(fs);
            }

            return resimAdi;
        }
    }
}
