namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLE_FACTURA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_FACTURA { get; set; }

        public int? CODIGO_PRODUCTO { get; set; }

        public int? CANTIDAD { get; set; }

        [StringLength(500)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VALOR_UNITARIO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VALOR_TOTAL { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VALOR_DESCUENTO { get; set; }

        public virtual FACTURA_EMPRESA FACTURA_EMPRESA { get; set; }
    }
}
