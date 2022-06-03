using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace blogSitesi.Models
{
    public class Yazar
    {
        public Guid YazarId { get; set; } // TODO:? nullable olarak denenecek.
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string ProfilFotoYolu { get; set; }
        public string Bio { get; set; }
        public bool AktifMi { get; set; } = true;
        public bool IsEmailVerificated { get; set; }  //kullanıcı eposta aktivasyon linkine bastığında true'a dönecek.
        public bool IlkLogin { get; set; } = true;
        //public int KategoriId { get; set; }
        public List<Makale> Makaleler { get; set; }
        public List<Kategori> Kategoriler { get; set; } //takip ettiği kategoriler
    }
}
