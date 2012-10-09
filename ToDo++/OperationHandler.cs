using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace ToDo
{
    public abstract class OperationHandler
    {
        protected TaskList taskList;
        protected Stack<Operation> undoStack;
        protected Stack<Operation> redoStack;
        protected Storage xml;
        
        public OperationHandler()
        {
            taskList = new TaskList();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            xml = new Storage();
        }

        //Need to take in an instance of Operation to execute
        public abstract Result ExecuteOperation(Operation operation);
    }

    public class ExecuteAdd:OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {
                Task taskToAdd = operation.GetTask();
                taskList.Add(taskToAdd);

                xml.WriteXML(taskList);

                return Result.ADD_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Result.ERROR;
            }

        }
    }
    public class ExecuteDelete : OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {
              //  Task taskToDelete = operation.GetTask();
                taskList.RemoveAt(((OperationDelete)operation).index);

                xml.WriteXML(taskList);

                return Result.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Result.ERROR;
            }

        }
    }

    public class ExecuteModify : OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {
                Task taskRevised = operation.GetTask();
                taskList[((OperationModify)operation).oldTaskindex] = taskRevised;

                xml.WriteXML(taskList);

                return Result.MODIFY_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Result.ERROR;
            }

        }

       
    }

}
