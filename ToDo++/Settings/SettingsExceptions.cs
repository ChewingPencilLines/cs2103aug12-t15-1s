using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TextSizeOutOfRange : Exception { public TextSizeOutOfRange(string message) : base(message) { } }
    public class RepeatCommandException : Exception { public RepeatCommandException(string message) : base(message) { } }
    public class NothingSelectedException : Exception { public NothingSelectedException(string message) : base(message) { } }
}
