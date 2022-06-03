using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace blogSitesi.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public List<KategoriMakale> KategorilerMakaleler { get; set; }
    }
}
