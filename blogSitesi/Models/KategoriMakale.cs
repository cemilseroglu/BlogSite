using System.ComponentModel.DataAnnotations.Schema;

namespace blogSitesi.Models
{
    public class KategoriMakale
    {
        [ForeignKey("Makale")]
        public int MakaleId { get; set; }
        public Makale Makale { get; set; }

        [ForeignKey("Kategori")]
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
    }
}
