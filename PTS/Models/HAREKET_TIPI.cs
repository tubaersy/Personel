namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HAREKET_TIPI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HAREKET_TIPI()
        {
            PERSONEL_HAREKET = new HashSet<PERSONEL_HAREKET>();
        }

        [Key]
        public int HAREKET_TIPI_REFNO { get; set; }

        [Required]
        [StringLength(50)]
        public string HAREKET_TIPI_ADI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONEL_HAREKET> PERSONEL_HAREKET { get; set; }
    }
}
