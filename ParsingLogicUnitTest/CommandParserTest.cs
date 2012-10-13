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
        CommandParser testParser = new CommandParser();

        [TestMethod]
        public void OperationParseTestTimedSingle()
        {
            Operation op1 = testParser.ParseOperation("task do stuff add Oct 15 5 am");
            return;
        }

        [TestMethod]
        public void OperationParseTestTimedDuo()
        {
            Operation op1 = testParser.ParseOperation("task do stuff add Oct 15 5 am to 6am");
            return;
        }

        [TestMethod]
        public void OperationParseTestDeadline()
        {
            Operation op1 = testParser.ParseOperation("task do stuff add by 9 pm");
            Assert.IsTrue(op1 is OperationAdd);
            Task task1 = ((OperationAdd)op1).GetTask();
            Assert.IsTrue(task1.taskname == "task do stuff");
            Assert.IsTrue(task1 is TaskDeadline);
            TimeSpan deadline = ((TaskDeadline)task1).endtime.TimeOfDay;
            Assert.IsTrue((deadline.Equals(new TimeSpan(21,0,0))));
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
