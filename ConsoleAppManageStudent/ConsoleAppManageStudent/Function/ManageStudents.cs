using ConsoleAppManageStudent.HandleException;
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
        HandleFormatDate handleFormat = new HandleFormatDate();

        public ManageStudents() { 
            listStudents = new List<Students>();
        }

        private int GenerateID()
        {
            int currentID = 1;

            if (listStudents.Count > 0 && listStudents != null)
            {
                currentID = listStudents[0]._idStudent;
                foreach (var student in listStudents)
                {
                    if (student._idStudent > currentID)
                    { 
                        currentID = student._idStudent;
                    }
                }
                currentID++;
            }

            return currentID;
        }

        public void AddStudents()
        {
            Students students = new Students();
            students._idStudent = GenerateID();
            Console.WriteLine("Nhập tên của sinh viên: ");
            students._name = Console.ReadLine();

            Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
            students._birthday = handleFormat.HandleFormatBirthday();

            Console.WriteLine("Nhập địa chỉ của sinh viên: ");
            students._address = Console.ReadLine();

            Teachers teachers = new Teachers(1, "Nguyễn Văn A", new DateTime(2000, 02, 20));
            students._classrooms = new Classrooms(1, "Phòng H.200", "Môn Tin", teachers);

            listStudents.Add(students);
            Console.WriteLine("Thêm thành công!");
        }

        public void ShowStudents(List<Students> list)
        {
            if (list != null && list.Count > 0)
            {
                Console.Write(new String('-', 42));
                Console.Write("DANH SÁCH SINH VIÊN");
                Console.Write(new String('-', 42));
                Console.WriteLine();
                Console.WriteLine("MSSV\t| Tên Sinh Viên\t| Ngày Sinh\t| Địa Chỉ\t| Lớp Học\t| Môn học\t| Tên giáo viên");
                foreach (Students student in list)
                { 
                    Console.WriteLine(student.toString());
                }
                Console.Write(new String('-', 50));
                Console.Write("***");
                Console.Write(new String('-', 50));
                Console.WriteLine();
            } else
            {
                Console.WriteLine("Danh sách trống!");
            }
        }

        public void UpdateStudent()
        {
            int studentID = int.Parse(Console.ReadLine());
            Students students = FindStudentById(studentID);
            if (students != null)
            {
                Console.WriteLine("Nếu không muốn thay đổi hãy bỏ trống!");
                Console.WriteLine("Thay đổi tên của sinh viên: ");
                string name = Console.ReadLine();
                if (name != null && name.Length > 0)
                {
                    students._name = name;
                }

                Console.WriteLine("Thay đổi ngày sinh của sinh viên (Nhập 1 để bỏ qua hoặc bấm bất kỳ để thay đổi) : ");
                string skip = Console.ReadLine();
                if (skip.Equals("1"))
                {
                    students._birthday = students._birthday;
                } else
                {
                    Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
                    DateTime date = handleFormat.HandleFormatBirthday();
                    if (date != null)
                    {
                        students._birthday = date;
                    }
                }
                    

                Console.WriteLine("Thay đổi địa chỉ của sinh viên: ");
                string address = Console.ReadLine();
                if (address != null && address.Length > 0)
                {
                    students._address = address;
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
            }
        }

        public bool DeleteStudent()
        {
            int studentID = int.Parse(Console.ReadLine());
            Students students = FindStudentById(studentID);
            bool isDelete = false;
            if (students != null && students._idStudent == studentID)
            {
                isDelete = listStudents.Remove(students);
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
            }
            return isDelete;
        }

        public void SortStudentByName()
        {
            listStudents.Sort(delegate (Students student1, Students student2)
            {
                return student1._name.CompareTo(student2._name);
            });
        }

        public Students FindStudentById(int studentID)
        {
            Students students = null;
            if (listStudents != null && listStudents.Count > 0)
            {
                foreach (var student in listStudents)
                {
                    if (student._idStudent == studentID)
                    {
                        students = student;
                    } 
                }
            } 
            return students;
        }

        public List<Students> GetStudents() { return listStudents; }
    }
}
