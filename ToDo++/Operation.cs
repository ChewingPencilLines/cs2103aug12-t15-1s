using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public enum OperationType
    {
        ADD_FLOATING,
        ADD_EVENT,
        ADD_DEADLINE,
        DISPLAY_ALL,
        DISPLAY_TIME,
        DISPLAY_NAME,
        DISPLAY_TYPE,
        UPDATE_NAME,
        UPDATE_TIME,
        DELETE_TASK,
        DELETE_DATE,
        UNDO,
        REDO,
    }

    public abstract class Operation
    {
        private OperationType operationType;

        public void SetOperationType(OperationType opType)
        {
            operationType=opType;
        }

        public OperationType GetOperationType()
        {
            return operationType;
        }

        public abstract Task GetTask();
    }

    public class OperationAdd:Operation
    {
        private Task newTask;

        public OperationAdd(Task setTask,OperationType setOpType)
        {
            SetOperationType(setOpType);
            newTask = setTask;
        }

        public override Task GetTask()
        {
            return newTask;
        }
    }

    class OperationDisplay:Operation
    {
        //Think of Variables
        public override Task GetTask() { return null;}
    }

    class OperationDelete:Operation
    {
        int index;
        public override Task GetTask() { return null; }
    }

    class OperationUpdate:Operation
    {
        private Task newTask;
        private Task oldTask;

        public override Task GetTask() { return null; }
    }

    class OperationUndoRedo:Operation
    {
        //Variables not needed for now
        public override Task GetTask() { return null; }
    }


}
