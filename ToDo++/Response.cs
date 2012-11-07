using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public enum Result { SUCCESS, SUCCESS_MULTIPLE, FAILURE, INVALID_TASK, INVALID_COMMAND, XML_READWRITE_FAIL, TASK_MISSING_FROM_FILE, EXCEPTION_FAILURE };
    public enum Format { DEFAULT, NAME, DATE_TIME, DONE_STATE };    
    public class Response
    {
        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string STRING_ADD_SUCCESS = "Added new task \"{0}\" successfully.";
        const string STRING_ADD_FAILURE = "Failed to add task!";
        const string STRING_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string STRING_DELETE_SUCCESS_MULTI = "Deleted all tasks successfully.";
        const string STRING_DELETE_FAILURE = "No matching tasks found!";
        const string STRING_DELETE_INVALID_TASK = "No task to delete!";
        const string STRING_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string STRING_MODIFY_FAILURE = "Failed to modify task..!";
        const string STRING_DISPLAY_NO_TASK = "There are no tasks for display.";
        const string STRING_SEARCH_SUCCESS = "Showing {0}tasks{1}.";
        const string STRING_SORT_SUCCESS = "Sorting by {0}.";
        const string STRING_UNDO_SUCCESS = "Undid last operation.";
        const string STRING_UNDO_FAILURE = "Cannot undo last executed operation!";
        const string STRING_REDO_SUCCESS = "Redid last operation.";
        const string STRING_REDO_FAILURE = "Cannot redo last executed operation!";
        const string STRING_POSTPONE_SUCCESS = "Postponed task \"{0}\" successfully.";
        const string STRING_POSTPONE_SUCCESS_MULTI = "Postponed all tasks successfully.";
        const string STRING_POSTPONE_INVALID_TASK = "Cannot postpone floating tasks!";
        const string STRING_POSTPONE_FAILURE = "No matching task found!";
        const string STRING_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string STRING_MARKASDONE_SUCCESS_MULTI = "Successfully marked all tasks as done.";
        const string STRING_MARKASDONE_FAILURE = "No matching tasks found!";
        const string STRING_MARKASDONE_INVALID_TASK = "Cannot mark this task as done!";
        const string STRING_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone."; // Not implemented.
        const string STRING_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string STRING_CALLED_INVALID_TASK_INDEX = "Invalid task index!";
        const string STRING_INVALID_COMMAND = "Invalid command input!";
        const string STRING_UNDEFINED = "Undefined feedback string!";
        #endregion

        #region Parameter indices
        public const int MODIFY_PARAM_OLD_TASK = 0;
        public const int MODIFY_PARAM_NEW_TASK = 1;
        public const int MODIFY_PARAM_NUM = 2;
        public const int SEARCH_PARAM_DONE = 0;
        public const int SEARCH_PARAM_ALL = 0;
        public const int SEARCH_PARAM_SEARCH_STRING = 1;
        public const int SEARCH_PARAM_NUM = 2;
        #endregion

        Result result;
        Format formatType;
        string[] args;
        string feedbackString = null;
        List<Task> tasksToBeDisplayed;

        public Format FormatType
        {
            get { return formatType; }
        }
        public string FeedbackString
        {
            get { return feedbackString; }
        }        
        public List<Task> TasksToBeDisplayed
        {
            get { return tasksToBeDisplayed; }
        }
        public Response(
            Result resultType,
            Format formatType = Format.DEFAULT,
            Type operationType = null,
            List<Task> tasks = null,
            params string[] args
            )
        {        
            this.formatType = formatType;
            this.tasksToBeDisplayed = tasks;
            this.result = resultType;
            this.args = args;
            SetFeedbackString(resultType, operationType);            
        }

        public bool IsSuccessful()
        {
            if (result == Result.SUCCESS || result == Result.SUCCESS_MULTIPLE) return true;
            else return false;
        }

        private void SetFeedbackString(Result resultType, Type operationType)
        {
            try
            {
                switch (resultType)
                {
                    case Result.SUCCESS:
                        if (operationType == typeof(OperationAdd))
                            feedbackString = String.Format(STRING_ADD_SUCCESS, args);
                        if (operationType == typeof(OperationDelete))
                            feedbackString = String.Format(STRING_DELETE_SUCCESS, args);
                        if (operationType == typeof(OperationModify))
                            feedbackString = String.Format(STRING_MODIFY_SUCCESS, args);
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = String.Format(STRING_MARKASDONE_SUCCESS, args);
                        if (operationType == typeof(OperationSearch))
                            feedbackString = String.Format(STRING_SEARCH_SUCCESS, args);
                        if (operationType == typeof(OperationPostpone))
                            feedbackString = String.Format(STRING_POSTPONE_SUCCESS, args);
                        if (operationType == typeof(OperationUndo))
                            feedbackString = STRING_UNDO_SUCCESS;
                        if (operationType == typeof(OperationRedo))
                            feedbackString = STRING_REDO_SUCCESS;
                        if (operationType == typeof(OperationSort))
                            feedbackString = GetSortTypeString();
                        break;
                    case Result.SUCCESS_MULTIPLE:
                        if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_SUCCESS_MULTI;
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_SUCCESS_MULTI;
                        if (operationType == typeof(OperationPostpone))
                            feedbackString = STRING_POSTPONE_SUCCESS_MULTI;
                        break;
                    case Result.FAILURE:
                        if (operationType == typeof(OperationAdd))
                            feedbackString = STRING_ADD_FAILURE;
                        if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_FAILURE;
                        if (operationType == typeof(OperationModify))
                            feedbackString = STRING_MODIFY_FAILURE;
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_FAILURE;
                        if (operationType == typeof(OperationUndo))
                            feedbackString = STRING_UNDO_FAILURE;
                        if (operationType == typeof(OperationRedo))
                            feedbackString = STRING_REDO_FAILURE;
                        break;
                    case Result.INVALID_TASK:
                        if (operationType == typeof(OperationDisplayDefault) ||
                            operationType == typeof(OperationSearch))
                            feedbackString = STRING_DISPLAY_NO_TASK;
                        else if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_INVALID_TASK;
                        else if (operationType == typeof(OperationPostpone))
                            feedbackString = STRING_POSTPONE_INVALID_TASK;
                        else if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_INVALID_TASK;
                        else
                            feedbackString = STRING_CALLED_INVALID_TASK_INDEX;
                        break;
                    case Result.INVALID_COMMAND:
                        feedbackString = STRING_INVALID_COMMAND;
                        break;
                    case Result.XML_READWRITE_FAIL:
                        feedbackString = STRING_XML_READWRITE_FAIL;
                        break;
                    default:
                        throw new Exception("Type of Result in invalid!");
                }
            }
            catch (FormatException e)
            {
                // change exception to log.
                feedbackString = "Invalid number of parameters called!";
                resultType = Result.EXCEPTION_FAILURE;
            }
            if (feedbackString == null)
            {
                feedbackString = STRING_UNDEFINED;
            }
        }

        private string GetSortTypeString()
        {
            string sortTypeString = "";
            switch (formatType)
            {
                case Format.NAME:
                    sortTypeString = "Name";
                    break;
                case Format.DATE_TIME:
                    sortTypeString = "Date";
                    break;
                case Format.DONE_STATE:
                    sortTypeString = "[DONE] state";
                    break;
                default:
                    sortTypeString = "the same as before";
                    break;
            }
            return String.Format(STRING_SORT_SUCCESS, sortTypeString);
        }
    }
}
