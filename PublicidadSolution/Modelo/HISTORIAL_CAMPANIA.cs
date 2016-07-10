namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HISTORIAL_CAMPANIA
    {
        [Key]
        public int ID_HISTORIAL_CAMPANIA { get; set; }

        public int? SEC_CAMPANIA { get; set; }

        [StringLength(13)]
        public string RUC { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] FECHA_COMPRA { get; set; }

        public int? CLICS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? COSTO_CLIC { get; set; }

        public int? DESPLIEGUES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? COSTO_DESPLIEGUE { get; set; }

        public virtual CAMPANIA CAMPANIA { get; set; }
    }
}
