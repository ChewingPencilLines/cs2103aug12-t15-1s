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
        private SearchType searchType;

        public OperationSearch(
            string searchString,
            DateTime? startTime,
            DateTime? endTime,
            DateTimeSpecificity isSpecific,
            SearchType searchType,
            SortType sortType)
            : base(sortType)
        {
            this.searchString = searchString;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
            this.searchType = searchType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            List<Task> searchResults = SearchForTasks(searchString, isSpecific, false, startTime, endTime, searchType);
            currentListedTasks = new List<Task>(searchResults);

            string[] criteria;
            SetArgumentsForFeedbackString(out criteria, searchString, startTime, endTime, searchType);

            return new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks, criteria);
        }

    }   
}
