using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationMarkAsDone : Operation
    {
        private int? index;
        private int? endindex;
        private string doneString;
        private DateTime? doneDate;
/*
        public OperationMarkAsDone(int[] indexRange)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenCommand.START_INDEX] - 1;
                this.endindex = indexRange[TokenCommand.END_INDEX] - 1;
            }
            this.doneString = null;
        }

        public OperationMarkAsDone(string doneString)
        {
            this.index = null;
            this.doneString = doneString;
        }

        public OperationMarkAsDone(DateTime? doneDate)
        {
            this.doneDate = doneDate;
        }
*/
        public OperationMarkAsDone(string doneString, int[] indexRange, DateTime? doneDate)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenCommand.START_INDEX] - 1;
                this.endindex = indexRange[TokenCommand.END_INDEX] - 1;
            }
            if (doneString == "") this.doneString = null;
            else this.doneString = doneString;
            this.doneDate = doneDate;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;
            if (doneDate != null)
            {
                response = null;
                List<Task> searchResults = SearchForTasks(taskList, doneString, false, doneDate, doneDate);
                foreach(Task taskToDone in searchResults)
                {
                    response += MarkAsDone(taskToDone, out successFlag) + '\n';
                }
            }
            else if (index.HasValue == false && doneString != null)
            {
                List<Task> searchResults = SearchForTasks(taskList, doneString);
                if (searchResults.Count == 1)
                {
                    response = MarkAsDone(lastListedTasks[0], out successFlag);
                }
                else response = GenerateDisplayString(searchResults);
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                return RESPONSE_INVALID_TASK_INDEX;
            }
            else if (doneString == null)
            {
                response = null;
                for (int? i = index; i <= endindex; i++)
                {
                    Task taskToMarkAsDone = lastListedTasks[i.Value];
                    response += MarkAsDone(taskToMarkAsDone, out successFlag) + '\n';
                }
            }
            else
            {
                return REPONSE_INVALID_COMMAND;
            }
            if (successFlag) TrackOperation();
            return response;
        }

        public override string Undo(List<Task> taskList, Storage storageXML)
        {
            Task task = undoTask.Pop();
            task.State = false;
            if (storageXML.MarkTaskAsDone(task))
            {
                successFlag = true;
                return String.Format(RESPONSE_MARKASUNDONE_SUCCESS, task.TaskName);
            }
            else
                return RESPONSE_XML_READWRITE_FAIL;
        }
    } 
}
