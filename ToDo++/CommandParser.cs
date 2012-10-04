using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    class CommandParser
    {
        const int START_INDEX = 0;
        const int END_INDEX = 1;

        public Operation ParseOperation(string input)
        {
            CommandType command = CommandType.INVALID;
            List<int[]> positionsOfDelimiters;
            List<string> inputWords = null;
            string nonabsoluteString = null;
            int positionOfCommandKeyword = -1;
            
            // Get position of delimiters so we can treat those substrings as a single word.
            positionsOfDelimiters = GetPositionsOfDelimiters(input);

            inputWords = StringParser.SplitStringIntoWords(input, ref nonabsoluteString, positionsOfDelimiters);

            // Search for command keyword            
            int matchCount = StringParser.SearchForCommandKeyword(inputWords, ref command, ref positionOfCommandKeyword);
            if (positionOfCommandKeyword < 0) return null; // failed, either no match or multiple matches
            inputWords = StringParser.RemoveWordFromSentence_ByIndex(inputWords, positionOfCommandKeyword);

            // Search for dates/times            
            List<DateTime> taskTime = StringParser.SearchForDateTime(inputWords);
            return new Operation();
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
        internal void RemoveBadIndexes(ref List<int[]> indexOfDelimiters)
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

        internal void SortIndexes(ref List<int[]> indexOfDelimiters)
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
