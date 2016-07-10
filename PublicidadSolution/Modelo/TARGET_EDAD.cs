namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TARGET_EDAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TARGET_EDAD()
        {
            SEGMENTO_DETALLE_CAMPANIA = new HashSet<SEGMENTO_DETALLE_CAMPANIA>();
        }

        [Key]
        public int ID_TARGET_EDAD { get; set; }

        [StringLength(13)]
        public string RUC { get; set; }

        [Required]
        [StringLength(32)]
        public string NOMBRE { get; set; }

        [StringLength(128)]
        public string DESCRIPCION { get; set; }

        public int EDAD_MINIMA { get; set; }

        public int EDAD_MAXIMA { get; set; }

        [Required]
        [StringLength(3)]
        public string GENERO { get; set; }

        public virtual EMPRESA EMPRESA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEGMENTO_DETALLE_CAMPANIA> SEGMENTO_DETALLE_CAMPANIA { get; set; }
    }
}
