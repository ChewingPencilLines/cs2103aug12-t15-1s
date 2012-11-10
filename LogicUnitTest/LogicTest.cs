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
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add milk");
            Type type = result.TasksToBeDisplayed[0].GetType();
            string s = result.FormatType.ToString();
            Assert.AreEqual("ToDo.TaskFloating", type.ToString());
            Assert.AreEqual("DEFAULT", s);
            Assert.AreEqual("Added new task \"milk\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void AddTimedTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add test by 2020 jan 5th");
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("ToDo.TaskDeadline", type.ToString());
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void AddTimedFailTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add test feb 29th ");
            Assert.AreEqual("Invalid command input!", result.FeedbackString);
            result = logic.ProcessCommand("add test feb 29th 2016");
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void ComplicatedAddTest()
        {
            Response result;
            result = logic.ProcessCommand(" add \"add 5 and 5\" friday 2pm");
            Assert.AreEqual("Added new task \"add 5 and 5\" successfully.", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        }
        
        [TestMethod]
        public void DeleteByNameTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add abcd");
            result = logic.ProcessCommand("delete abcd");
            Assert.AreEqual("Deleted task \"abcd\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void DeleteByDateTest()
        {
            Response result;
            logic.ProcessCommand("add bb");
            result = logic.ProcessCommand("delete jan");
            Assert.AreEqual("Displaying all tasks within 2013/1/1 0:00 to 2013/1/1 0:00.", result.FeedbackString); 
            return;
        }

        [TestMethod]
        public void DeleteAllTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete all");
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void DeleteDateAllTest()
        {
            Response result;
            logic.ProcessCommand("add xx fri 5pm");
            logic.ProcessCommand("add yy fri 9pm");
            result = logic.ProcessCommand("delete friday all");
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            logic.ProcessCommand("delete all");
            return;
        } 

        [TestMethod]
        public void DeleteByNameMultipleResultTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("delete a");
            Assert.AreEqual("Displaying all tasks matching \"a\".", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void DeleteByNameFailTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add ques");
            logic.ProcessCommand("add quee");
            result = logic.ProcessCommand("delete quse");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void DeleteByIndexTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add cd");
            logic.ProcessCommand("add cde");
            logic.ProcessCommand("delete c");
            result = logic.ProcessCommand("delete 1");
            Assert.AreEqual("Deleted task \"cd\" successfully.", result.FeedbackString);
            result = logic.ProcessCommand("delete -1");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void SearchNameTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aaa");
            result = logic.ProcessCommand("search aaa");
            Assert.AreEqual("Displaying all tasks matching \"aaa\".", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void SearchDateTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aaa by 3th");
            logic.ProcessCommand("add bbb at 3th 7pm");
            result = logic.ProcessCommand("search 3th");
            Assert.AreEqual("Displaying all tasks matching \"3th\".", result.FeedbackString);
            //Assert.AreEqual("aaa", result.TasksToBeDisplayed[0].TaskName);
            //Assert.AreEqual("bbb", result.TasksToBeDisplayed[1].TaskName);
            return;
        }

        [TestMethod]
        public void SearchDateSpecifyTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aba 8th 5pm");
            result = logic.ProcessCommand("search 8th 5pm");
            Assert.AreEqual("Displaying all tasks within 2012/12/8 0:00 to 2012/12/8 17:00.", result.FeedbackString);
            Assert.AreEqual("aba", result.TasksToBeDisplayed[0].TaskName);
            return;
        }

        [TestMethod]
        public void DisplayEmptyTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("display");
            Assert.AreEqual("Displaying all tasks.", result.FeedbackString); 
            return;
        }

        //no feedback known since broken of postpone
        [TestMethod]
        public void PostponeByDateTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add pp 5pm");
            logic.ProcessCommand("add qq 3pm");
            result = logic.ProcessCommand("postpone all today to tmr");
            Assert.AreEqual("Postponed all tasks successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void PostponeByNameTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add pp 5pm");
            result = logic.ProcessCommand("postpone pp to jan");
            Assert.AreEqual("Postponed task \"pp\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void ModifyByIndexTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add bbbb tmr");
            result = logic.ProcessCommand("modify 1 aaaa");
            Assert.AreEqual("Modified task successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void MarkTaskTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa");
            result = logic.ProcessCommand("done aa");
            Assert.AreEqual("Successfully marked \"aa\" as done.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void MarkTaskByDateTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add done1 tmr");
            logic.ProcessCommand("add done2 tmr 3pm");
            result = logic.ProcessCommand("done ALL tmr");
            Assert.AreEqual("Successfully marked all tasks as done.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void SortByNameTest()
        {
            Response result; 
            result = logic.ProcessCommand("sort name");
            Assert.AreEqual("Sorting by name.", result.FeedbackString);
            return;
        }
    }
}
