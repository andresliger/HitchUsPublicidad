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
    public class Empresa
    {
        [DataMember]
        public string ruc { get; set; }

        [DataMember]
        public string razonSocial { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string telefono { get; set; }

        [DataMember]
        public string direccion { get; set; }

        [DataMember]
        public string representante { get; set; }

    }
}
