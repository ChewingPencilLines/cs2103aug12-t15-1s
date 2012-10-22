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
    // Definition for derived operations
    // ******************************************************************

    #region Definition for derived operations
    class OperationAdd : Operation
    {
        private Task newTask;

        internal OperationAdd(Task setTask)
        {
            newTask = setTask;
        }

        internal Task NewTask
        {
            get {return newTask;}
        }
    }

    class OperationDelete : Operation
    {

        private int? index;
        private string deleteString;

        public int? Index
        {
            get {  return index;  }
        }

        public string DeleteString
        {
            get {  return deleteString; }
        }

        public OperationDelete(int index)
        {
            this.index = index - 1;
            this.deleteString = null;
        }

        public OperationDelete(string deleteString)
        {
            this.index = null;
            this.deleteString = deleteString;
        }
    }

    class OperationDisplay : Operation
    {
        public OperationDisplay()
        { }
    }

    class OperationSort : Operation
    {
        public OperationSort()
        { }
    }

    class OperationPostpone : Operation
    { 
        private int? oldIndex;
        private Task postponedTask;

        public int? OldIndex
        {
            get { return oldIndex; }
        }

        public Task PostponedTask
        {
            get { return postponedTask; }
        }

        public OperationPostpone(int Previous, Task Postponed)
        {
            oldIndex = Previous - 1;
            postponedTask = Postponed;
        }

        public OperationPostpone()
        {
            oldIndex = null;
            postponedTask = null;
        }
    }

    class OperationSearch : Operation
    {
        private string searchString = "";

        public OperationSearch(string searchString)
        {
            this.searchString = searchString;
        }

        public string SearchString
        {
            get { return searchString; }
        }

    }
    
    class OperationModify : Operation
    {
        private int? oldIndex;
        private Task newTask;

        public int? OldIndex
        {
            get { return oldIndex; }
        }

        public Task NewTask
        {
            get { return newTask; }
        }

        public OperationModify(int Previous, Task Revised)
        {
            oldIndex = Previous - 1;
            newTask = Revised;
        }

        public OperationModify()
        {
            oldIndex = null;
            newTask = null;
        }
    }

    class OperationUndo : Operation
    {
        public OperationUndo()
        { }

    }
       
    class OperationRedo : Operation
    {
        public OperationRedo()
        { }
    }

    class OperationDone : Operation
    {
        private int? index;
        private string doneString;

        public int? Index
        {
            get { return index; }
        }

        public string DoneString
        {
            get{ return doneString; }
        }

        public OperationDone(int index)
        {
            this.index = index - 1;
            this.doneString = null;
        }

        public OperationDone(string doneString)
        {
            this.index = null;
            this.doneString = doneString;
        }

    } 

    #endregion


}
