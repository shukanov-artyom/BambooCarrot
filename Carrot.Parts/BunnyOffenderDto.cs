using System;
using System.Runtime.Serialization;

namespace Carrot.Parts
{
    [DataContract]
    internal sealed class BunnyOffenderDto
    {
        [DataMember]
        public string devName { get; set; }

        [DataMember]
        public string planName { get; set; }

        [DataMember]
        public string failTime { get; set; }
    }
}
