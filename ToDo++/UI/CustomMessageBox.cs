using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public static class CustomMessageBox
    {
        private static PopUpForm popUp = new PopUpForm();

        internal static void Show(string title,string subTitle)
        {
            popUp.title = title;
            popUp.subTitle = subTitle;
            popUp.userInput = "";
            popUp.ShowDialog();
        }

        internal static bool ValidData()
        {
            if (popUp.userInput == "")
                return false;
            else
                return true;
        }

        internal static string GetInput()
        {
            return popUp.userInput;
        }

    }
}
