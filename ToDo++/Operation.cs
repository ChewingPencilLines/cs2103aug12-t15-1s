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
            get
            {
                return index;
            }
        }

        public string DeleteString
        {
            get
            {
                return deleteString;
            }
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
        //Variables not needed for now
        public OperationUndo()
        { }

    }

    class OperationDone : Operation
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
