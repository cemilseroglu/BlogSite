using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace blogSitesi.Models
{
    public class YeniBlogViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Baslik { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Icerik { get; set; }
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
        public int KategoriId { get; set; }
        public IFormFile Resim { get; set; }
        public string ResimYolu { get; set; }
        public int OkunmaSuresi { get; set; } 
        public Guid YazarId { get; set; }
        public string yazarKullaniciAdi { get; set; }
    }
}
