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

                xml.WriteXML(Program.taskList);

                return Add_Suceess_Message;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Wrong_Message;
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

                xml.WriteXML(Program.taskList);

                return Delete_Success_Message;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Wrong_Message;
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

                xml.WriteXML(Program.taskList);

                return Modify_Success_Message;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Wrong_Message;
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

                    if (result.Equals(Delete_Success_Message))
                        flag = true;
                    
                }
                else if (undo is OperationDelete)
                {
                    Task undoTask = pastTask.Pop();
                    OperationAdd undoOperation = new OperationAdd(undoTask);

                    ExecuteAdd execute = new ExecuteAdd();
                    result = execute.ExecuteOperation(undoOperation);

                    if (result.Equals(Add_Suceess_Message))
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

                    if (result.Equals(Modify_Success_Message))
                        flag = true;
                }

                if(flag == true)
                    return  Undo_Success_Message;
                else
                    return  Wrong_Message;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return  Wrong_Message;
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
                        else if (task is TaskTimed)
                        {
                            result = string.Concat(result, ((TaskDeadline)task).taskname,
                                ((TaskTimed)task).starttime.ToString(),
                                ((TaskDeadline)task).endtime.ToString(), "\n");
                        }
                    }
                    return result;
                }

                return  Wrong_Message;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return  Wrong_Message;
            }

        }
    }
}
