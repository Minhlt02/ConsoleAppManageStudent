using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entity
{
    public class Classrooms
    {
        public virtual int Id { get; set; }
        public virtual string NameClassroom { get; set; }
        public virtual string NameSubject { get; set; }
        public virtual Teachers Teacher { get; set; }

        public Classrooms() { }

        public Classrooms(string classroomName, string subjectName, Teachers teachers)
        {
            this.NameClassroom = classroomName;
            this.NameSubject = subjectName;
            this.Teacher = teachers;
        }

        public virtual string toString()
        {
            return this.NameClassroom + "\t\t|" + this.NameSubject + "\t|" + this.Teacher.toString();
        }
    }
}
