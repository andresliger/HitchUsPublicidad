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
    public class DetalleCampaniaJson
    {
        [DataMember]
        public DetalleCampaniaPK detalleCampaniaPK { get; set; }

        [DataMember]
        public CampaniaJson campania { get; set; }

        [DataMember]
        public ElementoJson elemento { get; set; }

        [DataMember]
        public Int32 despliegues { get; set; }

        [DataMember]
        public Int32 clics { get; set; }

        [DataMember]
        public String modoFacturacion { get; set; }

    }
}
