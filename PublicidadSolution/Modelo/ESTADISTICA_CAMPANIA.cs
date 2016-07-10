namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ESTADISTICA_CAMPANIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEC_CAMPANIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string RUC { get; set; }

        public int? DESPLIEGUES { get; set; }

        public int? CLICS { get; set; }

        public virtual CAMPANIA CAMPANIA { get; set; }
    }
}
