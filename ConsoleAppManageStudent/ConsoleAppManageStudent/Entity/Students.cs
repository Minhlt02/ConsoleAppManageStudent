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
        public DateTime _birthday { get; set; }
        public string _address { get; set; }
        public Classrooms _classrooms { get; set; }

        public Students() { }   
        public Students(int id, string name, DateTime birthday, string address, Classrooms classrooms)
        {
            this._idStudent = id;
            this._name = name;
            this._birthday = birthday;
            this._address = address;
            this._classrooms = classrooms;
        }

        public string toString()
        {
            return this._idStudent + "\t|" + this._name + "\t\t|" + this._birthday.ToString("dd/MM/yyyy") + "\t|" + this._address + "\t\t|" + this._classrooms.ToString();
        }
    }
}
