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
    public class ElementoJson
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string posicion { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string path { get; set; }


    }
}
