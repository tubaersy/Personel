namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEGERLENDIRME")]
    public partial class DEGERLENDIRME
    {
        [Key]
        public int DEGERLENDIRME_REFNO { get; set; }

        public int PERSONEL_REFNO { get; set; }

        [Required(ErrorMessage = "Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime TARIH { get; set; }

        public int IS_BILGISI { get; set; }

        public int IS_KALITESI { get; set; }

        public int ZAMANLAMA { get; set; }

        public int KAVRAMA { get; set; }

        public int SORUMLULUK { get; set; }

        public int INISIYATIF { get; set; }

        public int OZVERI { get; set; }

        public int CALISMA_ODAKLI { get; set; }

        public int EKIP_CALISMASI { get; set; }

        public int YARATICILIK { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
