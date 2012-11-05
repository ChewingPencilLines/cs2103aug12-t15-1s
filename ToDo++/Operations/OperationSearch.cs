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
        private bool isAll;
        private bool searchForIsDone = false;

        public OperationSearch(string searchString, DateTime? startTime, DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, bool searchForIsDone)
        {
            this.searchString = searchString;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
            this.isAll = isAll;
            this.searchForIsDone = searchForIsDone;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            List<Task> searchResults = SearchForTasks(taskList, searchString, isSpecific, false, startTime, endTime, searchForIsDone);
            currentListedTasks = new List<Task>(searchResults);

            string[] criteria;
            SetArgumentsForFeedbackString(out criteria, searchString, startTime, endTime, searchForIsDone, isAll);

            return new Response(Result.SUCCESS, Format.DEFAULT, this.GetType(),  currentListedTasks, criteria);
        }

    }   
}
