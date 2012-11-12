﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System.Linq;
using System.Collections.Generic; 
using ToDo;

namespace IntegrationTests
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
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            Assert.AreEqual("INVALID_COMMAND", result.Result.ToString());
            result = logic.ProcessCommand("add test feb 29th 2016");
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void ComplicatedAddTest()
        {
            Response result;
            result = logic.ProcessCommand(" add \"add 5 and 5\" feb 3 2pm to feb 5");
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("Added new task \"add 5 and 5\" successfully.", result.FeedbackString);
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            logic.ProcessCommand("add abd");
            result = logic.ProcessCommand("delete abcd");
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("abd", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("Deleted task \"abcd\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void DeleteByDateTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add bb JAN 3");
            logic.ProcessCommand("add bA JAN 5");
            result = logic.ProcessCommand("delete jan");
            Assert.AreEqual("Displaying all tasks within 2013/1/1 0:00 to 2013/1/31 23:59.", result.FeedbackString);
            Assert.AreEqual("bb", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("bA", result.TasksToBeDisplayed[1].TaskName);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            Assert.AreEqual(0, result.TasksToBeDisplayed.Count);
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS_MULTIPLE", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void DeleteDateAllTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add xx fri 5pm");
            logic.ProcessCommand("add yy fri 9pm");
            result = logic.ProcessCommand("delete friday all");
          //  Assert.AreEqual(0, result.TasksToBeDisplayed.Count);
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS_MULTIPLE", result.Result.ToString());
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
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("FAILURE", result.Result.ToString());
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
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            result = logic.ProcessCommand("delete -1");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("FAILURE", result.Result.ToString());
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
            Assert.AreEqual("aaa",result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void SearchDateTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aaa by dec 3");
            logic.ProcessCommand("add bbb dec 3 7pm");
            result = logic.ProcessCommand("search dec");
            Assert.AreEqual("Displaying all tasks within 2012/12/1 0:00 to 2012/12/31 23:59.", result.FeedbackString);
            Assert.AreEqual("aaa", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("bbb", result.TasksToBeDisplayed[1].TaskName);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void SearchDateSpecifyTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aba feb 8th 5pm");
            result = logic.ProcessCommand("search  feb 8th 5pm");
            Assert.AreEqual("Displaying all tasks within 2013/2/8 0:00 to 2013/2/8 17:00.", result.FeedbackString);
            Assert.AreEqual("aba", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void DisplayEmptyTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("display");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("FAILURE", result.Result.ToString());
            return;
        }

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
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS_MULTIPLE", result.Result.ToString());
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
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            Assert.AreEqual("aaaa", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void ModifyByIndexFailTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add bbbb tmr");
            result = logic.ProcessCommand("modify -1");
            Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("FAILURE", result.Result.ToString());
            result = logic.ProcessCommand("modify 3");
            Assert.AreEqual("Invalid task index!", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("INVALID_TASK", result.Result.ToString());
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
            bool st = result.TasksToBeDisplayed[0].DoneState;
            Assert.AreEqual(true, st);
            Assert.AreEqual("Successfully marked \"aa\" as done.", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
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
            bool st = false;
            foreach (Task task in result.TasksToBeDisplayed)
            {
                st = task.DoneState;
                Assert.AreEqual(true, st);
            }
            Assert.AreEqual("Successfully marked all tasks as done.", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS_MULTIPLE", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void SortByNameTest()
        {
            Response result; 
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa by feb");
            logic.ProcessCommand("add bb jan");
            result = logic.ProcessCommand("sort name");
            Assert.AreEqual("Sorting by name.", result.FeedbackString);
            Assert.AreEqual("NAME", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("aa", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("ToDo.TaskDeadline", type.ToString());
            type = result.TasksToBeDisplayed[1].GetType();
            Assert.AreEqual("bb", result.TasksToBeDisplayed[1].TaskName);
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            return;
        }

        [TestMethod]
        public void SortByDateTest()
        {
            Response result;
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add aa BY feb");
            logic.ProcessCommand("add bb jan");
            result = logic.ProcessCommand("SORT DATE");
            Assert.AreEqual("Sorting by date.", result.FeedbackString);
            Assert.AreEqual("DATE_TIME", result.FormatType.ToString());
            Assert.AreEqual("SUCCESS", result.Result.ToString());
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("aa", result.TasksToBeDisplayed[0].TaskName);
            Assert.AreEqual("ToDo.TaskDeadline", type.ToString());
            type = result.TasksToBeDisplayed[1].GetType();
            Assert.AreEqual("bb", result.TasksToBeDisplayed[1].TaskName);
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            return;
        }

        [TestMethod]
        public void MultipleKeywordsTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add aa delete");
            Assert.AreEqual("Invalid command input!", result.FeedbackString);
            Assert.AreEqual("DEFAULT", result.FormatType.ToString());
            Assert.AreEqual("INVALID_COMMAND", result.Result.ToString());
            result = logic.ProcessCommand("postpone delete");
            Assert.AreEqual("Invalid command input!", result.FeedbackString);
            Assert.AreEqual("INVALID_COMMAND", result.Result.ToString());
            result = logic.ProcessCommand("postpone \"delete\"");
            Assert.AreEqual("Trying to postpone a task by a more specific time than it allows!", result.FeedbackString);
            Assert.AreEqual("INVALID_TASK", result.Result.ToString());
            return;
        }

        [TestMethod]
        public void CheckSpacesTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add  a  3  pm to  9 PM");
            Assert.AreEqual("Added new task \"a\" successfully.", result.FeedbackString);
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            return;
        }

        [TestMethod]
        public void TimeRangeTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            result = logic.ProcessCommand("add a morning 3am to 5 am");
            Assert.AreEqual("Added new task \"a\" successfully.", result.FeedbackString);
            Type type = result.TasksToBeDisplayed[0].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            result = logic.ProcessCommand("add a morning 4th");
            Assert.AreEqual("Added new task \"a\" successfully.", result.FeedbackString);
            type = result.TasksToBeDisplayed[1].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            result = logic.ProcessCommand("add a morning evening night");
            Assert.AreEqual("Added new task \"a\" successfully.", result.FeedbackString);
            type = result.TasksToBeDisplayed[2].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            return;
        }

        [TestMethod]
        public void ScheduleTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add a tmr 4am to 5 am");
            logic.ProcessCommand("add a tmr afternoon");
            result = logic.ProcessCommand("schedule aaa 1 hour tmr morning");
            Assert.AreEqual("Scheduled new task \"aaa\" successfully.", result.FeedbackString);
            Type type = result.TasksToBeDisplayed[2].GetType();
            Assert.AreEqual("ToDo.TaskEvent", type.ToString());
            return;
        }

        [TestMethod]
        public void ScheduleFailTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add a tmr 4am to 10 am");
            logic.ProcessCommand("add a tmr afternoon");
            result = logic.ProcessCommand("schedule aaa 5 hours tmr morning");
            Assert.AreEqual("Task could not be scheduled within specified time range (no free slot)!", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void ScheduleStrictTest()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add a tmr 4am to 10:01am");
            logic.ProcessCommand("add a tmr afternoon");
            result = logic.ProcessCommand("schedule aaa 2 hours tmr morning");
            Assert.AreEqual("Task could not be scheduled within specified time range (no free slot)!", result.FeedbackString);
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add a tmr 4am to tmr 10 am");
            logic.ProcessCommand("add a tmr afternoon");
            result = logic.ProcessCommand("schedule aaa 2 hours tmr morning");
            Assert.AreEqual("Scheduled new task \"aaa\" successfully.", result.FeedbackString);
        }

        [TestMethod]
        public void ScheduleStrictTest2()
        {
            Response result;
            logic.ProcessCommand("display");
            logic.ProcessCommand("delete all");
            logic.ProcessCommand("add a 1/6/13  to 5/6/13");
            logic.ProcessCommand("add a 7/6/13  to 30/6/13 ");
            result = logic.ProcessCommand("schedule aaa 1 day june");
            Assert.AreEqual("Scheduled new task \"aaa\" successfully.", result.FeedbackString);
            return;
        }
    }
}
