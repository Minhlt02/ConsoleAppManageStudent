using ConsoleAppManageStudent.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.View
{
    internal class MenuView
    {
        public void DisplayMenuView()
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

        public void SelectFunction(int numberChoice)
        {
            switch (numberChoice)
            {
                case 1:
                    DisplayListStudent displayListStudent = new DisplayListStudent();
                    displayListStudent.display();
                    break;
                case 0:
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                    break;
            }
                
        }
    }
}
