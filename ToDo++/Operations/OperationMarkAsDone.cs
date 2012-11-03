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
        public OperationMarkAsDone(string doneString, int[] indexRange, DateTime? doneDate)
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
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response;
            if (doneDate != null)
            {
                response = null;
                List<Task> searchResults = SearchForTasks(taskList, doneString, false, doneDate, doneDate);
                foreach(Task taskToDone in searchResults)
                {
                    response = MarkAsDone(taskToDone, out successFlag);
                }
            }
            else if (index.HasValue == false && doneString != null)
            {
                List<Task> searchResults = SearchForTasks(taskList, doneString);
                if (searchResults.Count == 1)
                {
                    response = MarkAsDone(currentListedTasks[0], out successFlag);
                }
                else
                {
                    currentListedTasks = searchResults;
                    response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(), currentListedTasks);
                }
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                response = new Response(Result.INVALID_TASK, Format.DEFAULT, this.GetType(),  currentListedTasks);
            }
            else if (doneString == null)
            {
                response = null;
                Debug.Assert(index <= endindex);
                Debug.Assert(endindex < taskList.Count);
                for (int? i = index; i <= endindex; i++)
                {
                    Task taskToMarkAsDone =  currentListedTasks[i.Value];
                    response = MarkAsDone(taskToMarkAsDone, out successFlag);
                }
            }
            else
            {
               // return REPONSE_INVALID_COMMAND;
                return new Response(Result.INVALID_COMMAND, Format.DEFAULT, this.GetType(),  currentListedTasks);
            }
            if (response.IsSuccessful()) TrackOperation();
            return response;
        }

        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            Task task = undoTask.Pop();
            redoTask.Push(task);
            task.DoneState = false;
            if (storageIO.MarkTaskAsDone(task))
            {
                successFlag = true;
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
                successFlag = true;
                return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(),  currentListedTasks);
               // return String.Format(RESPONSE_MARKASUNDONE_SUCCESS, task.TaskName);
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, this.GetType(),  currentListedTasks);
              //  return RESPONSE_XML_READWRITE_FAIL;
        }
    } 
}
