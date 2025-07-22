using ConsoleAppManageStudent.Function;
using ConsoleAppManageStudent.HandleException;
using System.Numerics;
using System.Text;

namespace ConsoleAppManageStudent;

class Program
{
    static void Main(string[] args)
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
        HandleMenu handle = new HandleMenu();

        int numberChoice = handle.InputNumber();
        handle.handleContinueProgram(numberChoice);

        

    }
}