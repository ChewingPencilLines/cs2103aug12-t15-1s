﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo;

namespace ToDo
{
    public static class CustomMessageBox
    {
        private static PopUpForm popUp = new PopUpForm();

        internal static void Show(string title,string subTitle)
        {
            popUp.SetTitle(title, subTitle);
            popUp.SetUserInput("");
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

    public static class AlertBox
    {
        private static AlertForm popUp = new AlertForm();

        internal static void Show(string alertText)
        {
            popUp.SetAlertText(alertText);
            popUp.ShowDialog();
        }
    }

    public static class FontBox
    {
        
        private static FontDialogToDo popUp = new FontDialogToDo();

        internal static void Show()
        {
            popUp.ShowDialog();
        }

    }
}
