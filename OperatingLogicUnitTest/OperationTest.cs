using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace OperatingUnitTest
{
    [TestClass]
    public class OperationTest
    {
        TaskFloating task = new TaskFloating("test", false, -1);
        TaskFloating task1 = new TaskFloating("testa", false, -1);

        Storage storagetest;
        List<Task> taskList;
        Response result;
        SortType sortType = SortType.DEFAULT;

        [TestMethod]
        public void OperationAddTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(task, sortType);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationAddFailTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(null, sortType);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Failed to add task!");
            return;
        }

        [TestMethod]
        public void OperationUndoAddTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(task, sortType);
            Op.Execute(taskList, storagetest); 
            result = Op.Undo(taskList, storagetest);
            Assert.AreEqual(result.FormatType.ToString(),"DEFAULT");
          //  Assert.AreEqual(result.FeedbackString, "Undid last operation.");
            return;
        }

        [TestMethod]
        public void OperationDeleteTest()
        {        
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            int[] index= new int[2]{1,1};
            OperationAdd Op = new OperationAdd(task, sortType);
            Op.Execute(taskList, storagetest);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(taskList, storagetest);
            Assert.AreEqual( "Deleted task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteRangeFailTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            int[] index = new int[2] { 1, 4 };
            OperationAdd Op = new OperationAdd(task, sortType);
            Op.Execute(taskList, storagetest);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(taskList, storagetest);
            Assert.AreEqual ("Invalid task index!",result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteMultipleTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            int[] index = new int[2] { 1, 2 };
            OperationAdd Op = new OperationAdd(task, sortType);
            Op.Execute(taskList, storagetest);
            Op = new OperationAdd(task1, sortType);
            Op.Execute(taskList, storagetest);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(taskList, storagetest);
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            return;
        }

    }
}
