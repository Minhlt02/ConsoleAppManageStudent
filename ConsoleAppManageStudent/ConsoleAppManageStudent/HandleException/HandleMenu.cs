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
        public int InputNumber()
        {
            int numberChoice = -1;
            while (numberChoice == -1)
            {
                try
                {
                    numberChoice = int.Parse(Console.ReadLine());
                    if (numberChoice < 0 || numberChoice > 6)
                    {
                        throw new Exception("FailInputNumber");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Vui lòng nhập đúng dạng số (VD: 1,2,3)!");
                    numberChoice = -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Vui lòng nhập đúng dạng số dương (0-6)!");
                    numberChoice = -1;
                }
            }
            return numberChoice;
        }

        public string InputString()
        {
            bool checkChoice = false;
            string stringChoice = "";
            while (checkChoice == false || string.IsNullOrEmpty(stringChoice))
            {
                try
                {
                    stringChoice = Console.ReadLine();
                    if (stringChoice.Equals("Y") || stringChoice.Equals("N"))
                    {
                        return stringChoice;
                    }
                    else
                    {
                        throw new Exception("FailInputString");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Vui lòng nhập lại!");
                    checkChoice = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Vui lòng nhập Y/N");
                    checkChoice = false;
                }

            }

            return stringChoice;

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
            ManageStudents manageStudents = new ManageStudents();
            switch (numberChoice)
            {
                case 1:
                    manageStudents.ShowStudents(manageStudents.GetStudents());
                    break;

                case 2:
                    manageStudents.AddStudents();

                    break;
                case 0:
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                    break;
            }
        }
    }
}
