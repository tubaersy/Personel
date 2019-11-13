namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PERSONEL_HAREKET
    {
        [Key]
        public int PERSONEL_HAREKET_REFNO { get; set; }

        public int PERSONEL_REFNO { get; set; }

        public int BORC { get; set; }

        public int ALACAK { get; set; }

        public int HAREKET_TIPI_REFNO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HAREKET_TARIHI { get; set; }

        public virtual HAREKET_TIPI HAREKET_TIPI { get; set; }

        public virtual PERSONEL PERSONEL { get; set; }
    }
}
