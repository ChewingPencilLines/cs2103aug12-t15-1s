using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDo
{
    public class Storage
    {
        XmlWriter taskWriter, settingsWriter;

        public Storage(string taskStorageFile, string settingsFile)
        {
            // Create an XmlWriterSettings object with the correct options. 
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = false;

            try
            {
                using (taskWriter = XmlWriter.Create(taskStorageFile, settings))
                {
                    taskWriter.WriteStartDocument();
                }

                using (settingsWriter = XmlWriter.Create(settingsFile, settings))
                {
                    settingsWriter.WriteStartDocument();
                }
            }            
            catch (ArgumentNullException nullfilename)
            {
                CustomMessageBox.Show("Error!", nullfilename.ParamName + " name was not set!");
            }
            catch (InvalidOperationException failedCreation)
            {
                CustomMessageBox.Show("Error!",  "Failed to create: " + failedCreation.Source);
            }
        }

        internal bool AddTask(Task taskToAdd, int id)
        {

            return true;
            throw new NotImplementedException();
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
