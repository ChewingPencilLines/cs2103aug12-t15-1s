using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    class CommandParser
    {
        const int START_INDEX = 0;
        const int END_INDEX = 1;
        StringParser stringParser;

        public CommandParser(ref StringParser stringParser)
        {
            this.stringParser = stringParser;
        }

        public Operation ParseOperation(string input)
        {
            // Get position of delimiters so we can treat those substrings as a single word.
            List<Token> tokens = stringParser.ParseStringIntoTokens(input);
            return GenerateOperation(tokens);            
        }

        private static Operation GenerateOperation(List<Token> tokens)
        {            
            OperationAttributes opAttributes = new OperationAttributes();
            foreach (Token token in tokens)
            {
                try
                {
                    token.UpdateAttributes(opAttributes);
                }
                catch (MultipleCommandsException)
                {
                    WarnUserOfMultipleCommands();
                }
            }
            if (opAttributes.commandType == CommandType.SCHEDULE)
            {
                opAttributes.SetScheduleTime();
            }
            else
            {
                opAttributes.SetSearchTime();
            }
            opAttributes.CombineDateTimes();
            Operation newOperation = CreateOperation(opAttributes);
            return newOperation;
        }

        // Create operation based on derived values, and whether they have been used.
        private static Operation CreateOperation(OperationAttributes opAttributes)
        {            
            CommandType commandType = opAttributes.commandType;
            DateTime? startCombined = opAttributes.startDateTime;
            DateTime? endCombined = opAttributes.endDateTime;
            DateTimeSpecificity isSpecific = opAttributes.isSpecific;
            bool isAll = opAttributes.rangeIsAll;
            string taskName = opAttributes.taskName;
            int[] taskIndex = opAttributes.rangeIndexes;
            int timeRangeIndex = opAttributes.timeRangeIndex;
            TimeRangeType timeRangeType = opAttributes.timeRangeType;
            SortType sortType = opAttributes.sortType;

            Task task;
            Operation newOperation = null;
            switch (commandType)
            {
                case CommandType.ADD:
                    task = GenerateNewTask(taskName, startCombined, endCombined, isSpecific);
                    newOperation = new OperationAdd(task);
                    break;
                case CommandType.DELETE:
                    newOperation = new OperationDelete(taskName, taskIndex, startCombined, endCombined, isSpecific, isAll);
                    break;
                case CommandType.DISPLAY:
                    newOperation = new OperationDisplayDefault();
                    break;
                case CommandType.MODIFY:
                    task = GenerateNewTask(taskName, startCombined, endCombined, isSpecific);
                    newOperation = new OperationModify(taskIndex,task);
                    break;
                case CommandType.SEARCH:
                    newOperation = new OperationSearch(taskName, startCombined, endCombined, isSpecific);
                    break;
                case CommandType.SORT:
                    newOperation = new OperationSort(sortType);
                    break; 
                case CommandType.REDO:
                    newOperation = new OperationRedo();
                    break;
                case CommandType.UNDO:
                    newOperation = new OperationUndo();
                    break;
                case CommandType.DONE:
                    newOperation = new OperationMarkAsDone(taskName,taskIndex,startCombined);
                    break;
                case CommandType.POSTPONE:
                    newOperation = new OperationPostpone(taskName, taskIndex, startCombined, endCombined, isSpecific, isAll);
                    break;
                case CommandType.SCHEDULE:
                    newOperation = new OperationSchedule(taskName, (DateTime)startCombined, endCombined, isSpecific, timeRangeIndex, timeRangeType);
                    break;
                case CommandType.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            return newOperation;
        }

        private static Task GenerateNewTask(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            DateTimeSpecificity isSpecific
            )
        {
            if (startTime == null && endTime == null)
                return new TaskFloating(taskName);
            else if (startTime == null && endTime != null)
                return new TaskDeadline(taskName, (DateTime)endTime, isSpecific);
            else if (startTime != null && endTime == null)
            {
                // If endTime is not specified set endTime based on startTime.
                endTime = startTime;
                if (!isSpecific.StartTime)
                {
                    endTime = ((DateTime)endTime).AddDays(1);
                    endTime = ((DateTime)endTime).AddMinutes(-1);
                }
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)startTime, isSpecific);
            }
            else
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)endTime, isSpecific);
        }

        private static void WarnUserOfMultipleCommands()
        {
            AlertBox.Show("Invalid input.\r\nMultiple commands were entered.");
            throw new NotImplementedException("Multiple commands were issued. Functionality NYI.");
        }  
    }
}
