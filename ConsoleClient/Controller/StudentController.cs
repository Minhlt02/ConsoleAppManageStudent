using AutoMapper;
using ConsoleClient.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Controller
{
    public class StudentController
    {
        private readonly IStudentContract studentContract;
        private readonly IMapper mapper;

        public StudentController(IStudentContract _studentContract, IMapper _mapper)
        {
            studentContract = _studentContract;
            mapper = _mapper;
        }

        public async Task MenuAsync()
        {
            while(true)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write(new String('-', 20));
                Console.Write("***");
                Console.Write(new String('-', 20));
                Console.WriteLine();
                Console.WriteLine("DANH SÁCH CHỨC NĂNG QUẢN LÝ SINH VIÊN");
                Console.WriteLine("1. Xem danh sách sinh viên");
                Console.WriteLine("2. Thêm mới sinh viên");
                Console.WriteLine("3. Chỉnh sửa thông tin sinh viên");
                Console.WriteLine("4. Xóa sinh viên");
                Console.WriteLine("5. Sắp xếp dữ liệu sinh viên theo tên");
                Console.WriteLine("6. Tìm kiếm sinh viên theo Mã số sinh viên");
                Console.WriteLine("0. Kết thúc chương trình");
                Console.Write(new String('-', 20));
                Console.Write("***");
                Console.Write(new String('-', 20));
                Console.WriteLine();
                Console.Write("Nhập lựa chọn (0-6): ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        await GetAllStudentAsync();
                        break;
                    case 2:
                        await AddStudentAsync();
                        break;
                    case 3:
                        await UpdateStudentAsync();
                        break;
                    case 4:
                        await DeleteStudentAsync();
                        break;
                    case 5:
                        await GetSortStudentAsync();
                        break;
                    case 6:
                        await GetStudentByIdAsync();
                        break;
                    case 0:
                        return;
                }
}
        }

        public async Task AddStudentAsync()
        {
            Students students = new Students();

            Console.WriteLine("Nhập mã số sinh viên của sinh viên: ");
            students.studentCode = int.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine("Nhập tên của sinh viên: ");
            string? nameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("Tên sinh viên không được để trống!");
                return; // hoặc xử lý phù hợp
            }
            students.studentName = nameInput;
            Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
            students.studentBirthday = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);

            Console.WriteLine("Nhập địa chỉ của sinh viên: ");
            string? addressInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(addressInput))
            {
                Console.WriteLine("Tên sinh viên không được để trống!");
                return; // hoặc xử lý phù hợp
            }
            students.studentAddress = addressInput;

            Console.WriteLine("Nhập mã lớp học: ");
            students.classroomId = int.Parse(Console.ReadLine() ?? "1");

            Console.WriteLine("Thêm sinh viên thành công!");

            var request = mapper.Map<StudentProfile>(students);
            var reply = await studentContract.AddStudentAsync(request);
            if (reply.Success)
            {
                Console.WriteLine("Da them moi sinh vien");
            }
            else
            {
                Console.WriteLine(reply.Message);
            }
        }

        public async Task DeleteStudentAsync()
        {
            int studentID = int.Parse(Console.ReadLine() ?? "1");
            var isDeleted = await studentContract.DeleteStudentAsync(new RequestId { id = studentID });
            if (isDeleted.Success)
            {
                Console.WriteLine($"Đã xóa sinh viên {studentID} thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID đã nhập.");
            }
        }

        public async Task UpdateStudentAsync()
        {
            Console.WriteLine("Nhập MSSV muốn cập nhật thông tin: ");
            int studentID = int.Parse(Console.ReadLine() ?? "1");
            var studentReply = await studentContract.GetStudentByIdAsync(new RequestId { id = studentID });
            if (studentReply.Student != null)
            {
                Students students = mapper.Map<Students>(studentReply.Student);
                Console.WriteLine("Nếu không muốn thay đổi hãy bỏ trống!");
                Console.WriteLine("Thay đổi tên của sinh viên: ");
                string? name = Console.ReadLine();
                if (name != null && name.Length > 0)
                {
                    students.studentName = name;
                }

                Console.WriteLine("Thay đổi ngày sinh của sinh viên (Nhập 1 để bỏ qua hoặc bấm bất kỳ để thay đổi) : ");
                string skip = Console.ReadLine() ?? "";
                if (skip.Equals("1"))
                {
                    students.studentBirthday = students.studentBirthday;
                }
                else
                {
                    Console.WriteLine("Nhập ngày sinh của sinh viên (dd/mm/yyyy): ");
                    DateTime date = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);
                    students.studentBirthday = date;
                }


                Console.WriteLine("Thay đổi địa chỉ của sinh viên: ");
                string? address = Console.ReadLine();
                if (address != null && address.Length > 0)
                {
                    students.studentAddress = address;
                }

                Console.WriteLine("Thay đổi mã lớp của sinh viên: ");
                int classID = int.Parse(Console.ReadLine() ?? "1");
                if (classID > 0)
                {
                    students.classroomId = classID;
                }
                
                var request = mapper.Map<StudentProfile>(students);
                var reply = await studentContract.UpdateStudentAsync(request);
                if (!reply.Success)
                {
                    Console.WriteLine(reply.Message);
                    return;
                }
                Console.WriteLine("Cập nhật thông tin sinh viên thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
            }
        }

        public async Task GetStudentByIdAsync()
        {
            Console.WriteLine("Nhập MSSV muốn tìm: ");
            int id = int.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine("{0,-6}| {1,-15}| {2,-12}| {3,-12}| {4,-10}| {5,-10}| {6,-15}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ", "Lớp Học", "Môn học", "Tên giáo viên");
            var studentReply = await studentContract.GetStudentByIdAsync(new RequestId { id = id });
            if (studentReply.Student != null)
            {
                Students students = mapper.Map<Students>(studentReply.Student);
                Console.WriteLine("{0,-6}| {1,-15}| {2,-12:dd/MM/yyyy}| {3,-12}| {4,-10}| {5,-10}| {6,-15}",
                        students.studentCode,
                        students.studentName,
                        students.studentBirthday.ToString("dd/MM/yyyy"),
                        students.studentAddress,
                        students.classroomName,
                        students.subjectName,
                        students.teacherName);
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID đã nhập.");
            }
        }

        public async Task GetAllStudentAsync()
        {
            var studentList = await studentContract.GetAllStudentAsync(new Empty());

            if (studentList.listStudents != null)
            {
                List<Students> students = mapper.Map<List<Students>>(studentList.listStudents);
                Console.WriteLine("{0,-5} | {1,-15} | {2,-12} | {3,-10}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ");
                foreach (var student in students)
                {
                    Console.WriteLine("{0,-5} | {1,-15} | {2,-12:dd/MM/yyyy} | {3,-10}",
                    student.studentCode,
                    student.studentName,
                    student.studentBirthday.ToString("dd/MM/yyyy"),
                    student.studentAddress);
                }
            }

            
        }

        public async Task GetSortStudentAsync()
        {
            var studentList = await studentContract.GetSortStudentAsync(new Empty());

            if (studentList.listStudents != null)
            {
                var students = mapper.Map<List<Students>>(studentList.listStudents);
                Console.WriteLine("{0,-5} | {1,-15} | {2,-12} | {3,-10}", "MSSV", "Tên Sinh Viên", "Ngày Sinh", "Địa Chỉ");
                foreach (var student in students)
                {
                    Console.WriteLine("{0,-5} | {1,-15} | {2,-12:dd/MM/yyyy} | {3,-10}",
                    student.studentCode,
                    student.studentName,
                    student.studentBirthday.ToString("dd/MM/yyyy"),
                    student.studentAddress);
                }
            }
        }
    }
}
