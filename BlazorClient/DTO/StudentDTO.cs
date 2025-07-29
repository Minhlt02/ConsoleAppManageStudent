using System.ComponentModel.DataAnnotations;

namespace BlazorClient.DTO
{
    public class StudentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string studentName { get; set; } = null!;

        [Required]
        public DateTime? studentBirthday { get; set; }

        [Required]
        public string studentAddress { get; set; } = null!;

        [Required]
        public int classroomID { get; set; }

        public string classroomName { get; set; } = null!;

        public string subjectName { get; set; } = null!;

        public int teacherID { get; set; }

        public string teacherName { get; set; } = null!;

        public DateTime teacherBirthday { get; set; }

    }
}
