using ConsoleAppManageStudent.Function;
using ConsoleAppManageStudent.HandleException;
using System.Numerics;
using System.Text;

namespace ConsoleAppManageStudent;

class Program
{
    static void Main(string[] args)
    {
        HandleMenu handle = new HandleMenu();
        handle.ShowMenu();

        int numberChoice = handle.InputNumber();
        handle.handleContinueProgram(numberChoice);

        

    }
}