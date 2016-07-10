namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [Key]
        public int ID_USUARIO { get; set; }

        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(32)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(200)]
        public string NOMBRES { get; set; }
    }
}
