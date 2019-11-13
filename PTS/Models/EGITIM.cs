namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EGITIM")]
    public partial class EGITIM
    {
        [Key]
        public int EGITIM_REFNO { get; set; }

        public bool EGITIM_DURUMU { get; set; }

        [Required(ErrorMessage = "Üniversite Giriniz")]
        [StringLength(100)]
        public string UNIVERSITE { get; set; }

        [Required(ErrorMessage = "Fakülte Giriniz")]
        [StringLength(50)]
        public string FAKULTE { get; set; }

        [Required(ErrorMessage = "Bölüm Giriniz")]
        [StringLength(50)]
        public string BOLUM { get; set; }

        [Required(ErrorMessage = "Baþlangýç Tarihi Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BASLANGIC_TARIHI { get; set; }

        [Column(TypeName = "date")]
        public DateTime BITIS_TARIHI { get; set; }

        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Derece Giriniz")]
        [StringLength(50)]
        public string OGRETIM_TIPI { get; set; }

        [StringLength(50)]
        public string OGRETIM_DILI { get; set; }

        public double? DIPLOMA_NOTU { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
