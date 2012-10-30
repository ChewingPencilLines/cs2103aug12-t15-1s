using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationSearch : Operation
    {
        private string searchString = "";
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;

        public OperationSearch(string searchString, DateTime? startTime, DateTime? endTime, DateTimeSpecificity isSpecific)
        {
            this.searchString = searchString;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            this.storageXML = storageXML;
            string response;

            List<Task> searchResults = SearchForTasks(taskList, searchString, false, startTime, endTime);
            response = GenerateDisplayString(searchResults);
            return response;
        }
    }   
}
