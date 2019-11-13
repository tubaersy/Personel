namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FAZLA_MESAI
    {
        [Key]
        public int FAZLA_MESAI_REFNO { get; set; }

        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime TARIH { get; set; }

        [Required(ErrorMessage = "Toplam Saat Giriniz")]
        public int TOPLAM_SAAT { get; set; }

        [Required(ErrorMessage = "A��klama Giriniz")]
        [StringLength(100)]
        public string ACIKLAMA { get; set; }

        [Required(ErrorMessage = "Ay Giriniz")]
        public int? AY { get; set; }

        [Required(ErrorMessage = "Y�l Giriniz")]
        public int? YIL { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
