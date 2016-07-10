namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLE_CAMPANIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEC_CAMPANIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string RUC { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_ELEMENTO { get; set; }

        public int? DESPLIEGUES { get; set; }

        public int? CLICS { get; set; }

        [StringLength(3)]
        public string MODO_FACTURACION { get; set; }

        public virtual CAMPANIA CAMPANIA { get; set; }

        public virtual ELEMENTO ELEMENTO { get; set; }

        public virtual SEGMENTO_DETALLE_CAMPANIA SEGMENTO_DETALLE_CAMPANIA { get; set; }
    }
}
