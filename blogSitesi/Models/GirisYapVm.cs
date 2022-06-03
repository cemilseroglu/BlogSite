using System.ComponentModel.DataAnnotations;

namespace blogSitesi.Models
{
    public class GirisYapVm
    {
        [Required(ErrorMessage ="Bu alan zorunludur.")]
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string ProfilFotoYolu { get; set; }
        public string Bio { get; set; }
        public bool AktifMi { get; set; } = true; //kullanıcı silinirse eğer aktifMi true'a dönecek
        public bool IsEmailVerificated { get; set; } = false;  //kullanıcı eposta aktivasyon linkine bastığında true'a dönecek.
        public bool IlkLogin { get; set; } = false;
        //public int KategoriId { get; set; }
    }
}
