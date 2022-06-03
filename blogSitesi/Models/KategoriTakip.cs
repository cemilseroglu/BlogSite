using System;
using System.Collections.Generic;

namespace blogSitesi.Models
{
    public class KategoriTakip
    {
        public int Id { get; set; }
        public Guid YazarId { get; set; }
        public int KategoriId { get; set; }
        public Yazar Yazar { get; set; } 
        public Kategori Kategori { get; set; } 

        
         
    }
}
