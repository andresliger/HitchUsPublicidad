using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonMapping
{
    [Serializable]
    [DataContract]
    public class CampaniaJson
    {
        [DataMember]
        public int sec { get; set; }

        [DataMember]
        public Empresa empresa { get; set; }

        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public DateTime fechaCreacion { get; set; }

        [DataMember]
        public DateTime fechaInicio { get; set; }

        [DataMember]
        public DateTime fechaFin { get; set; }

        [DataMember]
        public string estado { get; set; }
        
    }
}
