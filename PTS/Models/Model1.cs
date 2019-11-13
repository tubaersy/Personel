namespace PTS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PROJE : DbContext
    {
        public PROJE()
            : base("name=PROJE")
        {
        }

        public virtual DbSet<CALISTIGI_YER> CALISTIGI_YER { get; set; }
        public virtual DbSet<DEGERLENDIRME> DEGERLENDIRMEs { get; set; }
        public virtual DbSet<DEPARTMAN> DEPARTMAN { get; set; }
        public virtual DbSet<DURUM> DURUMs { get; set; }
        public virtual DbSet<DUYURU> DUYURUs { get; set; }
        public virtual DbSet<EGITIM> EGITIMs { get; set; }
        public virtual DbSet<FAZLA_MESAI> FAZLA_MESAI { get; set; }
        public virtual DbSet<HAREKET_TIPI> HAREKET_TIPI { get; set; }
        public virtual DbSet<IZIN> IZINs { get; set; }
        public virtual DbSet<KULLANICI> KULLANICIs { get; set; }
        public virtual DbSet<KUR> KURS { get; set; }
        public virtual DbSet<MAA> MAAS { get; set; }
        public virtual DbSet<PERSONEL> PERSONELs { get; set; }
        public virtual DbSet<PERSONEL_HAREKET> PERSONEL_HAREKET { get; set; }
        public virtual DbSet<SAYFA> SAYFAs { get; set; }
        public virtual DbSet<SERTIFIKA> SERTIFIKAs { get; set; }
        public virtual DbSet<YABANCI_DIL> YABANCI_DIL { get; set; }
        public virtual DbSet<YETKI> YETKIs { get; set; }
        public virtual DbSet<YETKI_GRUBU> YETKI_GRUBU { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CALISTIGI_YER>()
                .Property(e => e.ADI)
                .IsUnicode(false);

            modelBuilder.Entity<CALISTIGI_YER>()
                .Property(e => e.UNVANI)
                .IsUnicode(false);

            modelBuilder.Entity<CALISTIGI_YER>()
                .Property(e => e.SEHIR)
                .IsUnicode(false);

            modelBuilder.Entity<DEPARTMAN>()
                .Property(e => e.DEPARTMAN_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<DEPARTMAN>()
                .HasMany(e => e.PERSONELs)
                .WithRequired(e => e.DEPARTMAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DURUM>()
                .Property(e => e.DURUM_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<DURUM>()
                .HasMany(e => e.IZINs)
                .WithRequired(e => e.DURUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DUYURU>()
                .Property(e => e.DUYURU_BASLIK)
                .IsUnicode(false);

            modelBuilder.Entity<DUYURU>()
                .Property(e => e.DUYURU_ICERIK)
                .IsUnicode(false);

            modelBuilder.Entity<EGITIM>()
                .Property(e => e.UNIVERSITE)
                .IsUnicode(false);

            modelBuilder.Entity<EGITIM>()
                .Property(e => e.FAKULTE)
                .IsUnicode(false);

            modelBuilder.Entity<EGITIM>()
                .Property(e => e.BOLUM)
                .IsUnicode(false);

            modelBuilder.Entity<EGITIM>()
                .Property(e => e.OGRETIM_TIPI)
                .IsUnicode(false);

            modelBuilder.Entity<EGITIM>()
                .Property(e => e.OGRETIM_DILI)
                .IsUnicode(false);

            modelBuilder.Entity<FAZLA_MESAI>()
                .Property(e => e.ACIKLAMA)
                .IsUnicode(false);

            modelBuilder.Entity<HAREKET_TIPI>()
                .Property(e => e.HAREKET_TIPI_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<HAREKET_TIPI>()
                .HasMany(e => e.PERSONEL_HAREKET)
                .WithRequired(e => e.HAREKET_TIPI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KULLANICI>()
                .Property(e => e.KULLANICI_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<KULLANICI>()
                .Property(e => e.PAROLA)
                .IsUnicode(false);

            modelBuilder.Entity<KULLANICI>()
                .HasMany(e => e.PERSONELs)
                .WithRequired(e => e.KULLANICI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KUR>()
                .Property(e => e.KURS_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<KUR>()
                .Property(e => e.KURS_YERI)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .Property(e => e.ADI_SOYADI)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .Property(e => e.TELEFON)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .Property(e => e.RESIM)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .Property(e => e.ADRES)
                .IsUnicode(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.CALISTIGI_YER)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.DEGERLENDIRMEs)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.EGITIMs)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.FAZLA_MESAI)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.IZINs)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.KURS)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.MAAS)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.PERSONEL_HAREKET)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PERSONEL>()
                .HasOptional(e => e.SERTIFIKA)
                .WithRequired(e => e.PERSONEL);

            modelBuilder.Entity<PERSONEL>()
                .HasMany(e => e.YABANCI_DIL)
                .WithRequired(e => e.PERSONEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SAYFA>()
                .Property(e => e.SAYFA_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<SAYFA>()
                .HasMany(e => e.YETKIs)
                .WithRequired(e => e.SAYFA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SERTIFIKA>()
                .Property(e => e.SERTIFIKA_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<SERTIFIKA>()
                .Property(e => e.ALINAN_KURUM)
                .IsUnicode(false);

            modelBuilder.Entity<YABANCI_DIL>()
                .Property(e => e.YABANCI_DIL_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<YABANCI_DIL>()
                .Property(e => e.SEVIYESI)
                .IsUnicode(false);

            modelBuilder.Entity<YETKI_GRUBU>()
                .Property(e => e.GRUP_ADI)
                .IsUnicode(false);

            modelBuilder.Entity<YETKI_GRUBU>()
                .HasMany(e => e.KULLANICIs)
                .WithRequired(e => e.YETKI_GRUBU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YETKI_GRUBU>()
                .HasMany(e => e.YETKIs)
                .WithRequired(e => e.YETKI_GRUBU)
                .WillCascadeOnDelete(false);
        }
    }
}
