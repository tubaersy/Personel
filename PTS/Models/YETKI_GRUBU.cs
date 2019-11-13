namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class YETKI_GRUBU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YETKI_GRUBU()
        {
            KULLANICIs = new HashSet<KULLANICI>();
            YETKIs = new HashSet<YETKI>();
        }

        [Key]
        public int YETKI_GRUBU_REFNO { get; set; }

        [Required]
        [StringLength(50)]
        public string GRUP_ADI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KULLANICI> KULLANICIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YETKI> YETKIs { get; set; }
    }
}
