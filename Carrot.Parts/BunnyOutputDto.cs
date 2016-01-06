using System;
using System.Runtime.Serialization;

namespace Carrot.Parts
{
    [DataContract]
    internal sealed class BunnyOutputDto
    {
        [DataMember]
        public string bunnyId { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int timeToEvaluate { get; set; }

        [DataMember]
        public BunnyOffenderDto[] offenders;
    }
}
