using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for Operation
    // ******************************************************************
    #region Abstract definition for Operation
    public abstract class Operation
    {
    }
    #endregion

    // ******************************************************************
    // Definition for five different operation
    // ******************************************************************

    #region Definition for five different operation
    public class OperationAdd : Operation
    {
        private Task newTask;

        public OperationAdd(Task setTask)
        {
            newTask = setTask;
        }

        public Task GetTask()
        {
            return newTask;
        }
    }

    public class OperationDelete : Operation
    {
        private int index;

        public int Index
        {
            get { return index; }
        }

        public OperationDelete(int index)
        {
            this.index = index - 1;
        }
    }

    public class OperationDisplay : Operation
    {

    }

    public class OperationSearch : Operation
    {
        private string searchString = "";

        public OperationSearch(string searchString)
        {
            this.searchString = searchString;
        }

        public string GetSearchString() { return searchString; }

    }
    
    public class OperationModify : Operation
    {
        public int oldTaskindex;
        private Task newTask;

        public OperationModify(int Previous, Task Revised)
        {
            oldTaskindex = Previous - 1;
            newTask = Revised;
        }
    }

    public class OperationUndo : Operation
    {
        //Variables not needed for now
        public OperationUndo()
        { }

    }

    public class OperationDone : Operation
    {
        private int index;

        public int Index
        {
            get { return index; }
        }

        public OperationDone(int index)
        {
            this.index = index - 1;
        }

    } 

    #endregion


}
