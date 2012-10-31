using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    enum Result { SUCCESS, FAILURE, ALREADY_DONE, INVALID_TASK, INVALID_COMMAND, XML_READWRIE_FAIL };
    
    class Response
    {
        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string STRING_ADD_SUCCESS = "Added \"{0}\" successfully.";
        const string STRING_ADD_FAILURE = "Failed to add task!";
        const string STRING_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string STRING_DELETE_FAILURE = "No matching task found!";
        const string STRING_DELETE_ALREADY = "Task has already been deleted!";
        const string STRING_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string STRING_DISPLAY_NOTASK = "There are no tasks for display.";
        const string STRING_UNDO_SUCCESS = "Removed task successfully.";
        const string STRING_UNDO_FAILURE = "Cannot undo last executed task!";
        const string STRING_POSTPONE_SUCCESS = "Postponed task \"{0}\" successfully.";
        const string STRING_POSTPONE_FAIL = "Cannot postpone floating tasks!";
        const string STRING_POSTPONE_FAILURE = "No matching task found!";
        const string STRING_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string STRING_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone.";
        const string STRING_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string STRING_INVALID_TASK_INDEX = "Invalid task index!";
        const string STRING_INVALID_COMMAND = "Invalid command input!";
        #endregion

        string feedbackString;
        List<Task> tasksToBeDisplayed;

        public Response(CommandType operationType, Result resultType, string param = "")
        {
            switch (resultType)
            {
                default: throw new Exception("Type of Response in invalid!");
            }
        }

        public string GetFeedbackString()
        {
            return feedbackString;
        }
    }
}
