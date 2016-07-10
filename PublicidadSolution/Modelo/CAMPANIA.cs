namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAMPANIA")]
    public partial class CAMPANIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAMPANIA()
        {
            DETALLE_CAMPANIA = new HashSet<DETALLE_CAMPANIA>();
            HISTORIAL_CAMPANIA = new HashSet<HISTORIAL_CAMPANIA>();
        }

        [Key]
        [Column(Order = 0)]
        public int SEC_CAMPANIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string RUC { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(256)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] FECHA_CREACION { get; set; }

        public DateTime FECHA_INICIO { get; set; }

        public DateTime FECHA_FIN { get; set; }

        [Required]
        [StringLength(1)]
        public string ESTADO { get; set; }

        public virtual EMPRESA EMPRESA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_CAMPANIA> DETALLE_CAMPANIA { get; set; }

        public virtual ESTADISTICA_CAMPANIA ESTADISTICA_CAMPANIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORIAL_CAMPANIA> HISTORIAL_CAMPANIA { get; set; }
    }
}
