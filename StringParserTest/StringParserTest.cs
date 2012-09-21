using System;
using System.Collections.Generic;
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
            int[,] expected = new int[,] { { 0, 4 } };
            CollectionAssert.AreEqual(expected , StringParser.FindIndexOfDelimiters(input));
        }

        [TestMethod]
        public void Multiple_FindIndexOfDelimitersTest()
        {
            // index         0123 4567890 12345 6
            string input = "\'add\' hii! \"date\"";
            int[,] expected = new int[,] { { 0, 4 }, { 11, 16 } };
            CollectionAssert.AreEqual(expected, StringParser.FindIndexOfDelimiters(input));
        }

        [TestMethod]
        public void Null_FindIndexOfDelimitersTest()
        {
            string input = "\"add\'";
            int[,] expected = new int[,] { { -1, -1 } };
            CollectionAssert.AreEqual(expected, StringParser.FindIndexOfDelimiters(input));
        }
         
    }
}
