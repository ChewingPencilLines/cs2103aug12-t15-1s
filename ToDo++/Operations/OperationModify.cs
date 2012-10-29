﻿using System.Collections.Generic;

namespace ToDo
{
    class OperationModify : Operation
    {
        private int? oldIndex;
        private Task newTask;
        
        public OperationModify(int Previous, Task Revised)
        {
            oldIndex = Previous - 1;
            newTask = Revised;
        }

        public OperationModify(Task Search)
        {
            newTask = Search;
        }

        public OperationModify()
        {
            oldIndex = null;
            newTask = null;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            /*
             *  when modify, if user key in nothing or only index or only task details
             *  after the commandtype, then all tasks will be shown.
             *  only when user input full information will modify operated.
             */
            this.storageXML = storageXML;
            string response;
            List<Task> searchResults;

            if (oldIndex.HasValue == false && newTask == null)
            {
                response = GenerateDisplayString(taskList);
            }
            else if (oldIndex.HasValue == false && newTask != null)
            {
                searchResults = SearchForTasks(taskList, newTask.TaskName);
                response = GenerateDisplayString(searchResults);
            }
            else if (oldIndex.HasValue == true && (oldIndex < 0 || oldIndex > taskList.Count - 1))
            {
                if (newTask != null)
                {
                    searchResults = SearchForTasks(taskList, newTask.TaskName);
                    response = GenerateDisplayString(searchResults);
                }
                else
                {
                    response = GenerateDisplayString(taskList);
                }
            }
            else
            {
                if (oldIndex.Value >= 0 && oldIndex.Value < lastListedTasks.Count)
                {
                    Task taskToModify = lastListedTasks[oldIndex.Value];
                    response = ModifyTask(ref taskToModify, newTask, ref taskList, out successFlag);
                }
                else response = RESPONSE_INVALID_TASK_INDEX;
            }
            if (successFlag) TrackOperation();
            return response;
        }

        public override string Undo(List<Task> taskList, Storage storageXML)
        {
            Task taskToUndo = undoTask.Pop();
            Task previousTask = undoTask.Pop();
            return ModifyTask(ref taskToUndo, previousTask, ref taskList, out successFlag);
        }
    }
}