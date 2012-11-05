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
                List<Task> searchResults = SearchForTasks(taskList, doneString, isSpecific, false, doneDate, ((DateTime)doneDate).AddDays(1).AddMinutes(-1));
                foreach(Task taskToDone in searchResults)
                {
                    response = MarkAsDone(taskToDone);
                }
            }
            else if (index.HasValue == false && doneString != null)
            {
                List<Task> searchResults = SearchForTasks(taskList, doneString, isSpecific);
                if (searchResults.Count == 1)
                {
                    response = MarkAsDone(currentListedTasks[0]);
                }
                else
                {
                    currentListedTasks = new List<Task>(searchResults);
                    response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
                }
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType(),  currentListedTasks);
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
               // return REPONSE_INVALID_COMMAND;
                return new Response(Result.INVALID_COMMAND, Format.DEFAULT, this.GetType(),  currentListedTasks);
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
            if (storageIO.MarkTaskAsDone(task))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(),  currentListedTasks);
              //  return String.Format(RESPONSE_MARKASUNDONE_SUCCESS, task.TaskName);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, this.GetType(),  currentListedTasks);
                //return RESPONSE_XML_READWRITE_FAIL;
        }

        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            Task task = redoTask.Pop();
            undoTask.Push(task);
            task.DoneState = true;
            if (storageIO.MarkTaskAsDone(task))
            {
                return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(),  currentListedTasks);
               // return String.Format(RESPONSE_MARKASUNDONE_SUCCESS, task.TaskName);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, this.GetType(),  currentListedTasks);
              //  return RESPONSE_XML_READWRITE_FAIL;
        }
    } 
}
