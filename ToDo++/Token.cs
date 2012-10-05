using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    class Token
    {
        enum TokenType { COMMAND, DATETIME, DAY, CONTEXT, DATA };
        private int position;
        internal int Position
        {
            get { return position; }           
        }

        internal Token(int position)
        {
            this.position = position;
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
            keyword = raw;
        }
    }
}
