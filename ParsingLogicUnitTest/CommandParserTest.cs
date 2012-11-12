﻿using System;
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
        public void OperationSearchDeadlineParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("search by 2013 oct 30th 5:49 pm");
            Assert.AreEqual("ToDo.OperationSearch", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationTimedParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task do stuff add sunday morning to wed 13:20 ");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            
            return;
        }

        [TestMethod]
        public void OperationInvalidParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = null;
            op1 = testCmdParser.ParseOperation("add delete modify");
            Assert.IsTrue(op1 == null);
            return;
        }

        [TestMethod]
        public void OperationScheduleParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task  jan 15th midnight - jan 30th morning schedule 3000 hours");
            Assert.AreEqual("ToDo.OperationSchedule", op1.GetType().ToString());
            return;
        }

    }
}
