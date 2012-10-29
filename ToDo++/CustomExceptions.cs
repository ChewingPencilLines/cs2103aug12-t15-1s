using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TextSizeOutOfRangeException : Exception { public TextSizeOutOfRangeException(string message) : base(message) { } }
    public class RepeatCommandException : Exception { public RepeatCommandException(string message) : base(message) { } }
    public class NothingSelectedException : Exception { public NothingSelectedException(string message) : base(message) { } }
    public class InvalidDateTimeException : Exception { public InvalidDateTimeException(string message) : base(message) { } }
    public class InvalidDeleteFlexiException : Exception { public InvalidDeleteFlexiException(string message) : base(message) { } }
}
