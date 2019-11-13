namespace PTS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KULLANICI")]
    public partial class KULLANICI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KULLANICI()
        {
            PERSONELs = new HashSet<PERSONEL>();
        }

        [Key]
        public int KULLANICI_REFNO { get; set; }

        [Required(ErrorMessage = "Kullan�c� Ad� Giriniz")]
        [StringLength(50, ErrorMessage = "Kullan�c� ad� en fazla 50 karakter aras�nda olmal�",
                         ErrorMessageResourceName = "",
                         ErrorMessageResourceType = null)]
        public string KULLANICI_ADI { get; set; }


        [Required(ErrorMessage = "Parola Giriniz")]
        [StringLength(20, ErrorMessage = "Parola en fazla 20 karakter aras�nda olmal�",
                         ErrorMessageResourceName = "",
                         ErrorMessageResourceType = null)]
        public string PAROLA { get; set; }

        public bool? DURUMU { get; set; }

        public int YETKI_GRUBU_REFNO { get; set; }

        public virtual YETKI_GRUBU YETKI_GRUBU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONEL> PERSONELs { get; set; }
    }
}
