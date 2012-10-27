using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    class OperationMarkAsDone : Operation
    {
        private int? index;
        private string doneString;

        public OperationMarkAsDone(int[] indexRange)
        {
            if (indexRange == null) this.index = null;
            else this.index = indexRange[TokenCommand.START_INDEX] - 1;
            this.doneString = null;
        }

        public OperationMarkAsDone(string doneString)
        {
            this.index = null;
            this.doneString = doneString;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;
            if (index.HasValue == false && doneString != null)
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
                Task taskToMarkAsDone = lastListedTasks[index.Value];
                response = MarkAsDone(taskToMarkAsDone, out successFlag);
            }
            else
            {
                return REPONSE_INVALID_COMMAND;
            }
            if (successFlag) TrackOperation();
            return response;
        }
    } 
}
