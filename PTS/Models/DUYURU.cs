namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DUYURU")]
    public partial class DUYURU
    {
        [Key]
        public int DUYURU_REFNO { get; set; }

        [Required(ErrorMessage = "Baþlýk Giriniz")]
        [StringLength(50)]
        public string DUYURU_BASLIK { get; set; }

        [Required(ErrorMessage = "Ýçerik Giriniz")]
        [StringLength(500)]
        public string DUYURU_ICERIK { get; set; }
    }
}
