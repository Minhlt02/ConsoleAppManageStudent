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
        Task<MultipleClassroomProfile> GetAllClassroomAsync(Empty empty, CallContext callContext = default);

    }

    [DataContract]
    public class ClassroomProfile
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string NameClassroom { get; set; }
        [DataMember(Order = 3)]
        public string NameSubject { get; set; }

    }

    [DataContract]
    public class MultipleClassroomProfile
    {
        [DataMember(Order = 1)] public List<ClassroomProfile> ClassroomList { get; set; }

        [DataMember(Order = 2)] public int Count { get; set; } = 0;

        [DataMember(Order = 3)] public string? Message { get; set; }
    }
}
