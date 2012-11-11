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
        CommandParser testCmdParser;

        [TestMethod]
        public void OperationParseSearchDeadlineTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("search by oct 30th");
            Assert.AreEqual("ToDo.OperationSearch", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationParseTestTimedSingle()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task do stuff add Oct 15 5 am");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationParseTestInvalid()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("add task do stuff  by 20");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationParseTestDay()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("add task do stuff by friday 2pm");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationParseTestTimedDuo()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task do stuff add Oct 15 5 am to 6am");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationParseTestDeadline()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task do stuff add by 9 pm");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            //Assert.IsTrue(task1.TaskName == "task do stuff");
            //Assert.IsTrue(task1 is TaskDeadline);
            //TimeSpan deadline = ((TaskDeadline)task1).EndTime.TimeOfDay;
            //Assert.IsTrue((deadline.Equals(new TimeSpan(21,0,0))));
            return;
        }

    }
}
