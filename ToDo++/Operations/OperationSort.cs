using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class OperationSort : Operation
    {
        private SortType? sortType;

        public OperationSort(SortType? sortType)
        {
            this.sortType = sortType;
        }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            List<Task> sortedTasks;
            Response response;
            switch (sortType)
            {
                case SortType.NAME:
                    sortedTasks = (from task in currentListedTasks
                               orderby task.TaskName
                               select task).ToList();
                    response = new Response(Result.SUCCESS, Format.NAME, this.GetType(), sortedTasks);
                    break;
                case SortType.DATE:
                    //@alice->ivan: change sortedtasks correspondingly
                    sortedTasks = currentListedTasks; 
                    response=new Response(Result.SUCCESS,Format.DATE, this.GetType(), sortedTasks);
                    break;
                case SortType.DONESTATE:
                    sortedTasks = currentListedTasks; 
                    response=new Response(Result.SUCCESS,Format.DONE_STATE, this.GetType(), sortedTasks);
                    break;
                case SortType.DEFAULT:
                default:
                    sortedTasks = (from task in currentListedTasks
                               orderby task.ID
                               select task).ToList();
                    response=new Response(Result.SUCCESS,Format.DEFAULT, this.GetType(), sortedTasks);
                    break;
            }
            TrackOperation();
            return response;
        }
    }
}
