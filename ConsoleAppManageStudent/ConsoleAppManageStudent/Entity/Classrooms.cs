using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Model
{
    public class Classrooms
    {
        public int _idClassroom { get; set; }
        public string _nameClasroom { get; set; }
        public string _nameSubject { get; set; }
        public Teachers _teacher { get; set; }

        public Classrooms() { }

        public Classrooms(int id, string nameClasroom, string nameSubject, Teachers teachers)
        {
            this._idClassroom = id;
            this._nameClasroom = nameClasroom;
            this._nameSubject = nameSubject;
            this._teacher = teachers;
        }

        public override string ToString()
        {
            return $"{_nameClasroom}\t| {_nameSubject}\t| {_teacher._nameTeacher}";
        }
    }
}
