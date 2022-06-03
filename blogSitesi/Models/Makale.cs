using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogSitesi.Models
{
    public class Makale
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public int KategoriId { get; set; }
        public string ResimYolu { get; set; }
        public int OkunmaSuresi { get; set; } //bunu notmapped yapıcaz.fluent.api ile//Yapıldı.
        public Guid YazarId { get; set; }
        public string YazarKullaniciAdi { get; set; }
        public Yazar Yazarlar { get; set; }
        public List<KategoriMakale> KategorilerMakaleler { get; set; } 
    }
}
