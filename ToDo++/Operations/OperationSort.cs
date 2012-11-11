using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class OperationSort : Operation
    {
        public OperationSort(SortType sortType)
            :base (sortType)
        {
            this.sortType = sortType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response;
            
            // sorting is done On-The-Fly in TaskListViewControl.
            if(sortType == SortType.DEFAULT)
                response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);
            else
                response = new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks);
            return response;
        }
    }
}
