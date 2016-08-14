namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("USUARIO")]
    [Serializable]
    [DataContract]    
    public partial class USUARIO
    {
        [Key]
        [DataMember]
        public int ID_USUARIO { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(32)]
        [DataMember]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(200)]
        [DataMember]
        public string NOMBRES { get; set; }
    }
}
