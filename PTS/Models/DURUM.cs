namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DURUM")]
    public partial class DURUM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DURUM()
        {
            IZINs = new HashSet<IZIN>();
        }

        [Key]
        public int DURUM_REFNO { get; set; }

        [Required]
        [StringLength(50)]
        public string DURUM_ADI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IZIN> IZINs { get; set; }
    }
}
