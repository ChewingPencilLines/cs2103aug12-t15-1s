using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationDelete : Operation
    {

        // @ivan -> alice: not good enough. needs to be able to delete by range of index / dates.
        // work on letting it search by dates first. i will get range of index detected soon.
        // i will also catch the "all" keyword, letting u allow all search hits to be deleted immediately.

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

            List<Task> searchResults;
            if (index.HasValue == false && deleteString != null)
            {
                searchResults = opHandler.Search(taskList, deleteString, true);
                if (searchResults.Count == 0)
                {
                    //check substring
                    searchResults = opHandler.Search(taskList, deleteString, false);
                    if (searchResults.Count == 0)
                        response = RESPONSE_DELETE_FAILURE;
                    else response = opHandler.Display(searchResults);
                }
                else if (searchResults.Count == 1)
                {
                    response = opHandler.Delete(searchResults[0], taskList, out successFlag);
                }
                else response = opHandler.Display(searchResults);
            }
            else if (index < 0 || index > taskList.Count - 1)
            {
                return RESPONSE_INVALID_TASK_INDEX;
            }
            else if (deleteString == null)
            {
                Task taskToDelete = opHandler.LastListedTasks[index.Value];
                if (taskToDelete == null)
                    return RESPONSE_DELETE_ALREADY;
                else response = opHandler.Delete(taskToDelete, taskList, out successFlag);
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
