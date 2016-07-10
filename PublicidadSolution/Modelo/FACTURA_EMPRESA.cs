namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FACTURA_EMPRESA
    {
        [Key]
        public int ID_FACTURA { get; set; }

        [StringLength(13)]
        public string RUC { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] FECHA_EMISION { get; set; }

        [StringLength(15)]
        public string SECUENCIAL { get; set; }

        [StringLength(128)]
        public string DIRECCION { get; set; }

        [StringLength(20)]
        public string TELEFONO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VALOR_TOTAL { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PORCENTAJE_IVA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SUBTOTAL { get; set; }

        public virtual DETALLE_FACTURA DETALLE_FACTURA { get; set; }

        public virtual EMPRESA EMPRESA { get; set; }
    }
}
