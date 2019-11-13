namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IZIN")]
    public partial class IZIN
    {
        [Key]
        public int IZIN_REFNO { get; set; }

        [Required(ErrorMessage = "Ba�lang�� Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BASLANGIC_TARIHI { get; set; }

        [Required(ErrorMessage = "Biti� Tarih Giriniz")]
        [Column(TypeName = "date")]
        public DateTime BITIS_TARIHI { get; set; }

        public int GUN { get; set; }

        public int DURUM_REFNO { get; set; }

        public int PERSONEL_REFNO { get; set; }

        public virtual DURUM DURUM { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
