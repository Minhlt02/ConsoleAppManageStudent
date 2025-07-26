using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Entity
{
    public class Classrooms
    {
        public virtual int _id { get; set; }
        public virtual int _idClassroom { get; set; }
        public virtual string _nameClassroom { get; set; }
        public virtual string _nameSubject { get; set; }
        public virtual Teachers _teacher { get; set; }

        public Classrooms() { }

        public Classrooms(int classroomId, string classroomName, string subjectName, Teachers teachers)
        {
            this._idClassroom = classroomId;
            this._nameClassroom = classroomName;
            this._nameSubject = subjectName;
            this._teacher = teachers;
        }

        public virtual string toString()
        {
            return this._idClassroom + "\t|" + this._nameClassroom + "\t\t|" + this._nameSubject + "\t|" + this._teacher.toString();
        }
    }
}
