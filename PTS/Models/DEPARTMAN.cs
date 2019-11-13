namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEPARTMAN")]
    public partial class DEPARTMAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEPARTMAN()
        {
            PERSONELs = new HashSet<PERSONEL>();
        }

        [Key]
        public int DEPARTMAN_REFNO { get; set; }

        [Required]
        [StringLength(50)]
        public string DEPARTMAN_ADI { get; set; }

        public int? KULLANICI_REFNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONEL> PERSONELs { get; set; }
    }
}
