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
        protected XML xml;
        
        public OperationHandler()
        {
            taskList = new TaskList();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            xml = new XML();
        }

        //Need to take in an instance of Operation to execute
        abstract public Responses ExecuteOperation(Operation operation)
        {
            return Responses.ERROR;
        }
    }

    public class ExecuteAdd:OperationHandler
    {
        public override Responses ExecuteOperation(OperationAdd operation)
        {
            try
            {
                Task taskToAdd = operation.GetTask();
                taskList.Add(taskToAdd);

                xml.WriteXML(taskList);

                return Responses.ADD_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Responses.ERROR;
            }

        }
    }
    public class ExecuteDelete : OperationHandler
    {
        public override Responses ExecuteOperation(OperationDelete operation)
        {
            try
            {
              //  Task taskToDelete = operation.GetTask();
                taskList.RemoveAt(operation.index);

                xml.WriteXML(taskList);

                return Responses.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Responses.ERROR;
            }

        }
    }

    public class ExecuteModify : OperationHandler
    {
        public override Responses ExecuteOperation(OperationModify operation)
        {
            try
            {
                Task taskRevised = operation.GetTask();
                taskList[operation.oldTaskindex] = taskRevised;

                xml.WriteXML(taskList);

                return Responses.MODIFY_SUCCESS;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Responses.ERROR;
            }

        }

       
    }

}
