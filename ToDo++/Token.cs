using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Token
    {        
        private int position;
        internal Token(int position)
        {
            this.position = position;
        }  
        internal int Position
        {
            get { return position; }           
        }
    }

    public class TokenCommand : Token
    {
        public const int START_INDEX = 0;
        public const int END_INDEX = 1;
        public const int RANGE = 2;
        CommandType commandType;
        int[] taskIndex;
        
        internal CommandType Value
        {
            get { return commandType; }
        }
        internal int[] TaskIndex
        {
            get { return taskIndex; }
        }

        internal TokenCommand(int position, CommandType val, int[] taskIndex = null)
            : base(position)
        {
            commandType = val;
            this.taskIndex = taskIndex;
        }
    }

    public class TokenDate : Token
    {
        private DateSpecificity dateSpecificity = new DateSpecificity();
        DateTime dateTime;
        Boolean specific;
        internal DateTime Value
        {
            get { return dateTime; }
        }
        internal Boolean IsSpecific
        {
            get { return specific; }
        }
        internal TokenDate(int position, DateTime date, DateSpecificity specificity)
            : base(position)
        {
            dateTime = date;
            dateSpecificity = specificity;
        }
    }

    public class TokenTime : Token
    {
        TimeSpan time;
        Boolean specific;
        internal TimeSpan Value
        {
            get { return time; }
        }
        internal Boolean IsSpecific
        {
            get { return specific; }
        }
        internal TokenTime(int position, TimeSpan val, Boolean specific_flag)
            : base(position)
        {
            time = val;
            specific = specific_flag;
        }
    }

    public class TokenDay : Token
    {
        DayOfWeek dayOfWeek;
        internal DayOfWeek Value
        {
            get { return dayOfWeek; }
        }
        internal TokenDay(int position, DayOfWeek val)
            : base(position)
        {
            dayOfWeek = val;
        }
    }

    public class TokenContext : Token
    {
        ContextType contextType;
        internal ContextType Value
        {
            get { return contextType; }
        }
        internal TokenContext(int position, ContextType val)
            : base(position)
        {
            contextType = val;
        }
    }

    public class TokenIndex : Token
    {
        string index;
        internal string Value
        {
            get { return index; }
        }
        internal TokenIndex(int position, string val)
            : base(position)
        {
            index = val;
        }
    }

    public class TokenLiteral : Token
    {
        string literal;
        internal string Value
        {
            get { return literal; }
        }
        internal TokenLiteral(int position, string val)
            : base(position)
        {
            literal = val;
        }
    }
}
