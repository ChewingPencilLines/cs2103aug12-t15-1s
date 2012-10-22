using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDo
{
    public class Storage
    {
        string taskStorageFile, settingsFile;

        /// <summary>
        /// Constructs a Storage I/O handler class, creating two XML files for task storage and settings storage using
        /// the specified taskStorageFile and settingsFile as their respective filenames.
        /// </summary>
        /// <param name="taskStorageFile">String representing the filename to create the task storage XML file.</param>
        /// <param name="settingsFile">String representing the filename to create the settings XML file.</param>
        /// <returns>Nothing</returns>
        public Storage(string taskStorageFile, string settingsFile)
        {
            this.taskStorageFile = taskStorageFile;
            this.settingsFile = settingsFile;
            if (!ValidateTaskFile(this.taskStorageFile))
                CreateNewTaskFile(this.taskStorageFile);
            CreateNewSettingsFile(this.settingsFile);
        }

        private bool ValidateTaskFile(string filename)
        {
            //check for well-formedness
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            try
            {
                using (XmlReader reader = XmlReader.Create(filename, settings))
                {
                    // check for "tasks" node
                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                        if (reader.Name == "tasks")
                            return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        private bool CreateNewTaskFile(string filename)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<tasks>" +
                            "</tasks>");              
                doc.Save(filename);                
            }
            catch (ArgumentNullException)
            {
                CustomMessageBox.Show("Error!", "Task filename was not set!");
                return false;
            }
            catch (InvalidOperationException)
            {
                CustomMessageBox.Show("Error!", "Failed to create task file.");
                return false;
            }
            return true;
        }

        internal bool CreateNewSettingsFile(string filename)
        {
            return false;
        }

        private void LoadSettingsFile()
        {
            //System.IO.StreamReader file;

            //try
            //{
            //    file = new System.IO.StreamReader(fileName);
            //    System.Xml.Serialization.XmlSerializer writer =
            //    new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
            //    settingsList = (SettingsList)writer.Deserialize(file);
            //    file.Close();
            //}
            //catch (FileNotFoundException)
            //{
            //    AlertBox.Show("Settings File Not Found, new file will be created");
            //    //MessageBox.Show("Settings File Not Found, new file will be created");
            //    WriteToFile();
            //}
            //catch (InvalidOperationException)
            //{
            //    MessageBox.Show("There was an error with the Settings File, a new file will be created");
            //    WriteToFile();
            //}
        }

        private void UpdateSettingsFile()
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

            //System.Xml.Serialization.XmlSerializer writer =
            //new System.Xml.Serialization.XmlSerializer(typeof(SettingsList));
            //writer.Serialize(file, settingsList);
            //file.Close();
        }

        internal bool AddTaskToFile(Task taskToAdd)
        {
            try
            {
                XDocument doc = XDocument.Load(taskStorageFile);
                XElement newTaskElem = taskToAdd.ToXElement();
                doc.Root.Add(newTaskElem);
                doc.Save(taskStorageFile);
            }
            catch (Exception e)
            {
                CustomMessageBox.Show("Warning!", "A problem was encoutered saving the new task to file.");
                return false;
            }
            return true;
        }

        internal bool RemoveTaskFromFile(Task taskToDelete)
        {
            XDocument doc = XDocument.Load(taskStorageFile);
            
            var task =  from node in doc.Descendants("Task")
                        let attr = node.Attribute("id")
                        where attr != null && attr.Value == taskToDelete.ID.ToString()
                        select node;
            task.ToList().ForEach(x => x.Remove());

            doc.Save(taskStorageFile);

            return true;            
        }

        internal List<Task> LoadTasksFromFile()
        {
            List<Task> taskList = new List<Task>();
            XDocument doc = XDocument.Load(taskStorageFile);
            IEnumerable<XElement> tasks =
                (from task in doc.Root.Elements("Task") select task);
            foreach(XElement task in tasks)
            {
                Task addTask = GenerateTaskFromXElement(task);
                if (addTask == null)
                {
                    CustomMessageBox.Show("Warning!", "Task storage file seems corrupted. Error reading from it!");
                }
                taskList.Add(addTask);
            }
            return taskList;
        }

        private Task GenerateTaskFromXElement(XElement task)
        {
            Task newTask = null;
            string type = task.Attribute("type").Value;
            int id = Int32.Parse(task.Attribute("id").Value);
            string taskName = task.Element("Name").Value;
            DateTime startTime, endTime;
            bool state;

            if ( task.Element("Name").Value == "True" ) state = true;
            else state = false;

            switch (type)
            {
                case "Floating":
                    newTask = new TaskFloating(taskName, state, id);
                    break;
                case "Deadline":
                    endTime = DateTime.Parse(task.Element("EndTime").Value);
                    newTask = new TaskDeadline(taskName, endTime, state, id);
                    break;
                case "Event":
                    endTime = DateTime.Parse(task.Element("EndTime").Value);
                    startTime = DateTime.Parse(task.Element("StartTime").Value);
                    newTask = new TaskDeadline(taskName, endTime, state, id);
                    break;
            }

            return newTask;
        }
    }
}
