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
    public class DetalleCampaniaPK
    {
        [DataMember]
        public int secCampania { get; set; }

        [DataMember]
        public int idElemento { get; set; }

    }
}
