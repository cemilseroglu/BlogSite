// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using blogSitesi.Models;

namespace blogSitesi.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20220417202020_170422")]
    partial class _170422
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("blogSitesi.Models.Kategori", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KategoriAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("YazarId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KategoriId");

                    b.HasIndex("YazarId");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("blogSitesi.Models.KategoriMakale", b =>
                {
                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<int>("MakaleId")
                        .HasColumnType("int");

                    b.HasKey("KategoriId", "MakaleId");

                    b.HasIndex("MakaleId");

                    b.ToTable("KategorilerMakaleler");
                });

            modelBuilder.Entity("blogSitesi.Models.KategoriTakip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<Guid>("YazarId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("YazarId");

                    b.ToTable("KategorilerTakip");
                });

            modelBuilder.Entity("blogSitesi.Models.Makale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Baslik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icerik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OlusturulmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResimYolu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("YazarId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("YazarId");

                    b.ToTable("Makale");
                });

            modelBuilder.Entity("blogSitesi.Models.Yazar", b =>
                {
                    b.Property<Guid>("YazarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AktifMi")
                        .HasColumnType("bit");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IlkLogin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailVerificated")
                        .HasColumnType("bit");

                    b.Property<string>("KullaniciAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilFotoYolu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YazarId");

                    b.ToTable("Yazarlar");
                });

            modelBuilder.Entity("blogSitesi.Models.Kategori", b =>
                {
                    b.HasOne("blogSitesi.Models.Yazar", null)
                        .WithMany("Kategoriler")
                        .HasForeignKey("YazarId");
                });

            modelBuilder.Entity("blogSitesi.Models.KategoriMakale", b =>
                {
                    b.HasOne("blogSitesi.Models.Kategori", "Kategori")
                        .WithMany("KategorilerMakaleler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("blogSitesi.Models.Makale", "Makale")
                        .WithMany("KategorilerMakaleler")
                        .HasForeignKey("MakaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");

                    b.Navigation("Makale");
                });

            modelBuilder.Entity("blogSitesi.Models.KategoriTakip", b =>
                {
                    b.HasOne("blogSitesi.Models.Kategori", "Kategori")
                        .WithMany()
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("blogSitesi.Models.Yazar", "Yazar")
                        .WithMany()
                        .HasForeignKey("YazarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");

                    b.Navigation("Yazar");
                });

            modelBuilder.Entity("blogSitesi.Models.Makale", b =>
                {
                    b.HasOne("blogSitesi.Models.Yazar", "Yazarlar")
                        .WithMany("Makaleler")
                        .HasForeignKey("YazarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Yazarlar");
                });

            modelBuilder.Entity("blogSitesi.Models.Kategori", b =>
                {
                    b.Navigation("KategorilerMakaleler");
                });

            modelBuilder.Entity("blogSitesi.Models.Makale", b =>
                {
                    b.Navigation("KategorilerMakaleler");
                });

            modelBuilder.Entity("blogSitesi.Models.Yazar", b =>
                {
                    b.Navigation("Kategoriler");

                    b.Navigation("Makaleler");
                });
#pragma warning restore 612, 618
        }
    }
}
