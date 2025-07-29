using ManageStudentConsole.Entity;
using ManageStudentConsole.HandleException;
using ManageStudentConsole.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.Controller
{
    public class StudentController
    {
        private readonly IStudentRepository studentRepo;
        private Students students;
        private HandleFormatDate handleFormat;

        public StudentController(IStudentRepository _studentRepo)
        {
            studentRepo = _studentRepo;
            students = new Students();
            handleFormat = new HandleFormatDate();
        }
        public void AddStudent()
        {
            Console.WriteLine("Nhập tên của sinh viên: ");
            string? nameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("Tên sinh viên không được để trống!");
                return; // hoặc xử lý phù hợp
            }
            students._name = nameInput;


            Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
            students._birthday = handleFormat.HandleFormatBirthday();

            Console.WriteLine("Nhập địa chỉ của sinh viên: ");
            string? addressInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(addressInput))
            {
                Console.WriteLine("Tên sinh viên không được để trống!");
                return; // hoặc xử lý phù hợp
            }
            students._address = addressInput;

            Console.WriteLine("Thêm sinh viên thành công!");

            studentRepo.Add(students);
        }

        public void DeleteStudent()
        {
            int studentID = int.Parse(Console.ReadLine() ?? "1");
            students = studentRepo.FindById(studentID);
            if (students != null)
            {
                studentRepo.Delete(studentID);
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID đã nhập.");
            }
        }

        public void UpdateStudent()
        {
            int studentID = int.Parse(Console.ReadLine() ?? "1");
            students = studentRepo.FindById(studentID);
            if (students != null)
            {
                Console.WriteLine("Nếu không muốn thay đổi hãy bỏ trống!");
                Console.WriteLine("Thay đổi tên của sinh viên: ");
                string? name = Console.ReadLine();
                if (name != null && name.Length > 0)
                {
                    students._name = name;
                }

                Console.WriteLine("Thay đổi ngày sinh của sinh viên (Nhập 1 để bỏ qua hoặc bấm bất kỳ để thay đổi) : ");
                string? skip = Console.ReadLine() ?? "";
                if (skip.Equals("1"))
                {
                    students._birthday = students._birthday;
                }
                else
                {
                    Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
                    DateTime date = handleFormat.HandleFormatBirthday();
                    students._birthday = date;
                }


                Console.WriteLine("Thay đổi địa chỉ của sinh viên: ");
                string? address = Console.ReadLine();
                if (address != null && address.Length > 0)
                {
                    students._address = address;
                }

                studentRepo.Update(students);
                Console.WriteLine("Cập nhật thông tin sinh viên thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
            }
        }

        public void DisplayFindById()
        {
            Console.WriteLine("Nhập MSSV muốn tìm: ");
            int id = int.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine("{0,-6}| {1,-15}| {2,-12}| {3,-12}| {4,-10}| {5,-10}| {6,-15}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ", "Lớp Học", "Môn học", "Tên giáo viên");
            students = studentRepo.FindById(id);
            if (students != null)
            {
                if (students._classrooms != null && students._classrooms._teacher != null)
                {
                    Console.WriteLine("{0,-6}| {1,-15}| {2,-12:dd/MM/yyyy}| {3,-12}| {4,-10}| {5,-10}| {6,-15}",
                        students._id,
                        students._name,
                        students._birthday.ToString("dd/MM/yyyy"),
                        students._address,
                        students._classrooms._nameClassroom,
                        students._classrooms._nameSubject,
                        students._classrooms._teacher._nameTeacher);
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID đã nhập.");
            }
        }

        public void GetAllStudent()
        {
            var studentList = studentRepo.GetAll();

            if (!studentList.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-12} | {3,-10}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ");
            foreach (var student in studentList)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-12:dd/MM/yyyy} | {3,-10}",
                student._id,
                student._name,
                student._birthday.ToString("dd/MM/yyyy"),
                student._address);
            }
        }

        public void SortByName()
        {
            var studentList = studentRepo.SortByName();

            if (!studentList.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-12} | {3,-10}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ");
            foreach (var student in studentList)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-12:dd/MM/yyyy} | {3,-10}",
                student._id,
                student._name,
                student._birthday.ToString("dd/MM/yyyy"),
                student._address);
            }
        }
    }
}
