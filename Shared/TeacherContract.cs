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
    public interface ITeacherContract
    {
        [OperationContract]
        Task<MultipleTeacherProfile> GetAllTeacherAsync(Empty empty, CallContext callContext = default);

    }

    [DataContract]
    public class TeacherProfile
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string NameTeacher { get; set; }
        [DataMember(Order = 3)]
        public DateTime BirthdayTeacher { get; set; }

    }

    [DataContract]
    public class MultipleTeacherProfile
    {
        [DataMember(Order = 1)] public List<TeacherProfile> TeacherList { get; set; }

        [DataMember(Order = 2)] public int Count { get; set; } = 0;

        [DataMember(Order = 3)] public string? Message { get; set; }
    }
}
