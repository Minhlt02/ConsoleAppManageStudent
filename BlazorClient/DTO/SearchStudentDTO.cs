namespace BlazorClient.DTO
{
    public class SearchStudentDTO
    {
        public int? Id { get; set; }
        public string? studentName { get; set; }
        public DateTime? studentBirthday { get; set; }
        public string? studentAddress { get; set; }
        public int? classroomId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? keyword { get; set; }
        public int? teacherId { get; set; }

        public string? sortBy { get; set; }
    }
}
