﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public abstract class Operation
    {
        public Operation()
        {

        }
        
        public abstract Task GetTask();
    }

    public class OperationAdd:Operation
    {
        private Task newTask;

        public OperationAdd(Task setTask)
        {
            newTask = setTask;
        }

        public override Task GetTask()
        {
            return newTask;
        }
    }

    public class OperationSearch:Operation
    {
        //Think of Variables
        public OperationSearch()
        {  }

        public override Task GetTask() { return null;}
    }

    public class OperationDelete:Operation
    {
        public int index;

        public OperationDelete(int DeleteIndex)
        {
            index = DeleteIndex;
        }

       // public override Task GetTask() { return null; }
    }

    public class OperationModify:Operation
    {
        public int oldTaskindex;
        private Task newTask;

        public OperationModify(int Previous, Task Revised)
        {
            oldTaskindex = Previous;
            newTask = Revised;
        }

        public override Task GetTask() 
        {
            return newTask;
        }
    }

    public class OperationUndo:Operation
    {
        //Variables not needed for now
        public OperationUndo()
        { }

        public override Task GetTask() { return null; }
    }


}
