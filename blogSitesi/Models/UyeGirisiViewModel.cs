using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace blogSitesi.Models
{
    public class UyeGirisiViewModel
    {
        public Guid YazarId { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage ="Ad bölümü boş bırakılamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage ="Soyad bölümü boş bırakılamaz.")]
        public string Soyad { get; set; }
        public IFormFile ProfilFoto { get; set; }
        public string ProfilFotoYolu { get; set; }
        public string Bio { get; set; }
        public bool AktifMi { get; set; } 
        public bool IsEmailVerificated { get; set; } 
        public bool IlkLogin { get; set; } = false;
    }
}
