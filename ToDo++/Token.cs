using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Token
    {
        public enum TokenType { COMMAND, DATE, TIME, DAY, CONTEXT, LITERAL, INDEX };

        private int position;
        protected TokenType type;

        internal Token(int position)
        {
            this.position = position;
        }

        internal TokenType Type
        {            
            get { return type; }
        } 
           
        internal int Position
        {
            get { return position; }           
        }

    }

    public class TokenCommand : Token
    {
        CommandType commandType;
        int? taskIndex;

        internal CommandType Value
        {
            get { return commandType; }
        }
        internal int? TaskIndex
        {
            get { return taskIndex; }
        }

        internal TokenCommand(int position, CommandType val, int? taskIndex = null)
            : base(position)
        {
            type = TokenType.COMMAND;
            commandType = val;
            this.taskIndex = taskIndex;
        }
    }

    public class TokenDate : Token
    {
        DateTime dateTime;
        bool specific;
        internal DateTime Value
        {
            get { return dateTime; }
        }
        internal bool IsSpecific
        {
            get { return specific; }
        }
        internal TokenDate(int position, DateTime date, Boolean specific_flag)
            : base(position)
        {
            dateTime = date;
            specific = specific_flag;
        }
    }

    public class TokenTime : Token
    {
        TimeSpan time;
        bool specific;
        internal TimeSpan Value
        {
            get { return time; }
        }
        internal bool IsSpecific
        {
            get { return specific; }
        }
        internal TokenTime(int position, TimeSpan val, Boolean specific_flag)
            : base(position)
        {
            type = TokenType.TIME;
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
            type = TokenType.DAY;
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
            type = TokenType.CONTEXT;
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
            type = TokenType.LITERAL;
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
            type = TokenType.LITERAL;
            literal = val;
        }
    }
}
