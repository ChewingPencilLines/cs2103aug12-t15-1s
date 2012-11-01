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

        [TestMethod]
        public void OperationAddTest()
        {
            string result;
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();
            OperationAdd Op = new OperationAdd(task);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result, "Added \"test\" successfully.");

            return;
        }

        [TestMethod]
        public void OperationDeleteTest()
        {
            string result;
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();
            int[] index= new int[2];
            index[0] = -1;
            OperationDelete Op = new OperationDelete("", index, null, null, null);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result, "Invalid task index!");
            return;
        }

        [TestMethod]
        public void OperationUndoAddTest()
        {
            string result;
            storagetest = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            taskList = storagetest.LoadTasksFromFile();
            OperationAdd Op = new OperationAdd(task);
            result = Op.Execute(taskList, storagetest);
            Assert.AreEqual(result, "Added \"test\" successfully.");
            result = Op.Undo(taskList,storagetest);
            Assert.AreEqual(result, "Deleted task \"test\" successfully.");
            return;
        }
    }
}
