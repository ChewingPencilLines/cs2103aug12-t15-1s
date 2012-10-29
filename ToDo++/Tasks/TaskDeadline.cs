using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ToDo
{
    class TaskFloating : Task
    {
        private struct IsSpecific
        {
            public DateSpecificity endDate;
            DateSpecificity EndDate
            {
                get { return endDate; }
                set { endDate = value; }
            }
        }

        private IsSpecific isSpecific = new IsSpecific();

        public TaskFloating(string taskName, DateSpecificity endDateSpecificity, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            isSpecific.endDate = endDateSpecificity;
        }

        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Floating"),
                            new XElement("Name", taskName),
                            new XElement("State", doneState.ToString())
                            );
            return task;
        }
    }
}