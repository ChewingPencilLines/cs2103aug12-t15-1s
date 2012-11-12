using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace OperatingUnitTest
{
    /// <summary>
    /// This test is operation unit test,
    /// using "operation.execute" to to check the process of
    /// operation being executed and return response.
    /// contains 6 test cases.
    /// </summary>
  
    [TestClass]
    public class OperationTest
    {
        TaskFloating testTask = new TaskFloating("test", false, -1);
        TaskFloating testTaskNew = new TaskFloating("testa", false, -1);

        Storage testStorage;
        List<Task> testTaskList;
        Response result;
        SortType sortType = SortType.DEFAULT;

        [TestMethod]
        public void OperationAddTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(testTask, sortType);
            result = Op.Execute(testTaskList, testStorage);
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationAddFailTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(null, sortType);
            result = Op.Execute(testTaskList, testStorage);
            Assert.AreEqual(result.FeedbackString, "Failed to add task!");
            return;
        }

        [TestMethod]
        public void OperationUndoAddTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage); 
            result = Op.Undo(testTaskList, testStorage);
            Assert.AreEqual(result.FormatType.ToString(),"DEFAULT");
            return;
        }

        [TestMethod]
        public void OperationDeleteTest()
        {        
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index= new int[2]{1,1};
            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual( "Deleted task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteRangeFailTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index = new int[2] { 1, 4 };
            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual ("Invalid task index!",result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteMultipleTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index = new int[2] { 1, 2 };
            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            Op = new OperationAdd(testTaskNew, sortType);
            Op.Execute(testTaskList, testStorage);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            return;
        }

    }
}
