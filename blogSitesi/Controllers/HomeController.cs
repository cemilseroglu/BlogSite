using blogSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace blogSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _db;

        public HomeController(ILogger<HomeController> logger,BlogContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int? kategoriId)
        {
            BlogIndexViewModel vm = new BlogIndexViewModel();
            vm.Kategoriler = _db.Kategoriler.OrderBy(a => a.KategoriAdi).ToList();
            if (kategoriId == null)
                vm.Makaleler = _db.Makale.ToList();
            else
            {
                vm.Makaleler = _db.Makale.Where(a => a.KategoriId == kategoriId).ToList();
            }
            vm.KategoriId = kategoriId;


            return View(vm);
        }

        public IActionResult Hakkinda()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
