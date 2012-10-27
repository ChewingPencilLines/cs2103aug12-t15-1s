using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    enum Response { ADD_SUCCESS, ADD_FAIL, DELETE_SUCCESS, MODIFY_SUCCESS, UNDO_SUCCESS };

    class OperationResult
    {
        private bool successFlag;
        private string feedbackString;        

        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        static string STRING_ADD_SUCCESS = "Added {0} successfully.";
        static string STRING_ADD_FAIL = "Failed to add task!";
        static string STRING_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        static string STRING_MODIFY_SUCCESS = "Modified task successfully.";
        static string STRING_UNDO_SUCCESS = "Removed task successfully.";
        #endregion

        public OperationResult(Response type, string param = "")
        {
            switch(type)
            {
                case Response.ADD_SUCCESS:
                    feedbackString = STRING_ADD_SUCCESS;
                    successFlag = true;
                    break;
                case Response.ADD_FAIL:
                    feedbackString = STRING_ADD_FAIL;
                    successFlag = false;
                    break;
                case Response.DELETE_SUCCESS:
                    feedbackString = STRING_DELETE_SUCCESS;
                    successFlag = true;
                    break;
                case Response.MODIFY_SUCCESS:
                    feedbackString = STRING_MODIFY_SUCCESS;
                    successFlag = true;
                    break;
                case Response.UNDO_SUCCESS:
                    feedbackString = STRING_UNDO_SUCCESS;
                    successFlag = true;
                    break;
                default: throw new Exception("Type of Response in invalid!");
            }
        }

        public bool IsSuccess()
        {
            return successFlag;
        }

        public string GetFeedbackString()
        {
            return feedbackString;
        }
    }
}
