using System.ComponentModel.DataAnnotations;

namespace BlazorClient.DTO
{
    public class ClassroomDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string NameClassroom { get; set; }

        [Required]
        public string NameSubject { get; set; }

        public int Count { get; set; } = 0;
    }
}
