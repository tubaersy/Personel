namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PERSONEL")]
    public partial class PERSONEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSONEL()
        {
            CALISTIGI_YER = new HashSet<CALISTIGI_YER>();
            DEGERLENDIRMEs = new HashSet<DEGERLENDIRME>();
            EGITIMs = new HashSet<EGITIM>();
            FAZLA_MESAI = new HashSet<FAZLA_MESAI>();
            IZINs = new HashSet<IZIN>();
            KURS = new HashSet<KUR>();
            MAAS = new HashSet<MAA>();
            PERSONEL_HAREKET = new HashSet<PERSONEL_HAREKET>();
            YABANCI_DIL = new HashSet<YABANCI_DIL>();
        }

        [Key]
        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Ad Soyad Giriniz")]
        [StringLength(50, ErrorMessage = "Personel adý en fazla 50 karakter arasýnda olmalý",
                         ErrorMessageResourceName = "",
                         ErrorMessageResourceType = null)]
        public string ADI_SOYADI { get; set; }

        public int DEPARTMAN_REFNO { get; set; }

        [Required(ErrorMessage = "Doðum Günü Tarihi Ekleyiniz")]
        [Column(TypeName = "date")]
        public DateTime DOGUM_GUNU_TARIHI { get; set; }

        [Required(ErrorMessage = "Telefon Giriniz")]
        [StringLength(10)]
        public string TELEFON { get; set; }

        [Required(ErrorMessage = "Mail Giriniz")]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Resim Ekleyiniz")]
        [StringLength(50)]
        public string RESIM { get; set; }

        public int KULLANICI_REFNO { get; set; }

        [Required(ErrorMessage = "Ýþe Baþlama Tarihi Giriniz")]
        [Column(TypeName = "date")]
        public DateTime ISE_BASLAMA_TARIHI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ISTEN_CIKMA_TARIHI { get; set; }

        public bool DURUMU { get; set; }

        public int AYLIK_UCRET { get; set; }

        [StringLength(500)]
        public string ADRES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALISTIGI_YER> CALISTIGI_YER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEGERLENDIRME> DEGERLENDIRMEs { get; set; }

        [Required(ErrorMessage = "Departman Giriniz")]
        public virtual DEPARTMAN DEPARTMAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EGITIM> EGITIMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAZLA_MESAI> FAZLA_MESAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IZIN> IZINs { get; set; }

        public virtual KULLANICI KULLANICI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KUR> KURS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAA> MAAS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONEL_HAREKET> PERSONEL_HAREKET { get; set; }

        public virtual SERTIFIKA SERTIFIKA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YABANCI_DIL> YABANCI_DIL { get; set; }
    }
}
