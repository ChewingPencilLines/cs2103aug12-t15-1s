using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;

namespace ToDo
{
    public class Storage
    {
        public Storage()
        {   
            
        }
        /*
        public bool WriteXML(Task task)
        {

            // use reflection to get all derived types
            var knownTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => typeof(List<Task>).IsAssignableFrom(t) || typeof(
                TaskFloating).IsAssignableFrom(t) || typeof(TaskDeadline).IsAssignableFrom(t)).ToArray();

            // prepare to serialize a car object
            XmlSerializer writer = new XmlSerializer(typeof(List<Task>), knownTypes);

            //System.Xml.Serialization.XmlSerializer writer =
            //    new System.Xml.Serialization.XmlSerializer(typeof(TaskList));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                @"..\..\StorageofTaskList.xml");
            writer.Serialize(file, taskList);
            file.Close();
            return true;
        }*/

        internal bool AddTask(Task taskToAdd)
        {
            return true;
            throw new NotImplementedException();
        }

        internal bool RemoveTask(Task taskToDelete)
        {
            return true;
            throw new NotImplementedException();
        }
    }
}
