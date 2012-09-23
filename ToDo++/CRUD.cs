using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;

namespace ToDo
{
    public class CRUD
    {
        private TaskList taskList;
        private Stack<Operation> undoStack;
        private Stack<Operation> redoStack;

        public CRUD()
        {
            taskList = new TaskList();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
        }

        //Need to take in an instance of Operation to execute
        public Responses ExecuteOperation(Operation operation)
        {
            try
            {
                Task taskToAdd = operation.GetTask();
                taskList.Add(taskToAdd);
                undoStack.Push(operation);
                WriteXML();

                return Responses.ADD_SUCCESS;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return Responses.ERROR;
            }

        }

        public string ExecuteOperation2(Operation operation)
        {
            try
            {
                Task taskToAdd = operation.GetTask();
                taskList.Add(taskToAdd);
                undoStack.Push(operation);
                WriteXML();

                return "ok";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return e.ToString();
            }

        }

        public void WriteXML()
        {

            // use reflection to get all derived types
            var knownTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => typeof(TaskList).IsAssignableFrom(t) || typeof(
                FloatingTask).IsAssignableFrom(t) || typeof(DeadlineTask).IsAssignableFrom(t)).ToArray();

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
