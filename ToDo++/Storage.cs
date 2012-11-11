using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Windows.Forms;

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
            if (!ValidateTaskFile())
                CreateNewTaskFile();
        }

        private bool ValidateTaskFile()
        {
            //check for well-formedness
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            try
            {
                using (XmlReader reader = XmlReader.Create(taskStorageFile, settings))
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
                Logger.Error(ex, "ValidateTaskFile::Storage");
                return false;
            }            
        }

        internal bool CreateNewTaskFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<tasks>" +
                            "</tasks>");              
                doc.Save(taskStorageFile);                
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, "CreateNewTaskFile::Storage");
                UserInputBox.Show("Error!", "Task filename was not set!");
                return false;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, "CreateNewTaskFile::Storage");
                UserInputBox.Show("Error!", "Failed to create task file.");
                return false;
            }
            return true;
        }

        internal SettingInformation LoadSettingsFromFile()
        {
            SettingInformation settingInfo = new SettingInformation();
            try
            {
                using (StreamReader file = new StreamReader(settingsFile))
                {
                    string xml = file.ReadToEnd();  
                    settingInfo = xml.Deserialize<SettingInformation>();
                    file.Close();
                }
            }
            // Write default settings if file not found or invalid.
            catch (FileNotFoundException e)
            {
                Logger.Error(e, "LoadSettingsFromFile::Storage");
                AlertBox.Show("Settings file not found.\nNew file will be created");
                WriteSettingsToFile(settingInfo);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                Logger.Error(e, "LoadSettingsFromFile::Storage");
                AlertBox.Show("There was an error with the settings file, a new file will be created");
                WriteSettingsToFile(settingInfo);
            }
            return settingInfo;
        }

        internal bool WriteSettingsToFile(SettingInformation settingInfo)
        {
            try
            {
                /*
                StreamWriter file = new StreamWriter(settingsFile);
                XmlSerializer writer = new XmlSerializer(typeof(ToDo.SettingInformation.MiscSettings));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                writer.Serialize(file, settingInfo.misc, ns);*/
                StreamWriter file = new StreamWriter(settingsFile);
                file.Write(settingInfo.ToXML());
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e, "WriteSettingsToFile::Storage");
                AlertBox.Show("Failed to write to settings file!");
                return false;
            }
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
                Logger.Error(e, "AddTaskToFile::Storage");
                AlertBox.Show("A problem was encoutered saving the new task to file.");
                return false;
            }
            return true;
        }

        internal bool RemoveTaskFromFile(Task taskToDelete)
        {
            XDocument doc = XDocument.Load(taskStorageFile);
            
            var taskNode =  from node in doc.Descendants("Task")
                        let attr = node.Attribute("id")
                        where attr != null && attr.Value == taskToDelete.ID.ToString()
                        select node;

            if (taskNode == null) return false;

            try
            {
                taskNode.ToList().ForEach(x => x.Remove());
            }
            catch (Exception e)
            {
                Logger.Error(e, "RemoveTaskFromFile::Storage");
                return false;
            }

            doc.Save(taskStorageFile);

            return true;            
        }
        
        internal bool UpdateTask(Task taskToUpdate)
        {
            XDocument doc = XDocument.Load(taskStorageFile);

            var task = from node in doc.Descendants("Task")
                       let attr = node.Attribute("id")
                       where attr != null && attr.Value == taskToUpdate.ID.ToString()
                       select node;

            if (task == null) return false;

            try
            {
                XElement taskNode = task.First();
                taskNode.ReplaceWith(taskToUpdate.ToXElement());
            }
            catch (Exception e)
            {
                Logger.Error(e, "UpdateTask::Storage");
                return false;
            }

            doc.Save(taskStorageFile);
            return true;
        }
               
        internal bool MarkTaskAs(Task taskToMarkAsDone, bool done)
        {
            XDocument doc = XDocument.Load(taskStorageFile);

            var task = from node in doc.Descendants("Task")
                       let attr = node.Attribute("id")
                       where attr != null && attr.Value == taskToMarkAsDone.ID.ToString()
                       select node;

            if (task == null) return false;

            try
            {
                if(done)
                    task.First().Element("Done").ReplaceNodes("True");
                else
                    task.First().Element("Done").ReplaceNodes("False");
            }
            catch (Exception e)
            {
                Logger.Error(e, "MarkTaskAs::Storage");
                return false;
            }

            doc.Save(taskStorageFile);
            return true;
        }

        public List<Task> LoadTasksFromFile()
        {
            List<Task> taskList = new List<Task>();
            try
            {
                XDocument doc = XDocument.Load(taskStorageFile);
                IEnumerable<XElement> tasks =
                    (from task in doc.Root.Elements("Task") select task);
                foreach (XElement task in tasks)
                {
                    try
                    {
                        Task addTask = GenerateTaskFromXElement(task);
                        if (addTask == null)
                        {
                            return null;
                        }
                        taskList.Add(addTask);
                    }
                    catch (ArgumentNullException e)
                    {
                        Logger.Error(e, "LoadTasksFromFile::Storage");
                        return null;
                    }
                    catch (TaskFileCorruptedException e)
                    {
                        Logger.Error(e, "LoadTasksFromFile::Storage");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "LoadTasksFromFile::Storage");
                return null;
            }
            return taskList;
        }
  
        private Task GenerateTaskFromXElement(XElement task)
        {
            Task newTask = null;

            try
            {
                string type = task.Attribute("type").Value;
                int id = Int32.Parse(task.Attribute("id").Value);
                string taskName = task.Element("Name").Value;
                DateTime startTime, endTime;
                DateTimeSpecificity isSpecific = new DateTimeSpecificity();

                XElement DTSpecElement = task.Element("DateTimeSpecificity");
                if (DTSpecElement != null) isSpecific = DTSpecElement.FromXElement<DateTimeSpecificity>();
                bool state;

                if (task.Element("Done").Value == "True") state = true;
                else state = false;
                switch (type)
                {
                    case "Floating":
                        newTask = new TaskFloating(taskName, state, id);
                        break;
                    case "Deadline":
                        endTime = DateTime.Parse(task.Element("EndTime").Value);
                        newTask = new TaskDeadline(taskName, endTime, isSpecific, state, id);
                        break;
                    case "Event":
                        endTime = DateTime.Parse(task.Element("EndTime").Value);
                        startTime = DateTime.Parse(task.Element("StartTime").Value);
                        newTask = new TaskEvent(taskName, startTime, endTime, isSpecific, state, id);
                        break;
                }
            }
            catch (NullReferenceException)
            {
                throw new TaskFileCorruptedException();
            }
            return newTask;

        }
    }
}
