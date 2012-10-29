using System;
using System.Collections.Generic;
using System.Text;

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

        public override string Undo(List<Task> taskList, Storage storageXML)
        {
            throw new NotImplementedException();
        }
    }
}
