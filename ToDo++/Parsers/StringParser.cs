using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    public class StringParser
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] delimitingCharacters = { { '\'', '\'' }, { '\"', '\"' }, { '[', ']' }, { '(', ')' }, { '{', '}' } };

        static StringParser()
        {
        }

        // ******************************************************************
        // Public Methods
        // ******************************************************************

        #region Internal Methods

        /// <summary>
        /// This method parses a string of words into a list of tokens, each containing a token representing the meaning of each word or substring.
        /// By inputting a list of integer pairs to mark delimiting characters, multiple words can be taken as a single absolute substring (word).  
        /// </summary>
        /// <param name="input">The string of words to be parsed</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings</param>
        /// <returns>The list of tokens</returns>
        internal List<Token> ParseStringIntoTokens(string input)
        {
            List<int[]> indexOfDelimiters = FindPositionOfDelimiters(input);
            List<string> words = SplitStringIntoSubstrings(input, indexOfDelimiters);
            TokenGenerator tokenGenerator = new TokenGenerator();
            return tokenGenerator.GenerateTokens(words);
        }

        internal static string MarkWordsAsAbsolute(string absoluteSubstr)
        {
            string[] words = absoluteSubstr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
            string output = "";
            foreach (string word in words)
            {
                output += "\"" + word + "\" ";
            }
            output.TrimEnd();
            return output;
        }

        internal static string UnmarkWordsAsAbsolute(string absoluteSubstr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in absoluteSubstr)
            {
                if (c != '\"')
                    sb.Append(c);
            }
            return sb.ToString();
        }
        #endregion

        // ******************************************************************
        // String Parsing Algorithms
        // ******************************************************************

        #region String Splitting and Merging Methods
        /// <summary>
        /// This method splits a string and returns a list of substrings, each containing either a word delimited by a space,
        /// or a substring delimited by positions in the parameter indexOfDelimiters.
        /// </summary>
        /// <param name="input">The string of words to be split</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings</param>
        /// <returns>List of substrings</returns>
        private List<string> SplitStringIntoSubstrings(string input, List<int[]> indexOfDelimiters)
        {
            List<string> words = new List<string>();
            int processedIndex = 0, removedCount = 0;

            if (indexOfDelimiters == null)
                return input.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (int[] substringIndex in indexOfDelimiters)
            {
                int count = substringIndex[END_INDEX] - substringIndex[START_INDEX] + 1;
                int startIndex = substringIndex[START_INDEX];
                string subStr = input.Substring(processedIndex, startIndex - processedIndex);

                // Add words leading up to the delimiting character
                words.AddRange(subStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());

                // Get absolute substring without the delimiter characters and add to return list
                string absoluteSubstr = input.Substring(startIndex + 1, count - 2);
                absoluteSubstr = MarkWordsAsAbsolute(absoluteSubstr);
                words.Add(absoluteSubstr);

                // Update processed index state and count of removed characters
                processedIndex = substringIndex[END_INDEX] + 1;
                removedCount += count;
            }

            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());            
            words = MergeDateAndTimeWords(words);
            words = MergeRangeKeywords(words);
            return words;
        }

        private List<string> MergeRangeKeywords(List<string> words)
        {               
            int skipWords = 1;
            List<string> output = new List<string>();
            for (int i = 0; i < words.Count; i++)
            {
                if (skipWords > 1)
                {
                    skipWords--;
                    continue;
                }
                string mergedString = words[i];
                if (CustomDictionary.IsValidTime(words[i]))
                {
                    if (i < words.Count-2
                        && CustomDictionary.IsPartOfValidNumericalTimeRange(words, i))
                    {
                        if (words[i - 1] == "from")
                        {
                            output.RemoveAt(output.Count-1);
                            mergedString = words[i] + " to " + words[i + 2];
                        }
                        else
                        {
                            mergedString += "-" + words[i + 2];
                        }
                        skipWords = 3;
                    }
                }
                else
                {
                    bool success = false;
                    while (i + skipWords < words.Count)  // Don't check last word.
                    {
                        success = CustomDictionary.isNumericalRange.IsMatch(mergedString + words[i + skipWords]);
                        if (success) mergedString += words[i + skipWords];
                        else break;
                        skipWords++;
                    }
                }
                if (mergedString == String.Empty)
                {
                    output.Add(words[i]);
                }
                else
                {
                    output.Add(mergedString);
                }
            }
            return output;
        }

        /// <summary>
        /// This method detects and merges all the date and time words into a single string
        /// while keeping the other words separate and unmerged.
        /// For example, the list input "add", "task", "friday", "5", "pm", "28", "sept", "2012"
        /// will return "add", "task", "friday", "5pm", "28 sept 2012"
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time/date phrases</returns>
        private List<string> MergeDateAndTimeWords(List<string> input)
        {
            input = MergeTimeWords(input);
            input = MergeDateWords(input);
            return input;
        }
                
        #region Time merging methods
        /// <summary>
        /// This method checks all words within an input list of words for valid times and returns a list of words
        /// where all times are merged as a single word.
        /// For example, if there is a valid time such as i.e. 5 pm, it combines "5" and "pm" in the returned list of words as "5pm".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time phrases</returns>
        private List<string> MergeTimeWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0;
            bool isWordAdded = false;
            foreach (string word in input)
            {
                if (CustomDictionary.CheckIfWordIsTimeSuffix(word))
                {
                    isWordAdded = MergeWord_IfValidTime(ref output, input, position);
                    if (isWordAdded) break;
                }
                if (!isWordAdded)
                {
                    output.Add(word);
                }
                isWordAdded = false;
                position++;
            }
            return output;
        }

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of a time phrase
        /// and merges it with the other words constituting the time phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged time phrases up to the indicated word/time phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word in the input list to be checked</param>
        /// <returns>True if the indicated word is part of a time phrase and false if otherwise</returns>
        private bool MergeWord_IfValidTime(ref List<string> output, List<string> input, int position)
        {
            string backHalf = input.ElementAt(position);
            string frontHalf;
            if (position == 0)
            {
                return false;
            }
            frontHalf = input.ElementAt(position - 1);
            string mergedWord = String.Concat(frontHalf, backHalf);
            if (CustomDictionary.IsValidTime(mergedWord))
            {
                output.RemoveAt(output.Count - 1);
                output.Add(mergedWord);
                return true;
            }
            else return false;
        }
        #endregion

        #region Date merging methods
        /// <summary>
        /// This method checks all words within an input list of words for valid date and returns a list of words
        /// where all dates are merged as a single word.
        /// For example, if there is a valid time such as i.e. 23 sept 2012, it combines "23", "sept" and "2012"
        /// in the returned list of words as "23 sept 2012".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged date phrases</returns>
        private List<string> MergeDateWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0, skipWords = 0;
            bool isWordAdded = false;
            foreach (string word in input)
            {
                // skip word if it has been combined with the last determined date keyword into a date phrase
                if (skipWords > 0)
                {
                    skipWords--;
                    position++;
                    continue;
                }
                if (CustomDictionary.monthKeywords.ContainsKey(word.ToLower()))
                {
                    isWordAdded = MergeWord_IfValidAlphabeticDate(ref output, input, position, ref skipWords);
                }
                if (!isWordAdded)
                {
                    output.Add(word);
                }
                isWordAdded = false;
                position++;
            }
            // dates in numeric date formats and dates that are only specified by day with suffixes i.e. "15th"
            // need not be checked for and merged since they are already whole words on their own.
            return output;
        }

        /* Note that "12 may 2012 2012" will produce merged word "12 may 2012"
        * and "12 may 23 2012" will produce the merged word "12 may 23".
        */

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of an alphabetic date phrase
        /// and merges it with the other words constituting the date phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged alphabetic date phrases up to the indicated date phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word  in the input list to be checked</param>
        /// <param name="numberOfWords">The number of words behind the indicated word that were merged to form the date</param>
        /// <returns>True if the indicated word is part of a date phrase and false if otherwise</returns>
        private bool MergeWord_IfValidAlphabeticDate(ref List<string> output, List<string> input, int position, ref int numberOfWords)
        {
            string month = input.ElementAt(position);
            string mergedWord = month;
            bool isWordUsed = false;
            int i = 1;
            // Backward check
            if ((position > 0) &&
                (CustomDictionary.IsValidAlphabeticDate(input[position - 1] + " " + mergedWord.ToLower())))
            {
                mergedWord = input[position - 1] + " " + mergedWord;
                isWordUsed = true;
            }
            // Forward check
            while (position + i < input.Count)
            {
                if (CustomDictionary.IsValidAlphabeticDate(mergedWord.ToLower() + " " + input[position + i]))
                {
                    mergedWord = mergedWord + " " + input[position + i];
                }
                else break;
                i++;
            }
            if (mergedWord == month)
            {
                return false;
            }
            if (isWordUsed == true)
            {
                output.RemoveAt(output.Count - 1);
            }
            output.Add(mergedWord);
            numberOfWords = i - 1;
            return true;
        }
        #endregion

        #region Delimiter searching methods
        /// <summary>
        /// This method searches the input string against the set delimiters'
        /// and return the positions of the delimiters as a list of integer pairs.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List containing all matching sets of delimiters as integer pairs</returns>
        private List<int[]> FindPositionOfDelimiters(string input)
        {
            List<int[]> indexOfDelimiters = new List<int[]>();
            int startIndex = 0, endIndex = -1;
            for (int i = 0; i < delimitingCharacters.GetLength(0); i++)
            {
                startIndex = 0;
                endIndex = -1;
                do
                {
                    startIndex = input.IndexOf(delimitingCharacters[i, START_INDEX], endIndex + 1);
                    endIndex = input.IndexOf(delimitingCharacters[i, END_INDEX], startIndex + 1);
                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        int[] index = new int[2] { startIndex, endIndex };
                        indexOfDelimiters.Add(index);
                    }
                    else break;
                } while (true);

            }
            return indexOfDelimiters;
        }

        private List<int[]> GetPositionsOfDelimiters(string input)
        {
            List<int[]> positionsOfDelimiters;
            positionsOfDelimiters = FindPositionOfDelimiters(input);
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
            indexOfDelimiters = (from index in indexOfDelimiters
                                 orderby index[0]
                                 select index).ToList();
        }
        #endregion

        #endregion
    }
}
