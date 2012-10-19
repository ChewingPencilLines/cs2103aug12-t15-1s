using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace ToDo
{
    public class Storage
    {
        string taskStorageFile, settingsFile;

        public Storage(string taskStorageFile, string settingsFile)
        {
            this.taskStorageFile = taskStorageFile;
            this.settingsFile = settingsFile;
        }

        internal bool CreateNewTaskFile(string filename)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<tasks>" +
                            "</tasks>");

                // Create a new element node.
                XmlNode newElem = doc.CreateNode("element", "tasks", "");
                newElem.InnerText = "task1";

                XmlElement root = doc.DocumentElement;
                root.AppendChild(newElem);

                doc.Save("testfile.xml");
            }
            catch (ArgumentNullException)
            {
                CustomMessageBox.Show("Error!", "Task filename was not set!");
            }
            catch (InvalidOperationException)
            {
                CustomMessageBox.Show("Error!", "Failed to create task file.");
            }
            return false;
        }

        internal bool CreateNewSettingsFile(string filename, XmlWriterSettings settings)
        {
            return false;
        }
        
        internal bool AddTask(Task taskToAdd, string id)
        {
            XmlDocument doc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(taskStorageFile);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            reader.MoveToContent();
            reader.Read();
            doc.Load(reader);
            doc.CreateNode("element", id, "");
            XmlSerializer xS = new XmlSerializer(taskToAdd.GetType(), "");
            try
            {
             //   xS.Serialize(doc, taskToAdd);
            } 
            catch (Exception)
            {            
            }
            return true;
        }

        internal bool RemoveTask(Task taskToDelete)
        {
            return true;
            throw new NotImplementedException();
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
    }
}
