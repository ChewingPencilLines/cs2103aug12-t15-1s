using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    class OperationMarkAsDone : Operation
    {
        private int? index;
        private int? endindex;
        private string doneString;
        private DateTime? doneDate;
        private DateTime? doneDateEnd;
        private bool isAll;

        /// <summary>
        /// This is the base constructor for the MarkAsDone operation.
        /// There are three ways to execute this operation in the following priorities.
        /// By using a single date to mark all tasks on that date.
        /// by searching the task name against a string,
        /// and by using the current display index of the task.
        /// </summary>
        /// <param name="doneString">The name of the task to mark as done. Can be a substring of it.</param>
        /// <param name="indexRange">The display index of the task to be marked.</param>
        /// <param name="doneDate">The date in which to mark all tasks as done.</param>
        /// <param name="isAll">If this boolean is true, the current displayed tasks will all be marked as done.</param>
        /// <returns></returns>
        public OperationMarkAsDone(string doneString, int[] indexRange, DateTime? doneDate, bool isAll)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenIndexRange.START_INDEX] - 1;
                this.endindex = indexRange[TokenIndexRange.END_INDEX] - 1;
            }
            if (doneString == "") this.doneString = null;
            else this.doneString = doneString;
            this.doneDate = doneDate;
            if(doneDate != null)
                this.doneDateEnd = ((DateTime)doneDate).AddDays(1).AddMinutes(-1);
            else 
                this.doneDateEnd = null;
            this.isAll = isAll;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            this.storageIO = storageIO;
            Response response;
            if (doneDate != null)
            {
                response = null;
                List<Task> searchResults = SearchForTasks(taskList, doneString, isSpecific, false, doneDate, doneDateEnd);
                if (searchResults.Count == 0)
                {
                    response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                }
                else
                {
                    foreach (Task taskToDone in searchResults)
                    {
                        response = MarkAsDone(taskToDone);
                    }
                }
            }
            else if (index.HasValue == false && doneString != null)
            {
                List<Task> searchResults = SearchForTasks(taskList, doneString, isSpecific, true);
                if (searchResults.Count == 1)
                {
                    response = MarkAsDone(currentListedTasks[0]);
                }
                else if (searchResults.Count > 0)
                {
                    currentListedTasks = new List<Task>(searchResults);
                    string[] args;
                    this.SetArgumentsForFeedbackString(out args, doneString, doneDate, doneDateEnd, false, false);
                    response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, args);
                }
                else
                {
                    searchResults = SearchForTasks(taskList, doneString, isSpecific, false);
                    if (searchResults.Count > 0)
                    {
                        currentListedTasks = new List<Task>(searchResults);
                        string[] args;
                        this.SetArgumentsForFeedbackString(out args, doneString, doneDate, doneDateEnd, false, false);
                        response = new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationSearch), currentListedTasks, args);
                    }
                    else
                        response = new Response(Result.FAILURE, Format.DEFAULT, this.GetType());
                }
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT);
            }
            else if (index != null)
            {
                response = null;
                Debug.Assert(index <= endindex);
                Debug.Assert(endindex < currentListedTasks.Count);
                for (int? i = index; i <= endindex; i++)
                {
                    Task taskToMarkAsDone =  currentListedTasks[i.Value];
                    response = MarkAsDone(taskToMarkAsDone);
                }
            }
            else if (isAll)
            {
                foreach (Task task in currentListedTasks)
                {
                    response = MarkAsDone(task);
                    if (!response.IsSuccessful()) return response;
                }
                response = new Response(Result.SUCCESS_MULTIPLE, Format.DEFAULT, this.GetType(), currentListedTasks);
            }
            else
            {
                response = new Response(Result.INVALID_COMMAND, Format.DEFAULT, this.GetType(),  currentListedTasks);
            }
            if (response.IsSuccessful())
            {
                TrackOperation();
            }
            return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            redoTask.Push(task);
            task.DoneState = false;
            if (storageIO.MarkTaskAs(task, false))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo),  currentListedTasks);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            task.DoneState = true;
            if (storageIO.MarkTaskAs(task, true))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationUndo), currentListedTasks);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationRedo), currentListedTasks);
        }
    } 
}
