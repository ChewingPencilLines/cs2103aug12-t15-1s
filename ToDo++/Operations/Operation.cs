using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for Operation
    // ******************************************************************
    public abstract class Operation
    {
        // Containers for keeping track of executed operations
        protected static List<Task>  currentListedTasks;
        protected static Stack<Operation> undoStack;
        protected static Stack<Operation> redoStack;
        protected static Stack<Task> undoTask;
        protected static Stack<Task> redoTask;
        protected Storage storageIO;
   //     protected bool successFlag;

        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
            undoTask = new Stack<Task>();
            redoTask = new Stack<Task>();
        }

        protected void TrackOperation()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }

        public abstract Response Execute(List<Task> taskList, Storage storageIO);


        /// <summary>
        /// Base Undo Operation Method. All undoable operations should be override this method.
        /// This base method will throw an assertion if called.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to write changes to file. </param>
        /// <returns></returns>
        public virtual Response Undo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be undoable!");
            return null;
        }

        public virtual Response Redo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be redoable!");
            return null;
        }

        // ******************************************************************
        // Task Manipulation Methods
        // ******************************************************************

        #region Task Manipulation Methods
        protected Response AddTask(Task taskToAdd, List<Task> taskList)
        {
          //  successFlag = false;
            try
            {
                taskList.Add(taskToAdd);
                undoTask.Push(taskToAdd);
                if (storageIO.AddTaskToFile(taskToAdd))
                {
                  //  successFlag = true;
                    currentListedTasks.Add(taskToAdd);
                    return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationAdd),  currentListedTasks);
                    //  return String.Format(RESPONSE_ADD_SUCCESS, taskToAdd.TaskName);
                }
                else
                    return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationAdd),  currentListedTasks);
                  //  return RESPONSE_XML_READWRITE_FAIL;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationAdd),  currentListedTasks); 
                //return RESPONSE_ADD_FAILURE + "\r\nThe following exception occured: " + e.ToString();
            }
        }

        protected Response DeleteTask(Task taskToDelete, List<Task> taskList)
        {
            //successFlag = false;

            // Remove tasks and push to undo stack.
            undoTask.Push(taskToDelete);
            taskList.Remove(taskToDelete);
            
            // Can just remove task from currentListedTask with new UI
            currentListedTasks.Remove(taskToDelete);

            if (storageIO.RemoveTaskFromFile(taskToDelete))
            {
              //  successFlag = true;
                currentListedTasks.Remove(taskToDelete);
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationDelete),  currentListedTasks);                
            }
            else
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationDelete),  currentListedTasks);
        }

        protected Response MarkAsDone(Task taskToMarkAsDone)
        {
           // successFlag = false;
            undoTask.Push(taskToMarkAsDone);
            taskToMarkAsDone.DoneState = true;

            if (storageIO.MarkTaskAsDone(taskToMarkAsDone))
            {
               // successFlag = true;
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationMarkAsDone),  currentListedTasks);
                //return String.Format(RESPONSE_MARKASDONE_SUCCESS, taskToMarkAsDone.TaskName);
            }
            else
                //return RESPONSE_XML_READWRITE_FAIL;
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationMarkAsDone),  currentListedTasks);
        }

        protected Response ModifyTask(Task taskToModify, Task newTask, List<Task> taskList)
        {
           // successFlag = false;
            undoTask.Push(taskToModify);
            taskList.Remove(taskToModify);
            taskList.Add(newTask);
            undoTask.Push(newTask);
            if (storageIO.RemoveTaskFromFile(taskToModify) && storageIO.AddTaskToFile(newTask))
            {
               // successFlag = true;
                 currentListedTasks.Remove(taskToModify);
                 currentListedTasks.Add(newTask);
               // return String.Format(RESPONSE_MODIFY_SUCCESS, taskToModify.TaskName, newTask.TaskName);
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationModify),  currentListedTasks);

            }
            else
              //  return RESPONSE_XML_READWRITE_FAIL;
                return new Response(Result.XML_READWRITE_FAIL, Format.DEFAULT, typeof(OperationModify),  currentListedTasks);
        }

        // todo: move search queries into the tasks themselves as methods.
        protected Response PostponeTask(Task taskToPostpone, List<Task> taskList, DateTime? NewDate)
        {
           // successFlag = false;
            Task taskPostponed = taskToPostpone.Postpone(NewDate);
            if (taskPostponed == null)
            {
                return new Response(Result.FAILURE, Format.DEFAULT, typeof(OperationPostpone));   
            }
            else
            {
                undoTask.Push(taskToPostpone);
                taskList.Remove(taskToPostpone);
                taskList.Add(taskPostponed);
                undoTask.Push(taskPostponed);
            }
            if (storageIO.RemoveTaskFromFile(taskToPostpone) && storageIO.AddTaskToFile(taskPostponed))
            {
               // successFlag = true;
                return new Response(Result.SUCCESS, Format.DEFAULT, typeof(OperationPostpone));
            }
            else
                return new Response(Result.XML_READWRITE_FAIL);
        }
        
        protected List<Task> SearchForTasks(List<Task> taskList, string searchString, bool exact = false, DateTime? startTime = null, DateTime? endTime = null)
        {
            List<Task> filteredTasks = taskList;
            if (searchString != null)
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString, task.TaskName, true) == 0 && exact == true))
                                 select task).ToList();
            if (!(startTime == null && endTime == null)) 
                filteredTasks = (from task in filteredTasks
                                 where task.IsWithinTime(startTime,endTime)
                                 select task).ToList();
            return filteredTasks;
        }

        #endregion
    }
}
