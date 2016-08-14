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
    public class TargetJson
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Empresa empresa { get; set; }

        [DataMember]        
        public string nombre { get; set; }

        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public int edadMinima { get; set; }

        [DataMember]
        public int edadMaxima { get; set; }

        [DataMember]
        public string genero { get; set; }

    }
}
