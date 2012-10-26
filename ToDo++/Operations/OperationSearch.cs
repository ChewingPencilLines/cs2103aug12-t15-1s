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

        public DateTime? EndTime
        {
            get { return endTime; }
            //set { endTime = value; }
        }

        public DateTime? StartTime
        {
            get { return startTime; }
            //set { startTime = value; }
        }

        public string SearchString
        {
            get { return searchString; }
        }

        public OperationSearch(string searchString, DateTime? startTime, DateTime? endTime)
        {
            this.searchString = searchString;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;
            int numberOfMatches;

            response = opHandler.Search(out numberOfMatches, taskList, searchString, false, startTime, endTime);
            return response;
        }
    }   
}
