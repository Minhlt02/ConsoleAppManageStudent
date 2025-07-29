using System.ComponentModel.DataAnnotations;

namespace BlazorClient.DTO
{
    public class TeacherDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string NameTeacher { get; set; }

        [Required]
        public DateTime BirthdayTeacher { get; set; }
    }
}
