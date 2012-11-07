using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    public class TaskFloating : Task
    {
        public TaskFloating(string taskName, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
        {
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Floating"),
                            new XElement("Name", taskName),
                            new XElement("Done", doneState.ToString())
                            );
            return task;
        }

        public override bool IsWithinTime(DateTimeSpecificity isSpecific, DateTime? start, DateTime? end)
        {
            return false;
        }
        
        public override void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific)
        {
            startTime = null;
            endTime = null;
            specific = new DateTimeSpecificity();
        }
    }
}