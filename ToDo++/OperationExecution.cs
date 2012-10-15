using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ToDo
{
    // ******************************************************************
    // Implementation of add execution
    // ******************************************************************
    public class ExecuteAdd : OperationHandler
    {
        public override string ExecuteOperation(Operation operation)
        {
            try
            { 
                Task taskToAdd = ((OperationAdd)operation).GetTask();
                Program.taskList.Add(taskToAdd);

                storageXML.WriteXML(Program.taskList);

                return RESPONSE_SUCCESS_ADD;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return REPONSE_INVALID_COMMAND;
            }
        }
    }

    // ******************************************************************
    // Implementation of delete execution
    // ******************************************************************
    public class ExecuteDelete : OperationHandler
    {
        public override string ExecuteOperation(Operation operation)
        {
            try
            {
                int Index= ((OperationDelete)operation).index;
                Debug.Assert(Index >= 0 && Index < Program.taskList.Count);
                Task taskToDelete = Program.taskList[Index];
                pastTask.Push(taskToDelete);
                Program.taskList.RemoveAt(Index);

                storageXML.WriteXML(Program.taskList);

                return RESPONSE_SUCCESS_DELETE;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return REPONSE_INVALID_COMMAND;
            }
        }
    }

    // ******************************************************************
    // Implementation of modify execution
    // ******************************************************************
    public class ExecuteModify : OperationHandler
    {
        public override string ExecuteOperation(Operation operation)
        {
            try
            {
                int Index = ((OperationModify)operation).oldTaskindex;
                Debug.Assert(Index >= 0 && Index < Program.taskList.Count);
                Task taskToModify = Program.taskList[Index];
                pastTask.Push(taskToModify);

                Task taskRevised = operation.GetTask();
                Program.taskList[((OperationModify)operation).oldTaskindex] = taskRevised;

                storageXML.WriteXML(Program.taskList);

                return RESPONSE_SUCCESS_MODIFY;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return REPONSE_INVALID_COMMAND;
            }
        }
    }

    // ******************************************************************
    // Implementation of undo execution
    // ******************************************************************
    public class ExecuteUndo : OperationHandler
    {
        public override string ExecuteOperation(Operation operation)
        {
            try
            {
                Operation undo; 
                undo = undoStack.Pop();

                string result;
                bool flag = false;

                if (undo is OperationAdd)
                {
                    Task undoTask = undo.GetTask();
                    int index = Program.taskList.IndexOf(undoTask);
                    Debug.Assert(index >= 0 && index < Program.taskList.Count);
                    OperationDelete undoOperation = new OperationDelete(index);

                    ExecuteDelete execute = new ExecuteDelete();
                    result = execute.ExecuteOperation(undoOperation);

                    if (result.Equals(RESPONSE_SUCCESS_DELETE))
                        flag = true;
                    
                }
                else if (undo is OperationDelete)
                {
                    Task undoTask = pastTask.Pop();
                    OperationAdd undoOperation = new OperationAdd(undoTask);

                    ExecuteAdd execute = new ExecuteAdd();
                    result = execute.ExecuteOperation(undoOperation);

                    if (result.Equals(RESPONSE_SUCCESS_ADD))
                        flag = true;
                }
                else if (undo is OperationModify)
                {
                    Task undoTask = pastTask.Pop();
                    Task redoTask = undo.GetTask();
                    int Index = Program.taskList.IndexOf(redoTask);
                    Debug.Assert(Index >= 0 && Index < Program.taskList.Count);
                    OperationModify undoOperation = new OperationModify(Index, undoTask);

                    ExecuteModify execute = new ExecuteModify();
                    result = execute.ExecuteOperation(operation);

                    if (result.Equals(RESPONSE_SUCCESS_MODIFY))
                        flag = true;
                }

                if(flag == true)
                    return  RESPONSE_SUCCESS_UNDO;
                else
                    return  REPONSE_INVALID_COMMAND;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return  REPONSE_INVALID_COMMAND;
            }
        }
    }

    // ******************************************************************
    // Implementation of search execution
    // ******************************************************************
    public class ExecuteSearch : OperationHandler
    {
        public override string ExecuteOperation(Operation operation)
        {
            try
            { 
                string condition = ((OperationSearch)operation).search;
                string result = "";

                if (condition == "")
                {
                    foreach (Task task in Program.taskList)
                    {  
                        if (task is TaskFloating)
                        {
                            result= string.Concat(result,task.taskname,"\n"); 
                        }
                        else if (task is TaskDeadline)
                        {
                            result = string.Concat(result, ((TaskDeadline)task).taskname, 
                                ((TaskDeadline)task).endtime.ToString(),"\n"); 
                        }
                        else if (task is TaskEvent)
                        {
                            result = string.Concat(result, ((TaskDeadline)task).taskname,
                                ((TaskEvent)task).starttime.ToString(),
                                ((TaskDeadline)task).endtime.ToString(), "\n");
                        }
                    }
                    return result;
                }

                return  REPONSE_INVALID_COMMAND;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return  REPONSE_INVALID_COMMAND;
            }

        }
    }
}
