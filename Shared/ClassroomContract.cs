using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IClassroomContract
    {
        [OperationContract]
        Task<ListClassroomProfile> GetAllClassroomAsync(Empty empty, CallContext callContext = default);

    }

    [DataContract]
    public class ClassroomProfile
    {
        [DataMember(Order = 1)]
        public int classroomID { get; set; }
        [DataMember(Order = 2)]
        public string? classroomName { get; set; }
        [DataMember(Order = 3)]
        public string? subjectName { get; set; }
    }

    public class ListClassroomProfile
    {
        [DataMember(Order = 1)] public List<ClassroomProfile>? ClassroomList { get; set; }

        [DataMember(Order = 2)] public int Count { get; set; } = 0;

        [DataMember(Order = 3)] public string? Message { get; set; }
    }
}
