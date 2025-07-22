using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppManageStudent.Controller
{
    internal class HandleInputException
    {
        public int InputNumber()
        {
            bool numberCheck = false;
            int numberChoice = -1;
            while (numberCheck && numberChoice < 0)
            {
                try
                {
                    numberChoice = int.Parse(Console.ReadLine());
                    if (numberChoice < 0 || numberChoice > 6)
                    {
                        throw new ArgumentException("Vui lòng nhập số nguyên dương (0-6)!");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Vui lòng nhập đúng dạng số (VD: 1,2,3)!");
                    numberCheck = false;
                    numberChoice = -1;
                }
                catch (Exception ex) {
                    Console.WriteLine("Vui lòng nhập đúng dạng số dương (0-6)!");
                    numberCheck = false;
                    numberChoice = -1;
                }
            }
            return numberChoice;
        }
    }
}
