using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Entity
{
    public class Teachers
    {
        public virtual int _id { get; set; }
        public virtual int _idTeacher { get; set; }
        public virtual string _nameTeacher { get; set; }
        public virtual DateTime _birthdayTeacher { get; set; }

        public Teachers() { }

        public Teachers(int teacherId, string teacherName, DateTime teacherBirthday)
        {
            _idTeacher = teacherId;
            _nameTeacher = teacherName;
            _birthdayTeacher = teacherBirthday;
        }

        public virtual string toString()
        {
            return this._idTeacher + "\t|" + this._nameTeacher + "\t\t|" + this._birthdayTeacher.ToString("dd/MM/yyyy");
        }
    }
}
