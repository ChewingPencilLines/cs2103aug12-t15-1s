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
            Assert.AreEqual("Added new task \"milk\" successfully.", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteByNameTest()
        {
            Response result;
            logic.ProcessCommand("add abcd");
            result = logic.ProcessCommand("delete abcd");
            Assert.AreEqual("Deleted task \"abcd\" successfully.", result.FeedbackString);
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
            Assert.AreEqual("Deleted all tasks successfully.", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        } 

        [TestMethod]
        public void DeleteByNameMultipleResultTest()
        {
            Response result;
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete a");
            Assert.AreEqual("Showing tasks matching \"a\".", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteByNameFailTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete bb");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DeleteByIndexTest()
        {
            Response result;
            logic.ProcessCommand("add cd");
            logic.ProcessCommand("add cde");
            logic.ProcessCommand("delete c");
            result = logic.ProcessCommand("delete 1");
            Assert.AreEqual("Deleted task \"cd\" successfully.", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void SearchNameTest()
        {
            Response result;
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("search aaa");
            Assert.AreEqual("Showing tasks matching \"aaa\".", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void SearchDateTest()
        {
            Response result;
            logic.ProcessCommand("delete today");
            logic.ProcessCommand("add aaa 5pm");
            logic.ProcessCommand("add bbb 7pm");
            result = logic.ProcessCommand("search today");
            Assert.AreEqual("Showing tasks with time constraints.", result.FeedbackString);
           // Assert.AreEqual("aaa", result.TasksToBeDisplayed[0].TaskName);
            //Assert.AreEqual("bbb", result.TasksToBeDisplayed[1].TaskName);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void SearchDateSpecifyTest()
        {
            Response result;
            logic.ProcessCommand("add aba 5pm");
            result = logic.ProcessCommand("search 5pm");
            Assert.AreEqual("Showing tasks with time constraints.", result.FeedbackString);
          //  Assert.AreEqual("aba", result.TasksToBeDisplayed[0].TaskName);
            logic.ProcessCommand("delete all");
            return;
        }

        [TestMethod]
        public void DisplayEmptyTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("display");
            Assert.AreEqual("Showing tasks.", result.FeedbackString); 
            return;
        }

        //no feedback known since broken of postpone
        [TestMethod]
        public void PostponeByDateTest()
        {
            Response result; 
            logic.ProcessCommand("add pp 5pm");
            logic.ProcessCommand("add qq 3pm");
            result = logic.ProcessCommand("postpone today tmr");
            Assert.AreEqual("Postponed task \"pp\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void PostponeByNameTest()
        {
            Response result;
            logic.ProcessCommand("add pp 5pm");
            result = logic.ProcessCommand("postpone pp jan");
            Assert.AreEqual("Postponed task \"pp\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void ModifyByIndexTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add bbbb");
            result = logic.ProcessCommand("modify 1 aaaa");
            Assert.AreEqual("Modified task \"bbbb\" into \"aaaa\"  successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void MarkTaskTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa");
            result = logic.ProcessCommand("done aa");
            Assert.AreEqual("Successfully marked \"aa\" as done.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void SortByNameTest()
        {
            Response result; 
            result = logic.ProcessCommand("sort name");
            Assert.AreEqual("Sorting by Name.", result.FeedbackString);
            return;
        }
    }
}
