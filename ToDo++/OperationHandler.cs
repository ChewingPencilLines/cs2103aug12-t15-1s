using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;

namespace ToDo
{
    public class OperationHandler
    {
        private TaskList taskList;
        
        public OperationHandler()
        {
             
        }

        //Need to take in an instance of Operation to execute
        public Responses ExecuteOperation(Operation operation)
        {
            try
            {
                Task taskToAdd = operation.GetTask();
                taskList.Add(taskToAdd);
               
                WriteXML();

                return Responses.ADD_SUCCESS;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Responses.ERROR;
            }

        }

        public void WriteXML()
        {

            // use reflection to get all derived types
            var knownTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => typeof(TaskList).IsAssignableFrom(t) || typeof(
                TaskFloating).IsAssignableFrom(t) || typeof(TaskDeadline).IsAssignableFrom(t)).ToArray();

            // prepare to serialize a car object
            XmlSerializer writer = new XmlSerializer(typeof(TaskList), knownTypes);

            //System.Xml.Serialization.XmlSerializer writer =
            //    new System.Xml.Serialization.XmlSerializer(typeof(TaskList));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                @"C:\Users\Raaj\Desktop\SerializationOverview.xml");
            writer.Serialize(file, taskList);
            file.Close();
        }
    }

}
