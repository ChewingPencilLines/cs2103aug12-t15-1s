using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TokenDate : Token
    {
        Specificity isSpecific;
        DateTime dateTime;
        internal Specificity IsSpecific
        {
            get { return isSpecific; }
            set { isSpecific = value; }
        }
        internal DateTime Value
        {
            get { return dateTime; }
        }

        internal TokenDate(int position, DateTime date, Specificity isSpecific)
            : base(position)
        {
            dateTime = date;
            this.isSpecific = isSpecific;
        }
    }
}
