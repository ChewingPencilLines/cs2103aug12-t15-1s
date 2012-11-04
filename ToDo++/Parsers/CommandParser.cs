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

            if (opAttributes.commandType == CommandType.SEARCH) opAttributes.SetSearchTime();
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
            TimeRangeKeywordsType? timeRange = opAttributes.timeRange;
            TimeRangeType? timeRangeType = opAttributes.timeRangeType;

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
                    newOperation = new OperationSort();
                    break; 
                case CommandType.REDO:
                    newOperation = new OperationRedo();
                    break;
                case CommandType.UNDO:
                    newOperation = new OperationUndo();
                    break;
                case CommandType.DONE:
                    newOperation=new OperationMarkAsDone(taskName,taskIndex,startCombined);
                    break;
                case CommandType.POSTPONE:
                    newOperation = new OperationPostpone(taskName, taskIndex, startCombined, endCombined, isSpecific);
                    break;
                case CommandType.SCHEDULE:
                    //todo: make use of overloaded GenerateNewTask method
                    task = GenerateNewTask(taskName, startCombined, endCombined, isSpecific);
                    newOperation = new OperationSchedule(task);
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
            //if (!specificity)
            //    endTime = new DateTime(((DateTime)startTime).Year, ((DateTime)startTime).Month, DateTime.DaysInMonth(((DateTime)startTime).Year, ((DateTime)startTime).Month));
            if (startTime == null && endTime == null)
                return new TaskFloating(taskName);
            else if (startTime == null && endTime != null)
                return new TaskDeadline(taskName, (DateTime)endTime, isSpecific);
            else if (startTime != null && endTime == null)
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)startTime, isSpecific); 
            else
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)endTime, isSpecific);
        }

        /*
        //overloading for schedule op
        private static Task GenerateNewTask(
            string taskName,
            TimeRangeType timeRangeType,
            TimeRangeKeywordsType timeRange,
            int timeRangeIndex,
            DateTime? userDefinedStartTime,
            DateTime? userDefinedEndTime,
            DateTimeSpecificity isUserDefinedDateTimeSpecific
            )
        {
            //if there is no time span indicated i.e. 3 days etc., get default 
            //getting task duration
            if (timeRangeIndex == 0 && timeRangeType == TimeRangeType.DEFAULT)
            {
                // todo: get default time range settings
                // for now, just take it to be 1 hour long
                timeRangeIndex = 1;
                timeRangeType = TimeRangeType.HOUR;
            }
            //getting search time range
            int timeRangeStartTime;
            int timeRangeEndTime;
            CustomDictionary.timeRangeKeywordsStartTime.TryGetValue(timeRange, out timeRangeStartTime);
            CustomDictionary.timeRangeKeywordsStartTime.TryGetValue(timeRange, out timeRangeEndTime);
            //todo: check that range > span, else return failure message
            //todo: loop through all tasks to find earliest possible fitting time
            //if cannot find, return failure message
            //otherwise, DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            //return GenerateNewTask(taskName, startTime, endTime, isSpecific) --- definitely specific
        }
        */

        private static void WarnUserOfMultipleCommands()
        {
            AlertBox.Show("Invalid input.\r\nMultiple commands were entered.");
            throw new NotImplementedException("Multiple commands were issued. Functionality NYI.");
        }  
    }
}
