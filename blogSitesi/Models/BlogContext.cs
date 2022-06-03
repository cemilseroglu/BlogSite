using Microsoft.EntityFrameworkCore;

namespace blogSitesi.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Makale> Makale { get; set; }
        public DbSet<KategoriTakip> KategorilerTakip { get; set; }
        public DbSet<KategoriMakale> KategorilerMakaleler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KategoriMakale>().HasKey(x => new { x.KategoriId, x.MakaleId });
            modelBuilder.Entity<Makale>().Ignore(c => c.OkunmaSuresi);
        }
    }
}
