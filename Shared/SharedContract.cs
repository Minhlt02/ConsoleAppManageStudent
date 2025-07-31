using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [DataContract]
    public class Empty { }

    [DataContract]
    public class RequestId 
    {
        [DataMember(Order = 1)] public int id { get; set; }
        [DataMember(Order = 2)] public List<int> ids { get; set; }
    }
   

    [DataContract]
    public class MultipleRequest
    {
        [DataMember(Order = 1)] public string name { get; set; }
        [DataMember(Order = 2)] public int classroomId { get; set; }
        [DataMember(Order = 2)] public int teacherId { get; set; }
    }

    [DataContract]
    public class OperationReply
    {
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
    }
}
