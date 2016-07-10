namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPRESA")]
    public partial class EMPRESA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPRESA()
        {
            CAMPANIAs = new HashSet<CAMPANIA>();
            FACTURA_EMPRESA = new HashSet<FACTURA_EMPRESA>();
            TARGET_EDAD = new HashSet<TARGET_EDAD>();
        }

        [Key]
        [StringLength(13)]
        public string RUC { get; set; }

        [StringLength(1000)]
        public string RAZON_SOCIAL { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(20)]
        public string TELEFONO { get; set; }

        [StringLength(128)]
        public string DIRECCION { get; set; }

        [StringLength(128)]
        public string REPRESENTANTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPANIA> CAMPANIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURA_EMPRESA> FACTURA_EMPRESA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TARGET_EDAD> TARGET_EDAD { get; set; }
    }
}
