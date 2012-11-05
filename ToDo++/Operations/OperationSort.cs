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
            Response response;
            switch (sortType)
            {
                // sorting is done On-The-Fly in display control.
                case SortType.NAME:                    
                    response = new Response(Result.SUCCESS, Format.NAME, this.GetType());
                    break;
                case SortType.DONE_STATE:
                    response=new Response(Result.SUCCESS,Format.DONE_STATE, this.GetType());
                    break;
                case SortType.DEFAULT:
                default:
                    response = new Response(Result.SUCCESS, Format.DEFAULT, this.GetType());
                    break;
            }
            return response;
        }
    }
}
