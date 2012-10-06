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

        public Operation ParseOperation(string input)
        {
            // Get position of delimiters so we can treat those substrings as a single word.
            List<int[]> positionsOfDelimiters = GetPositionsOfDelimiters(input);
            List<Token> tokens = StringParser.ParseStringIntoTokens(input, positionsOfDelimiters);
            return GenerateOperation(tokens);            
        }

        private static Operation GenerateOperation(List<Token> tokens)
        {
            CommandType commandType = new CommandType();            
            ContextType currentMode = new ContextType();
            ContextType currentSpecifier = new ContextType();
            TimeSpan startTime, endTime;
            DateTime startDate, endDate;
            DayOfWeek startDay, endDay;
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;

            foreach (Token token in tokens)
            {
                if (token is TokenContext)
                {
                    TokenContext curToken = (TokenContext)token;
                    if (curToken.Value == ContextType.CURRENT ||
                        curToken.Value == ContextType.NEXT ||
                        curToken.Value == ContextType.FOLLOWING
                       )
                        currentSpecifier = curToken.Value;
                    else currentMode = curToken.Value;
                }
                else if (token is TokenCommand)
                {
                    if (commandType != CommandType.INVALID)
                        WarnUserOfMultipleCommands();
                    else
                        commandType = ((TokenCommand)token).Value;
                }
                else if (token is TokenTime)
                {
                    switch (currentMode)
                    {
                        case ContextType.STARTTIME:
                            startTime = ((TokenTime)token).Value;
                    	    break;
                        case ContextType.ENDTIME:
                            endTime = ((TokenTime)token).Value;
                    	    break;
                        case ContextType.DEADLINE:
                            endTime = ((TokenTime)token).Value;
                            break;
                        default:
                            Debug.Assert(false,"Fell through switch statement in GenerateOperation, TokenTime case!");
                            break;
                    }
                }
                else if (token is TokenDay)
                {                                        
                    switch (currentMode)
                    {
                        case ContextType.STARTTIME:
                            startDay = ((TokenDay)token).Value;
                            // @ivan-todo: WarnUser if already determined startDate and startDay conflicts
                            startDate = GetDateFromDay(currentSpecifier, startDay);
                            break;
                        case ContextType.ENDTIME:
                            endDay = ((TokenDay)token).Value;
                            endDate = GetDateFromDay(currentSpecifier, endDay);
                            break;
                        case ContextType.DEADLINE:
                            endDay = ((TokenDay)token).Value;
                            endDate = GetDateFromDay(currentSpecifier, endDay);
                            break;
                        default:
                            Debug.Assert(false, "Fell through switch statement in GenerateOperation, TokenDay case!");
                            break;
                    }                    
                }
                else if (token is TokenDate)
                {
                    switch (currentMode)
                    {
                        case ContextType.STARTTIME:
                            startDate = ((TokenDate)token).Value;
                            // @ivan-todo: WarnUser if already determined startDate
                            break;
                        case ContextType.ENDTIME:
                            endDate = ((TokenDate)token).Value;
                            break;
                        case ContextType.DEADLINE:
                            endDate = ((TokenDate)token).Value;
                            break;
                        default:
                            Debug.Assert(false, "Fell through switch statement in GenerateOperation, TokenDay case!");
                            break;
                    } 
                }
                else if (token is TokenLiteral)
                {

                }
                else { }

                // Generate operation based on values, and whether they have been used.
            }            
            return new OperationAdd(new TaskFloating());
        }

        /// <summary>
        /// This method accepts a day of the week and returns the corresponding date depending on what the preposition is.
        /// </summary>
        /// <param name="preposition">The prefix specifying how many weeks to add to the next found day.</param>
        /// <param name="desiredDay">The required day.</param>
        /// <returns>Date on which the <var>preposition</var> <var>desiredDay</var> is found.</returns>
        private static DateTime GetDateFromDay(ContextType preposition, DayOfWeek desiredDay)
        {
            DateTime startDate;
            DateTime tempDate = DateTime.Today;
            int daysToAdd = GetDaysToAdd(DateTime.Today.DayOfWeek, desiredDay);
            switch (preposition)
            {
                case ContextType.CURRENT:
                    break;
                case ContextType.NEXT:
                    daysToAdd += 7;
                    break;
                case ContextType.FOLLOWING:
                    daysToAdd += 14;
                    break;
                default:
                    Debug.Assert(false, "Fell through switch statement in GetDateFromDay!");
                    break;
            }
            tempDate.AddDays(daysToAdd);
            startDate = tempDate;
            return startDate;
        }

        /// <summary>
        /// Calculates the number of days to add to the given day of
        /// the week in order to return the next occurrence of the
        /// desired day of the week.
        /// </summary>
        /// <param name="current">The starting day of the week.</param>
        /// <param name="desired">The desired day of the week.</param>
        /// <returns>
        ///		The number of days to add to <var>current</var> day of week
        ///		in order to achieve the next <var>desired</var> day of week.
        /// </returns>
        private static int GetDaysToAdd(DayOfWeek current, DayOfWeek desired )
        {
            // f( c, d ) = [7 - (c - d)] mod 7
            //   where 0 <= c < 7 and 0 <= d < 7

            int c = (int)current;
            int d = (int)desired;
            return (7 - c + d) % 7;
        }

        private static void WarnUserOfMultipleCommands()
        {
            throw new NotImplementedException();
        }  

        private List<int[]> GetPositionsOfDelimiters(string input)
        {
            List<int[]> positionsOfDelimiters;
            positionsOfDelimiters = StringParser.FindPositionOfDelimiters(input);
            SortIndexes(ref positionsOfDelimiters);
            RemoveBadIndexes(ref positionsOfDelimiters);
            return positionsOfDelimiters;
        }

        /// <summary>
        /// This method checks each pair of indexes in a List and removes those
        /// that overlaps with the previous pair.
        /// </summary>
        /// <param name="indexOfDelimiters"></param>
        /// <returns></returns>
        private void RemoveBadIndexes(ref List<int[]> indexOfDelimiters)
        {
            int previousEndIndex = -1;
            List<int[]> indexesToRemove = new List<int[]>();
            foreach (int[] set in indexOfDelimiters)
            {
                if (set[START_INDEX] < previousEndIndex)
                    indexesToRemove.Add(set);
                previousEndIndex = set[END_INDEX];
            }
            indexOfDelimiters.RemoveAll(x => indexesToRemove.Contains(x));
        }

        private void SortIndexes(ref List<int[]> indexOfDelimiters)
        {
            Comparison<int[]> comparison = new Comparison<int[]>(CompareBasedOnZerothIndex);
            indexOfDelimiters.Sort(comparison);
        }
            

        /// <summary>
        /// This method is a comparator to safely sort a container of int[] based on their zeroth index only.
        /// </summary>
        /// <param name="x">First object to compare</param>
        /// <param name="y">Second object to compare</param>
        /// <returns>int: -1 if x less y, 0 if x equals y, 1 if x more y</returns>
        private static int CompareBasedOnZerothIndex(int[] x, int[] y)
        {
            if (x == null || x.Count() < 1)
            {
                if (y == null || y.Count() < 1)
                {
                    // If x is null and y is null or have less than 1 element, they're equal
                    return 0;
                }
                else
                {
                    // If x is null or have less than 1 element and y is valid, y is greater
                    return -1;
                }
            }
            else
            {
                // If x is valid
                if (y == null || y.Count() < 1)
                // ...and y is null or less than 1 element, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is valid, compare the zeroth index of the array
                    if (x[0] == y[0])
                    {
                        return 0;
                    }
                    else if (x[0] < y[0])
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }

    }
}
