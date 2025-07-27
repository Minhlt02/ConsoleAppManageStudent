using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStudentConsole.HandleException
{
    public class HandleFormatDate
    {
        public DateTime HandleFormatBirthday()
        {
            while (true)
            {
                string? birthday = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(birthday))
                {
                    Console.WriteLine("Ngày sinh không được để trống!");
                    continue; // Yêu cầu nhập lại
                }
                bool isValid = DateTime.TryParseExact(birthday, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthdayFormat);
                if (isValid)
                {
                    return birthdayFormat;
                }
                Console.WriteLine("Vui lòng nhập đúng định dạng dd/MM/yyyy");
            }

        }
    }
}
