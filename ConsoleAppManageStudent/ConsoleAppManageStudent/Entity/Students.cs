using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Model
{
    internal class Students
    {
        private int _idStudent;
        private string _name;
        private DateTime _birthday;
        private string _address;
        Classrooms _classrooms;

        public int getId()
        {
            return _idStudent;
        }

        public void setId(int id)
        {
            _idStudent = id;
        }

        
    }
}
