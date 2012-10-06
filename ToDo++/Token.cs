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

        internal TokenCommand(int position, CommandType raw)
            : base(position)
        {
            type = TokenType.COMMAND;
            commandType = raw;
        }
    }

    class TokenDate : Token
    {
        DateTime dateTime;
        internal DateTime Value
        {
            get { return dateTime; }
        }
        internal TokenDate(int position, DateTime raw)
            : base(position)
        {
            type = TokenType.DATE;
            dateTime = raw;
        }
    }

    class TokenTime : Token
    {
        TimeSpan time;
        internal TimeSpan Value
        {
            get { return time; }
        }
        internal TokenTime(int position, TimeSpan raw)
            : base(position)
        {
            type = TokenType.TIME;
            time = raw;
        }
    }

    class TokenDay : Token
    {
        DayOfWeek dayOfWeek;
        internal DayOfWeek Value
        {
            get { return dayOfWeek; }
        }
        internal TokenDay(int position, DayOfWeek raw)
            : base(position)
        {
            type = TokenType.DAY;
            dayOfWeek = raw;
        }
    }

    class TokenContext : Token
    {
        string keyword;
        internal string Value
        {
            get { return keyword; }
        }
        internal TokenContext(int position, string raw)
            : base(position)
        {
            type = TokenType.CONTEXT;
            keyword = raw;
        }
    }

    class TokenLiteral : Token
    {
        string literal;
        internal string Value
        {
            get { return literal; }
        }
        internal TokenLiteral(int position, string raw)
            : base(position)
        {
            type = TokenType.LITERAL;
            literal = raw;
        }
    }
}
