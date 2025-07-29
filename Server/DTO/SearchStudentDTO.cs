using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    public class SearchStudentDTO
    {
        public int? Id { get; set; }
        public string? studentName { get; set; }
        public DateTime? studentBirthday { get; set; }
        public string? studentAddress { get; set; }
        public int? classroomId { get; set; }

        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public string? sortBy { get; set; }
    }
}
