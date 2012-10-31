using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{
    public static class UserInputBox
    {
        private static UserInputForm popUp = new UserInputForm();

        /// <summary>
        /// Shows the UserInputBox
        /// </summary>
        /// <param name="title">Specify the title</param>
        /// <param name="subTitle">Specify the subtitle</param>
        internal static void Show(string title,string subTitle)
        {
            popUp.SetTitle(title, subTitle);
            popUp.SetUserInput("");
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <returns>Returns true if data was actually entered</returns>
        internal static bool ValidData()
        {
            if (popUp.userInput == "")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns what the user entered. to be used with ValidData()
        /// </summary>
        /// <returns>Returns user input</returns>
        internal static string GetInput()
        {
            return popUp.userInput;
        }

        /// <summary>
        /// Sets This UserInputBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }

    }

    public static class AlertBox
    {
        private static AlertForm popUp = new AlertForm();

        /// <summary>
        /// Shows the AlertBox
        /// </summary>
        /// <param name="alertText">Specify the alert text</param>
        internal static void Show(string alertText)
        {
            popUp.SetAlertText(alertText);
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Sets This AlertBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }
    }

    public static class FontBox
    {

        private static FontDialogToDo popUp = new FontDialogToDo();

        /// <summary>
        /// Pres-Set the required Font,Size,Color
        /// </summary>
        /// <param name="font">Specify the font</param>
        /// <param name="size">Specify the size</param>
        /// <param name="color">Specify the color</param>
        internal static void InitializeOptions(string font, int size, Color color)
        {
            popUp.InitializeOptions(font, size, color);
        }

        /// <summary>
        /// Getters for Font Size and Color
        /// </summary>
        internal static int GetSize() { return popUp.GetSize(); }
        internal static string GetFont() { return popUp.GetFont(); }
        internal static Color GetColor() { return popUp.GetColor(); }

        /// <summary>
        /// Checks if the Okay Button was Hit or Not
        /// </summary>
        /// <returns>Returns true if Okay was hit, false if Cancel as hit</returns>
        internal static bool ConfirmHit()
        {
            if (popUp.CheckValidData())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Displays the FontBox
        /// </summary>
        /// <param name="font">Enable/Disable font</param>
        /// <param name="size">Enable/Disable size</param>
        /// <param name="color">Enable/Disable color</param>
        internal static void Show(bool font, bool size, bool color)
        {
            popUp.EnableDisableControls(font, size, color);
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Sets This FontBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }

    }
}
