using ConsoleAppManageStudent.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.HandleException
{
    internal class HandleMenu
    {
        private ManageStudents manageStudents = new ManageStudents();
        public int InputNumber()
        {
            int numberChoice;
            while (true)
            {
                Console.Write("Nhập lựa chọn (0-6): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out numberChoice) && numberChoice >= 0 && numberChoice <= 6)
                {
                    return numberChoice;
                }
                Console.WriteLine("Vui lòng nhập số trong khoảng từ 0 đến 6.");
            }
        }


        public string InputString()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    return input.ToUpper();
                }
                Console.WriteLine("Vui lòng nhập Y hoặc N.");
            }
        }


        public void handleContinueProgram(int numberChoice)
        {
            while (numberChoice != 0)
            {
                SelectFunction(numberChoice);
                Console.WriteLine("Bạn có muốn tiếp tục không ? (Y/N)");
                string choice = InputString();
                if (choice.Equals("Y"))
                {
                    ShowMenu();
                    numberChoice = InputNumber();
                }
                else
                {
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                    return;
                }

            }
        }

        public void SelectFunction(int numberChoice)
        {
            switch (numberChoice)
            {
                case 1:
                    manageStudents.ShowStudents(manageStudents.GetStudents());
                    break;

                case 2:
                    manageStudents.AddStudents();
                    break;
                case 3:
                    Console.WriteLine("Nhập MSSV muốn thay đổi: ");
                    manageStudents.UpdateStudent();
                    break;
                case 4:
                    Console.WriteLine("Nhập MSSV muốn xóa: ");
                    if (manageStudents.DeleteStudent())
                    {
                        Console.WriteLine("Đã xóa thành công sinh viên");
                    }
                    break;
                case 5:
                    manageStudents.SortStudentByName();
                    manageStudents.ShowStudents(manageStudents.GetStudents());
                    break;
                case 6:
                    Console.WriteLine("Nhập MSSV muốn tìm: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("MSSV\t| Tên Sinh Viên\t| Ngày Sinh\t| Địa Chỉ\t| Lớp Học\t| Môn học\t| Tên giáo viên");
                    Console.WriteLine(manageStudents.FindStudentById(id).toString());
                    break;
                case 0:
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                    break;
            }
        }

        public void ShowMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("""
                --------------------***--------------------
                DANH SÁCH CHỨC NĂNG QUẢN LÝ SINH VIÊN
                1. Xem danh sách sinh viên
                2. Thêm mới sinh viên
                3. Chỉnh sửa thông tin sinh viên
                4. Xóa sinh viên
                5. Sắp xếp dữ liệu sinh viên theo tên
                6. Tìm kiếm sinh viên theo Mã số sinh viên
                0. Kết thúc chương trình
                --------------------***--------------------
                """);
        }
    }
}
