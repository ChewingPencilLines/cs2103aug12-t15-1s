using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public enum Result { SUCCESS, FAILURE, INVALID_TASK, INVALID_COMMAND, XML_READWRITE_FAIL };
    public enum Format { DEFAULT, NAME, DATETIME };
    public class Response
    {
        // ******************************************************************
        // Feedback Strings
        // ******************************************************************
        #region Feedback Strings
        const string STRING_ADD_SUCCESS = "Added \"{0}\" successfully.";
        const string STRING_ADD_FAILURE = "Failed to add task!";
        const string STRING_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string STRING_DELETE_FAILURE = "No matching task found!";
        const string STRING_DELETE_INVALID_TASK = "Task has already been deleted!";
        const string STRING_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        const string STRING_DISPLAY_NO_TASK = "There are no tasks for display.";
        const string STRING_UNDO_SUCCESS = "Undid last operation.";
        const string STRING_UNDO_FAILURE = "Cannot undo last executed operation!";
        const string STRING_REDO_SUCCESS = "Redid last operation.";
        const string STRING_REDO_FAILURE = "Cannot redo last executed operation!";
        const string STRING_POSTPONE_SUCCESS = "Postponed task \"{0}\" successfully.";
        const string STRING_POSTPONE_INVALID_TASK = "Cannot postpone floating tasks!";
        const string STRING_POSTPONE_FAILURE = "No matching task found!";
        const string STRING_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string STRING_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone.";
        const string STRING_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string STRING_CALLED_INVALID_TASK_INDEX = "Invalid task index!";
        const string STRING_INVALID_COMMAND = "Invalid command input!";
        const string STRING_UNDEFINED = "Undefined feedback string!";
        #endregion

        Format formatType;
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

        public Response(Result resultType, Format formatType, Type operationType = null, List<Task> tasks = null)
        {
            this.formatType = formatType;
            this.tasksToBeDisplayed = tasks;
            SetFeedbackString(resultType, operationType);            
        }

        private void SetFeedbackString(Result resultType, Type operationType)
        {
            switch (resultType)
            {
                case Result.SUCCESS:
                    if (operationType == typeof(OperationAdd))
                        feedbackString = STRING_ADD_SUCCESS;
                    if (operationType == typeof(OperationDelete))
                        feedbackString = STRING_DELETE_SUCCESS;
                    if (operationType == typeof(OperationModify))
                        feedbackString = STRING_MODIFY_SUCCESS;
                    if (operationType == typeof(OperationMarkAsDone))
                        feedbackString = STRING_MARKASDONE_SUCCESS;
                    if (operationType == typeof(OperationPostpone))
                        feedbackString = STRING_POSTPONE_SUCCESS;
                    if (operationType == typeof(OperationUndo))
                        feedbackString = STRING_UNDO_SUCCESS;
                    if (operationType == typeof(OperationRedo))
                        feedbackString = STRING_REDO_SUCCESS;
                    break;
                case Result.FAILURE:
                    if (operationType == typeof(OperationAdd))
                        feedbackString = STRING_ADD_FAILURE;
                    if (operationType == typeof(OperationDelete))
                        feedbackString = STRING_DELETE_FAILURE;
                    //if (operationType == typeof(OperationModify))
                    //  feedbackString = STRING_MODIFY_FAILURE;
                    //if (operationType == typeof(OperationMarkAsDone))
                    //feedbackString = STRING_MARKASDONE_FAILURE;
                    if (operationType == typeof(OperationUndo))
                        feedbackString = STRING_UNDO_FAILURE;
                    if (operationType == typeof(OperationRedo))
                        feedbackString = STRING_REDO_FAILURE;
                    break;
                case Result.INVALID_TASK:
                    if (operationType == typeof(OperationDisplay) ||
                        operationType == typeof(OperationSearch))
                        feedbackString = STRING_DISPLAY_NO_TASK;
                    else if (operationType == typeof(OperationDelete))
                        feedbackString = STRING_DELETE_INVALID_TASK;
                    else if (operationType == typeof(OperationPostpone))
                        feedbackString = STRING_POSTPONE_INVALID_TASK;
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
            if (feedbackString == null)
            {
                feedbackString = STRING_UNDEFINED;
            }
        }
    }
}
