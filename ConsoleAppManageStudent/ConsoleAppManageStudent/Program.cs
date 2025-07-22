using ConsoleAppManageStudent.Controller;
using ConsoleAppManageStudent.View;

namespace ConsoleAppManageStudent;

class Program
{
    static void Main(string[] args)
    {
        MenuView menuView = new MenuView();
        HandleInputException handleInput = new HandleInputException();
        menuView.DisplayMenuView();

        int numberChoice = handleInput.InputNumber();
        while (numberChoice != 0) {
            menuView.SelectFunction(numberChoice);
            menuView.DisplayMenuView();
            numberChoice = handleInput.InputNumber();
        }
        
    }
}