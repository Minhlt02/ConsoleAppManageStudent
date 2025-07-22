using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Model
{
    public class Students
    {
        public int _idStudent { get; set; }
        public string _name {  get; set; }
        public string _birthday { get; set; }
        public string _address { get; set; }
        Classrooms _classrooms;

        public Students() { }   
        public Students(string name, string birthday, string address)
        {
            this._name = name;
            this._birthday = birthday;
            this._address = address;
        }

        public string toString()
        {
            return this._idStudent + ":" + this._name;
        }
    }
}
