using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entity
{
    public class Teachers
    {
        public virtual int Id { get; set; }
        public virtual string NameTeacher { get; set; }
        public virtual DateTime BirthdayTeacher { get; set; }

        public Teachers() { }

        public Teachers(string teacherName, DateTime teacherBirthday)
        {
            NameTeacher = teacherName;
            BirthdayTeacher = teacherBirthday;
        }

        public virtual string toString()
        {
            return this.NameTeacher + "\t\t|" + this.BirthdayTeacher.ToString("dd/MM/yyyy");
        }
    }
}
