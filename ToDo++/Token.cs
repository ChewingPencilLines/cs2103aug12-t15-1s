using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class Token
    {
        public enum TokenType { COMMAND, DATE, TIME, DAY, CONTEXT, LITERAL };

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
        internal CommandType Value
        {
            get { return commandType; }
        }

        internal TokenCommand(int position, CommandType val)
            : base(position)
        {
            type = TokenType.COMMAND;
            commandType = val;
        }
    }

    class TokenDate : Token
    {
        DateTime dateTime;
        internal DateTime Value
        {
            get { return dateTime; }
        }
        internal TokenDate(int position, DateTime val)
            : base(position)
        {
            type = TokenType.DATE;
            dateTime = val;
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
