using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationPostpone : Operation
    {
        private int? oldIndex;
        private Task postponedTask;

        public OperationPostpone(int Previous, Task Postponed)
        {
            oldIndex = Previous - 1;
            postponedTask = Postponed;
        }

        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            throw new NotImplementedException();
        }
    }
}
