namespace BlazorClient.DTO
{
    public class SearchStudentDTO
    {
        public int? Id { get; set; }
        public int? studentCode { get; set; }
        public string? studentName { get; set; }
        public DateTime? studentBirthday { get; set; }
        public string? studentAddress { get; set; }
        public int? classroomId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? sortBy { get; set; }
    }
}
