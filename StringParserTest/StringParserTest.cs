using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    [TestClass]
    public class StringParserTest
    {
        CommandType command = CommandType.INVALID;
        int indexOfCommandKeyword = -1;
        List<string> testWords = new List<string>();

        [TestMethod]
        public void IsValidTimeTest()
        {
            Assert.IsTrue(StringParser.IsValidTime("5:00"));
            Assert.IsTrue(StringParser.IsValidTime("5:01"));
            Assert.IsTrue(StringParser.IsValidTime("05:23"));
            Assert.IsTrue(StringParser.IsValidTime("15:59"));
            Assert.IsTrue(StringParser.IsValidTime("2359hr"));
            Assert.IsTrue(StringParser.IsValidTime("1200hrs"));
            Assert.IsTrue(StringParser.IsValidTime("2200 hours"));
            Assert.IsTrue(StringParser.IsValidTime("1234 HOURS"));
            Assert.IsFalse(StringParser.IsValidTime("2400 hr")); // only up to 2359 is valid.
        }

        [TestMethod]
        public void Simple_SearchForCommandTest_Add()
        {
            testWords.Clear();
            testWords.Add("adda");
            testWords.Add("bbbb");
            testWords.Add("add");
            testWords.Add("date");
            Assert.AreEqual(1, StringParser.SearchForCommandKeyword(testWords,ref command, ref indexOfCommandKeyword));
            Assert.AreEqual(2, indexOfCommandKeyword);
            Assert.AreEqual(CommandType.ADD, command);
        }

        [TestMethod]
        public void Simple_SearchForCommandTest_Modify()
        {
            testWords.Clear();
            testWords.Add("stuff");
            testWords.Add("modify");
            testWords.Add("more stuff?!");
            testWords.Add("date");
            Assert.AreEqual(1, StringParser.SearchForCommandKeyword(testWords, ref command, ref indexOfCommandKeyword));
            Assert.AreEqual(1, indexOfCommandKeyword);
            Assert.AreEqual(CommandType.MODIFY, command);
        }        

        [TestMethod]
        public void Null_SearchForCommandTest()
        {
            testWords.Clear();
            testWords.Add("stuff");
            testWords.Add("rubbish");
            testWords.Add("addify");
            testWords.Add("date");
            Assert.AreEqual(0, StringParser.SearchForCommandKeyword(testWords, ref command, ref indexOfCommandKeyword));
            Assert.AreEqual(-1, indexOfCommandKeyword);
            Assert.AreEqual(CommandType.INVALID, command);
        }

        [TestMethod]
        public void MultipleMatch_SearchForCommandTest()
        {
            testWords.Clear();
            testWords.Add("Add");
            testWords.Add("rubbish");
            testWords.Add("addify");
            testWords.Add("modify");
            Assert.AreEqual(2, StringParser.SearchForCommandKeyword(testWords, ref command, ref indexOfCommandKeyword));
            Assert.AreEqual(0, indexOfCommandKeyword);
            Assert.AreEqual(CommandType.ADD, command);
        }

        [TestMethod]
        public void Simple_FindIndexOfDelimitersTest()
        {
            string input = "\'add\'";
            List<int[]> expected = new List<int[]> { new int[2] { 0, 4 } };
            // Flatten before comparing. Product limitation of visual studio compiler / unit tester.
            CollectionAssert.AreEqual(
                expected.SelectMany(x => x).ToList(),
                StringParser.FindPositionOfDelimiters(input).SelectMany(x => x).ToList()
                );
        }
        
        [TestMethod]
        public void Multiple_FindIndexOfDelimitersTest()
        {
            // index         0123 4567890 12345 6
            string input = "\'add\' hii! \"date\"";
            List<int[]> expected = new List<int[]> { 
                new int[2] { 0, 4 },
                new int[2] { 11, 16 }
            };
            Assert.IsTrue(ListOfIntegerArraysAreEquivalent(expected, StringParser.FindPositionOfDelimiters(input)));
        }

        [TestMethod]
        public void Complex_FindIndexOfDelimitersTest()
        {
            // index         012345 67890123 45678 9012
            string input = "\'a{d}d\' h[ii! \"date\"";
            List<int[]> expected = new List<int[]> {
                new int[2] { 0, 6 },
                new int[2] { 2, 4 },
                new int[2] { 14, 19 },
            };
            Assert.IsTrue(ListOfIntegerArraysAreEquivalent(expected, StringParser.FindPositionOfDelimiters(input)));
        }

        [TestMethod]
        public void Null_FindIndexOfDelimitersTest()
        {
            string input = "\"add\'";
            List<int[]> expected = new List<int[]>();
            Assert.IsTrue(ListOfIntegerArraysAreEquivalent(expected, StringParser.FindPositionOfDelimiters(input)));
            Assert.IsTrue(StringParser.FindPositionOfDelimiters(input).Count == 0);
        }

        [TestMethod]
        public void SplitStringIntoWordsTest()
        {
            // index        01234567890123456789012
            string input = "add {modify car} tonight";
            List<int[]> delimiters = new List<int[]> {
                new int[2] { 4, 15 }
            };
            List<string> expected = new List<string> {
                "add",
                "modify car",
                "tonight"
            };
            CollectionAssert.AreEquivalent(expected,StringParser.SplitStringIntoWords(input,delimiters));
            CollectionAssert.AreEqual(expected, StringParser.SplitStringIntoWords(input, delimiters));
        }

        [TestMethod]
        public void Multiple_SplitStringIntoWordsTest()
        {
            // index        0123456789012345 678901234567 89012345678
            string input = "add {modify car}\"tonight 8pm\" deadline";
            List<int[]> delimiters = new List<int[]> {
                new int[2] { 4, 15 },
                new int[2] { 16, 28 }
            };
            List<string> expected = new List<string> {
                "add",
                "modify car",
                "tonight 8pm",
                "deadline"
            };
            CollectionAssert.AreEquivalent(expected, StringParser.SplitStringIntoWords(input, delimiters));
            CollectionAssert.AreEqual(expected, StringParser.SplitStringIntoWords(input, delimiters));
        }

        // Returns true if the two lists contains (any order) the same exact int arrays.
        private bool ListOfIntegerArraysAreEquivalent(List<int[]> first, List<int[]> second)
        {
            bool foundArray = false;
            foreach (int[] arrayFirst in first)
            {
                foreach (int[] arraySecond in second) 
                {
                    if (arrayFirst.SequenceEqual(arraySecond))
                    {
                        foundArray = true;
                        break;
                    }
                }
                if (foundArray == false) return false;
            }
            return true;
        }
         
    }
}
