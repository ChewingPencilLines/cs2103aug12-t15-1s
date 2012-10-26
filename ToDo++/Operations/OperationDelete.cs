using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationDelete : Operation
    {

        private int? index;
        private string deleteString;

        public int? Index
        {
            get { return index; }
        }

        public string DeleteString
        {
            get { return deleteString; }
        }

        public OperationDelete(int index)
        {
            this.index = index - 1;
            this.deleteString = null;
        }

        public OperationDelete(string deleteString)
        {
            this.index = null;
            this.deleteString = deleteString;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;

            if (index.HasValue == false && deleteString != null)
            {
                int numberOfMatches;
                response = opHandler.Search(out numberOfMatches, taskList, deleteString, true);
                if (numberOfMatches == 0)
                {
                    //check substring
                    response = opHandler.Search(out numberOfMatches, taskList, deleteString, false);
                    if (numberOfMatches == 0)
                        response = RESPONSE_DELETE_FAILURE;
                }
                else if (numberOfMatches == 1)
                {
                    response = opHandler.Delete(opHandler.LastListedTasks[0], taskList, out successFlag);
                }
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                return RESPONSE_INVALID_TASK_INDEX;
            }
            else if (deleteString == null)
            {
                Task taskToDelete = opHandler.LastListedTasks[index.Value];
                response = opHandler.Delete(taskToDelete, taskList, out successFlag);
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
