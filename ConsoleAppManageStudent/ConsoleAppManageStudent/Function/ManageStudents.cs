using ConsoleAppManageStudent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Function
{
    class ManageStudents
    {
        List<Students> listStudents;

        public ManageStudents() { 
            listStudents = new List<Students>();
        }

        public void AddStudents()
        {
            Students students = new Students();
            Console.WriteLine("Nhập tên của sinh viên: ");
            students._name = Console.ReadLine();
            Console.WriteLine("Nhập ngày sinh của sinh viên: ");
            students._birthday = Console.ReadLine();
            Console.WriteLine("Nhập địa chỉ của sinh viên: ");
            students._address = Console.ReadLine();

            listStudents.Add(students);
        }

        public void ShowStudents(List<Students> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (Students student in list)
                { 
                    Console.WriteLine(student.toString());
                }
            }
        }

        public List<Students> GetStudents() { return listStudents; }
    }
}
