﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    class OperationSort : Operation
    {
        public OperationSort()
        { }

        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
           // string response;

            List<Task> sortedTasks = (from task in  currentListedTasks
                               orderby task.TaskName
                               select task).ToList();
           // response = GenerateDisplayString(sortedTasks);

            TrackOperation();

            return new Response(Result.SUCCESS, Format.NAME, this.GetType(),  currentListedTasks);
           // return response;
        }
    }
}
