using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class Token
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

    class TokenCommand : Token
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

    class TokenDate : Token
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

    class TokenTime : Token
    {
        TimeSpan time;
        internal TimeSpan Value
        {
            get { return time; }
        }
        internal TokenTime(int position, TimeSpan val)
            : base(position)
        {
            type = TokenType.TIME;
            time = val;
        }
    }

    class TokenDay : Token
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

    class TokenContext : Token
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

    class TokenIndex : Token
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

    class TokenLiteral : Token
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
