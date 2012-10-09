﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;

namespace ToDo
{
    public class XML
    {
        public XML()
        {   }

        public void WriteXML(TaskList taskList)
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