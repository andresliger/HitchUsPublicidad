namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SEGMENTO_DETALLE_CAMPANIA
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

        public int ID_TARGET_EDAD { get; set; }

        [StringLength(5)]
        public string HORA_INICIO { get; set; }

        [StringLength(5)]
        public string HORA_FIN { get; set; }

        [StringLength(5)]
        public string MAXIMO_HORA { get; set; }

        [StringLength(5)]
        public string MINIMO_HORA { get; set; }

        public virtual DETALLE_CAMPANIA DETALLE_CAMPANIA { get; set; }

        public virtual TARGET_EDAD TARGET_EDAD { get; set; }
    }
}
