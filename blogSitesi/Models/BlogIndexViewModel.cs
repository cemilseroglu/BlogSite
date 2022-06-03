using System.Collections.Generic;

namespace blogSitesi.Models
{
    public class BlogIndexViewModel
    {
        public List<Kategori> Kategoriler { get; set; }
        public List<Makale> Makaleler { get; set; }
        public Yazar yazarBilgisi { get; set; }
        public int? KategoriId { get; set; }
    }
}
