namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SERTIFIKA")]
    public partial class SERTIFIKA
    {
        [Key]
        public int SERTIFIKA_REFNO { get; set; }

        [Required(ErrorMessage = "Sertifika Giriniz")]
        [StringLength(50)]
        public string SERTIFIKA_ADI { get; set; }

        public int GECERLILIK_SURESI { get; set; }

        [Required(ErrorMessage = "Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime ALINAN_TARIHI { get; set; }

        [Required(ErrorMessage = "Kurum Adý Giriniz")]
        [StringLength(50)]
        public string ALINAN_KURUM { get; set; }

        public int PERSONEL_REFNO { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
