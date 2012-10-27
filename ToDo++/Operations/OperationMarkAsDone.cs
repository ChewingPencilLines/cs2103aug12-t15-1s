using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationMarkAsDone : Operation
    {
        private int? index;
        private string doneString;

        public int? Index
        {
            get { return index; }
        }

        public string DoneString
        {
            get { return doneString; }
        }

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
            OperationHandler opHandler = new OperationHandler(storageXML);
            opHandler.TrackOperation(this);
            string response;
            if (index.HasValue == false && doneString != null)
            {
                List<Task> searchResults = opHandler.Search(taskList, doneString);
                if (searchResults.Count == 1)
                {
                    response = opHandler.MarkAsDone(opHandler.LastListedTasks[0], out successFlag);
                }
                else response = opHandler.Display(searchResults);
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                return RESPONSE_INVALID_TASK_INDEX;
            }
            else if (doneString == null)
            {
                Task taskToMarkAsDone = opHandler.LastListedTasks[index.Value];
                response = opHandler.MarkAsDone(taskToMarkAsDone, out successFlag);
            }
            else
            {
                return REPONSE_INVALID_COMMAND;
            }
            if (successFlag) opHandler.TrackOperation(this);
            return response;
        }
    } 
}
