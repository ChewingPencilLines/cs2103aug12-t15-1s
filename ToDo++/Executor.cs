using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ToDo
{
    public class ExecuteAdd : OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {
                Task taskToAdd = ((OperationAdd)operation).GetTask();
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
                int Index= ((OperationDelete)operation).index;
                Task taskToDelete = taskList[Index];
                pastTask.Push(taskToDelete);

                taskList.RemoveAt(Index);

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
                int Index = ((OperationModify)operation).oldTaskindex;
                Task taskToModify = taskList[Index];
                pastTask.Push(taskToModify);

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

    public class ExecuteUndo : OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {
                Operation undo; 
                undo = undoStack.Pop();

                Result result;
                bool flag = false;

                if (undo is OperationAdd)
                {
                    Task undoTask = undo.GetTask();
                    int index = taskList.IndexOf(undoTask);
                    OperationDelete undoOperation = new OperationDelete(index);

                    ExecuteDelete execute = new ExecuteDelete();
                    result = execute.ExecuteOperation(undoOperation);

                    if (result.Equals(Result.DELETE_SUCCESS))
                        flag = true;
                    
                }
                else if (undo is OperationDelete)
                {
                    Task undoTask = pastTask.Pop();
                    OperationAdd undoOperation = new OperationAdd(undoTask);

                    ExecuteAdd execute = new ExecuteAdd();
                    result = execute.ExecuteOperation(undoOperation);

                    if (result.Equals(Result.ADD_SUCCESS))
                        flag = true;
                }
                else if (undo is OperationModify)
                {
                    Task undoTask = pastTask.Pop();
                    Task redoTask = undo.GetTask();
                    int Index = taskList.IndexOf(redoTask);
                    OperationModify undoOperation = new OperationModify(Index, undoTask);

                    ExecuteModify execute = new ExecuteModify();
                    result = execute.ExecuteOperation(operation);

                    if (result.Equals(Result.MODIFY_SUCCESS))
                        flag = true;
                }

                if(flag == true)
                    return Result.UNDO_SUCCESS;
                else
                    return Result.ERROR;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Result.ERROR;
            }
        }
    }
    
    public class ExecuteSearch : OperationHandler
    {
        public override Result ExecuteOperation(Operation operation)
        {
            try
            {

                string condition = ((OperationSearch)operation).search;

                if (condition == "")
                {
                    foreach (Task task in taskList)
                    {
                        
                        //PrintToUI(task);
                    }
                    return Result.SEARCH_SUCCESS;
                }

                return Result.ERROR;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Result.ERROR;
            }

        }
    }
}
