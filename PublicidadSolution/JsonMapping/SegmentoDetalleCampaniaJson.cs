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
    public class SegmentoDetalleCampaniaJson
    {

        [DataMember]
        public SegmentoDetalleCampaniaPK segmentoDetalleCampaniaPK { get; set; }

        [DataMember]
        public CampaniaJson campania { get; set; }

        [DataMember]
        public ElementoJson elemento { get; set; }

        [DataMember]
        public TargetJson targetEdad { get; set; }

        [DataMember]
        public String horaInicio { get; set; }

        [DataMember]
        public String horaFin { get; set; }

        [DataMember]
        public String maximoHora { get; set; }

        [DataMember]
        public String minimoHora { get; set; }

    }
}
