using System;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System.Linq;
using System.Collections.Generic; 
using ToDo;

namespace LogicUnitTest
{
    [TestClass]
    public class LogicTest
    {
        Logic logic = new Logic();

        [TestMethod]
        public void BasicAddTest()
        {
            Response result;
            result = logic.ProcessCommand("add milk");
            Assert.AreEqual(result.FeedbackString, "Added new task \"milk\" successfully.");
            logic.ProcessCommand("delete milk");
            return;
        }

        [TestMethod]
        public void DeleteByNameTest()
        {
            Response result;
            logic.ProcessCommand("add abcd");
            result = logic.ProcessCommand("delete abcd");
            Assert.AreEqual(result.FeedbackString, "Deleted task \"abcd\" successfully.");
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteAllTest()
        {
            Response result;
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete all");
            Assert.AreEqual(result.FeedbackString, "Deleted all tasks successfully.");
            return;
        } 

        [TestMethod]
        public void DeleteByNameMultipleResultTest()
        {
            Response result;
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete a");
            Assert.AreEqual(result.FeedbackString, "Showing tasks matching \"a\".");
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteByNameFailTest()
        {
            Response result;
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete bb");
            Assert.AreEqual(result.FeedbackString, "No matching tasks found!");
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteByIndexTest()
        {
            Response result;
            logic.ProcessCommand("add cd");
            logic.ProcessCommand("add cde");
            result = logic.ProcessCommand("delete c");
            result = logic.ProcessCommand("delete 1");
            Assert.AreEqual(result.FeedbackString, "Deleted task \"cd\" successfully.");
            logic.ProcessCommand("delete all");
            return;
        }

    }
}
