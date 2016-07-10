namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ELEMENTO")]
    public partial class ELEMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ELEMENTO()
        {
            DETALLE_CAMPANIA = new HashSet<DETALLE_CAMPANIA>();
        }

        [Key]
        public int ID_ELEMENTO { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(2)]
        public string POSICION { get; set; }

        [StringLength(512)]
        public string URL { get; set; }

        [Required]
        [StringLength(512)]
        public string PATH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_CAMPANIA> DETALLE_CAMPANIA { get; set; }
    }
}
