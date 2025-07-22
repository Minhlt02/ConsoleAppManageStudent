using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Model
{
    public class Teachers
    {
        public int _idTeacher { get; set; }
        public string _nameTeacher { get; set; }
        public DateTime _birthdayTeacher { get; set; }

        public Teachers() { }

        public Teachers(int idTeacher, string nameTeacher, DateTime birthdayTeacher)
        {
            _idTeacher = idTeacher;
            _nameTeacher = nameTeacher;
            _birthdayTeacher = birthdayTeacher;
        }
    }
}
