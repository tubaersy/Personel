namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CALISTIGI_YER
    {
        [Key]
        public int CALISTIGI_YER_REFNO { get; set; }

        [Required(ErrorMessage = "Ýþ Yeri Adý Giriniz")]
        [StringLength(50)]
        public string ADI { get; set; }

        [Required(ErrorMessage = "Unvan Giriniz")]
        [StringLength(50)]
        public string UNVANI { get; set; }

        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Baþlangýç Tarihi Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BASLANGIC_TARIHI { get; set; }

        [Column(TypeName = "date")]
        public DateTime BITIS_TARIHI { get; set; }

        [StringLength(50)]
        public string SEHIR { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
