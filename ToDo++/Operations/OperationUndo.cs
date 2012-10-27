using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class OperationUndo : Operation
    {
        public OperationUndo()
        { }
        public override string Execute(List<Task> taskList, Storage storageXML)
        {
            OperationHandler opHandler = new OperationHandler(storageXML);
            string response;

            response = opHandler.UndoLastOperation();
            return response;
        }
    }
}
