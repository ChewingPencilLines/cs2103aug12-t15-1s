using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace OperatingLogicUnitTest
{
    [TestClass]
    public class OperationTest
    {
        TaskFloating task = new TaskFloating("test", false, -1);
        Storage storagetest;
        List<Task> taskList;
        Response result;

        [TestMethod]
        public void OperationAddTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();
           
            OperationAdd Op = new OperationAdd(task);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Added new task \"test\" successfully.");
            return;
        }

        [TestMethod]
        public void OperationAddFailTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(null);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Failed to add task!");
            return;
        }

        [TestMethod]
        public void OperationUndoAddTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(task);
            Op.Execute(taskList, storagetest); 
            result = Op.Undo(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Undid last operation.");
            return;
        }

        [TestMethod]
        public void OperationUndoAddFailTest()
        {
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(task); 
            result = Op.Undo(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Cannot undo last executed operation!");
            return;
        }

        [TestMethod]
        public void OperationDeleteTest()
        {        
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();

            int[] index= new int[2];
            index[0] = 1; index[1] = 1;
            OperationAdd Op = new OperationAdd(task);
            Op.Execute(taskList, storagetest);
            OperationDelete Op1 = new OperationDelete("", index, null,null, null,false, SearchType.NONE);
            result = Op1.Execute(taskList, storagetest);
            Assert.AreEqual(result.FeedbackString, "Deleted task \"test\" successfully.");
            return;
        }

        
    }
}
