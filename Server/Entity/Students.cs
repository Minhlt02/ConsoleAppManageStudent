using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entity
{
    public class Students
    {
        public virtual int _id { get; set; }
        public virtual string? _name { get; set; }
        public virtual DateTime _birthday { get; set; }
        public virtual string? _address { get; set; }
        public virtual Classrooms _classrooms { get; set; }


        public Students() { }
        public Students(string name, DateTime birthday, string address, Classrooms classrooms)
        {
            this._name = name;
            this._birthday = birthday;
            this._address = address;
            this._classrooms = classrooms;
        }

        public virtual string toString()
        {
            return this._name + "\t\t|" + this._birthday.ToString("dd/MM/yyyy") + "\t|" + this._address + "\t\t|" + this._classrooms.toString();
        }
    }
}
