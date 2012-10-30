using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationPostpone : Operation
    {
        private int? index;
        private int? endindex;
        private string taskName;
        private DateTime? oldTime = null, postponeTime = null;

        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime, DateTime? postponeTime)
        {
            if (indexRange == null) this.index = null;
            else
            {
                this.index = indexRange[TokenCommand.START_INDEX] - 1;
                this.endindex = indexRange[TokenCommand.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
            this.oldTime = startTime;
            this.postponeTime = postponeTime;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            throw new NotImplementedException();
        }
    }
}
