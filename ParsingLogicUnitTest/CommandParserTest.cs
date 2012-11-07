using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace CommandParserTest
{
    [TestClass]
    public class CommandParserTest
    {
        StringParser testStrParser;
        CommandParser testCmdParser;
/*
        [TestMethod]
        public void OperationParseSearchDeadlineTest()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("search by oct 30th");
            return;
        }

        [TestMethod]
        public void OperationParseTestTimedSingle()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("task do stuff add Oct 15 5 am");
            return;
        }

        [TestMethod]
        public void OperationParseTestInvalid()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("add task do stuff  by 20");
            return;
        }

        [TestMethod]
        public void OperationParseTestDay()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("add task do stuff by friday 2pm");
            return;
        }

        [TestMethod]
        public void OperationParseTestTimedDuo()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("task do stuff add Oct 15 5 am to 6am");
            return;
        }

        [TestMethod]
        public void OperationParseTestDeadline()
        {
            testStrParser = new StringParser();
            testCmdParser = new CommandParser(ref testStrParser);
            Operation op1 = testCmdParser.ParseOperation("task do stuff add by 9 pm");
            Assert.IsTrue(op1 is OperationAdd);
            //Task task1 = ((OperationAdd)op1).NewTask;
            //Assert.IsTrue(task1.TaskName == "task do stuff");
            //Assert.IsTrue(task1 is TaskDeadline);
            //TimeSpan deadline = ((TaskDeadline)task1).EndTime.TimeOfDay;
            //Assert.IsTrue((deadline.Equals(new TimeSpan(21,0,0))));
            return;
        }

        /*
        [TestMethod]
        public void IndexSortTest()
        {
            List<int[]> testIndexes = new List<int[]>();
            testIndexes.Add(new int[2] { 6, 10 });
            testIndexes.Add(new int[2] { 0, 4 });
            testIndexes.Add(new int[2] { 3, 7 });
            testIndexes.Add(new int[2] { 1, 2 });
            List<int[]> expectedResult = new List<int[]>{
                new int[2] { 0, 4 },
                new int[2] { 1, 2 },
                new int[2] { 3, 7 },
                new int[2] { 6, 10 }
            };            
            testParser.SortIndexes(ref testIndexes);
            CollectionAssert.AreEqual(
                testIndexes.SelectMany(x => x).ToList(),
                expectedResult.SelectMany(x => x).ToList()
                );
        }

        [TestMethod]
        public void IndexRemoveTest()
        {
            List<int[]> testIndexes = new List<int[]>();
            testIndexes.Add(new int[2] { 0, 4 });
            testIndexes.Add(new int[2] { 1, 2 });
            testIndexes.Add(new int[2] { 3, 7 });
            testIndexes.Add(new int[2] { 6, 10 });
            List<int[]> expectedResult = new List<int[]>{
                new int[2] { 0, 4 },
                new int[2] { 3, 7 }
            };
            testParser.RemoveBadIndexes(ref testIndexes);
            CollectionAssert.AreEqual(
                testIndexes.SelectMany(x => x).ToList(),
                expectedResult.SelectMany(x => x).ToList()
                );
        }
        */
    }
}
