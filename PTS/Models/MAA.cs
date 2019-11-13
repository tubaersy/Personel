namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MAAS")]
    public partial class MAA
    {
        [Key]
        public int MAAS_REFNO { get; set; }

        [Required(ErrorMessage = "Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime HESAPLAMA_TARIHI { get; set; }

        public int PERSONEL_REFNO { get; set; }

        public int? AVANS { get; set; }

        public int? FAZLA_MESAI_UCRETI { get; set; }

        public int? YIL { get; set; }

        public int? AY { get; set; }

        public double? TOPLAM_UCRET { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
