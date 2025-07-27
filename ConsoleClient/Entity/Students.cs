using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Entity
{
    public class Students
    {
        public int Id { get; set; }
        public int studentCode { get; set; }

        public string studentName { get; set; } = null!;

        public DateTime studentBirthday { get; set; }

        public string studentAddress { get; set; } = null!;

        public int classroomId { get; set; }

        public string classroomName { get; set; } = null!;

        public string subjectName { get; set; } = null!;

        public int teacherId { get; set; }

        public string teacherName { get; set; } = null!;

        public DateTime teacherBirthday { get; set; }

    }
}
