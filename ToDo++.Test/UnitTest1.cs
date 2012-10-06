using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodOpExecute()
        {
           CRUD crudClass = new CRUD();

           Task newTask = new FloatingTask("buy milk");
           Operation newOperation = new OperationAdd(newTask,OperationType.ADD_FLOATING);

           Responses actualResponse=crudClass.ExecuteOperation(newOperation);
           Responses expectedResponse = Responses.ADD_SUCCESS;

           Assert.AreEqual(expectedResponse, actualResponse, "Failure to Add");

        }

        [TestMethod]
        public void TestMethodOpExecute2()
        {
            CRUD crudClass = new CRUD();

            Task newTask = new FloatingTask("buy milk");
            Operation newOperation = new OperationAdd(newTask, OperationType.ADD_FLOATING);

            string actualResponse = crudClass.ExecuteOperation2(newOperation);
            string expectedResponse = "ok";

            Assert.AreEqual(expectedResponse, actualResponse, "Failure to Add");

        }


    }
}
