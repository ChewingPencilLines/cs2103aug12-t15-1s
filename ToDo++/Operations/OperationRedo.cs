using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationRedo : Operation
    {
        public OperationRedo()
        { }
        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            throw new NotImplementedException();
        }
    }
}
