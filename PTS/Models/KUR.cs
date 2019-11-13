namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KURS")]
    public partial class KUR
    {
        [Key]
        public int KURS_REFNO { get; set; }

        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Kurs Adý Giriniz")]
        [StringLength(50)]
        public string KURS_ADI { get; set; }

        [Required(ErrorMessage = "Baþlangýç Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BASLANGIC_TARIHI { get; set; }

        [Required(ErrorMessage = "Bitiþ Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BITIS_TARIHI { get; set; }

        public int KURS_SAATI { get; set; }

        [Required(ErrorMessage = "Kurs Yeri Giriniz")]
        [StringLength(50)]
        public string KURS_YERI { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
