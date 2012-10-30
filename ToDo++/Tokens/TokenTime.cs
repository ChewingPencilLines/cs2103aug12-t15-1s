using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
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
}
