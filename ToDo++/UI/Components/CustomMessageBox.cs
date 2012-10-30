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

        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
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

        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }
    }

    public static class FontBox
    {
        
        private static FontDialogToDo popUp = new FontDialogToDo();

        internal static void Show(bool font, bool size, bool color)
        {
            popUp.EnableDisableControls(font, size, color);
            popUp.ShowDialog();
        }

        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }

    }
}
