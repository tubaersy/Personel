namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class YABANCI_DIL
    {
        [Key]
        public int YABANCI_DIL_REFNO { get; set; }

        [Required(ErrorMessage = "Yabancý Dil Giriniz")]
        [StringLength(50)]
        public string YABANCI_DIL_ADI { get; set; }

        [Required(ErrorMessage = "Seviye Giriniz")]
        [StringLength(50)]
        public string SEVIYESI { get; set; }

        public int PERSONEL_REFNO { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
