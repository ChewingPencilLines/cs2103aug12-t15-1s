using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class Response
    {
        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string RESPONSE_ADD_SUCCESS = "Added \"{0}\" successfully.";
        const string RESPONSE_ADD_FAILURE = "Failed to add task!";
        const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string RESPONSE_DELETE_FAILURE = "No matching task found!";
        const string RESPONSE_DELETE_ALREADY = "Task has already been deleted!";
        const string RESPONSE_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string RESPONSE_DISPLAY_NOTASK = "There are no tasks for display.";
        const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        const string RESPONSE_UNDO_FAILURE = "Cannot undo last executed task!";
        const string RESPONSE_POSTPONE_SUCCESS = "Postponed task \"{0}\" successfully.";
        const string RESPONSE_POSTPONE_FAIL = "Cannot postpone floating tasks!";
        const string RESPONSE_POSTPONE_FAILURE = "No matching task found!";
        const string RESPONSE_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string RESPONSE_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone.";
        const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        const string REPONSE_INVALID_COMMAND = "Invalid command input!";
        #endregion


    }
}
