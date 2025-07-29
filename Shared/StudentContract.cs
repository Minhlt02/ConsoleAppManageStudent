using ProtoBuf.Grpc;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Shared
{
    [ServiceContract]
    public interface IStudentContract
    {
        [OperationContract]
        Task<OperationReply> AddStudentAsync(StudentProfile request, CallContext callContaxt = default);
        [OperationContract]
        Task<OperationReply> DeleteStudentAsync(RequestId request, CallContext callContaxt = default);
        [OperationContract]
        Task<StudentReply> GetStudentByIdAsync(RequestId request, CallContext callContaxt = default);
        [OperationContract]
        Task<MultipleStudentReply> GetAllStudentAsync(Empty request, CallContext callContaxt = default);
        [OperationContract]
        Task<MultipleStudentReply> GetSortStudentAsync(Empty request, CallContext callContaxt = default);
        [OperationContract]
        Task<OperationReply> UpdateStudentAsync(StudentProfile request, CallContext callContaxt = default);
        [OperationContract]
        Task<MultipleStudentReply> GetPaginationAsync(PaginationRequest request, CallContext callContext = default);

        [OperationContract]
        Task<MultipleStudentReply> GetPaginationSortAsync(PaginationRequest request, CallContext callContext = default);
        [OperationContract]
        Task<StudentAgeChart> GetStudentAgeChartAsync(RequestId request, CallContext callContext = default);
    }

    [DataContract]
    public class StudentReply
    {
        [DataMember(Order = 1)] 
        public StudentProfile? Student { get; set; }
        [DataMember(Order = 2)] 
        public string? Message { get; set; }
    }

    [DataContract]
    public class MultipleStudentReply
    {
        [DataMember(Order = 1)] 
        public List<StudentProfile>? listStudents { get; set; }
        [DataMember(Order = 2)] 
        public int Count { get; set; }
        [DataMember(Order = 3)] 
        public string? Message { get; set; }
    }


    [DataContract]
    public class StudentProfile
    {
        [DataMember(Order = 1)]
        public int id { get; set; }

        [DataMember(Order = 2)]
        public string? studentName { get; set; }
        [DataMember(Order = 3)]
        public DateTime studentBirthday { get; set; }
        [DataMember(Order = 4)]
        public string? studentAddress { get; set; }
        [DataMember(Order = 5)]
        public int classroomID { get; set; }
        [DataMember(Order = 6)]
        public string? classroomName{ get; set; }
        [DataMember(Order = 7)]
        public string? subjectName { get; set; }
        [DataMember(Order = 8)]
        public int teacherID { get; set; }
        [DataMember(Order = 9)]
        public string? teacherName { get; set; }
        [DataMember(Order = 10)]
        public DateTime teacherBirthday { get; set; }
    }

    [DataContract]
    public class PaginationRequest
    {
        [DataMember(Order = 1)]
        public int? Id { get; set; }
        [DataMember(Order = 2)] 
        public string? studentName { get; set; }
        [DataMember(Order = 3)]
        public DateTime? studentBirthday { get; set; }
        [DataMember(Order = 4)] 
        public string? studentAddress { get; set; }
        [DataMember(Order = 5)]
        public int? classroomId { get; set; }
        [DataMember(Order = 6)] 
        public int PageNumber { get; set; }
        [DataMember(Order = 7)] 
        public int PageSize { get; set; }
        [DataMember(Order = 8)] 
        public string? SortBy { get; set; }
    }

    [DataContract]
    public class StudentAge
    {
        [DataMember(Order = 1)] public int Age { get; set; }
        [DataMember(Order = 2)] public int Count { get; set; }
    }


    [DataContract]
    public class StudentAgeChart
    {
        [DataMember(Order = 1)] public List<StudentAge> ChartData { get; set; } = null!;
    }
}
