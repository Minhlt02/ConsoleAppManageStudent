using ManageStudentConsole.HandleException;

namespace ManageStudentConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HandleMenu handle = new HandleMenu();
            handle.ShowMenu();

            int numberChoice = handle.InputNumber();
            handle.handleContinueProgram(numberChoice);
        }
    }
}
