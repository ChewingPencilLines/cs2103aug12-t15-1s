using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToDo;
namespace ParsingLogicUnitTest
{
    [TestClass]
    public class StorageTest
    {
        [TestMethod]
        public void TaskFileStorageTest()
        {
            Storage IO = new Storage("a", "b");
            IO.CreateNewTaskFile("notused.xml");
        }
    }
}
