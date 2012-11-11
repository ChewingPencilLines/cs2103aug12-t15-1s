using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public abstract class Token
    {
        // Position of the Token within a string.
        private int position;
        internal Token(int position)
        {
            this.position = position;
        }  
        internal int Position
        {
            get { return position; }           
        }

        /// <summary>
        /// The base method which should be overriden by derived classes.
        /// It allows the token to configure an OperationGenerator to create
        /// an appropriate Operation.
        /// </summary>
        /// <param name="attrb">The OperationGenerator to configure.</param>
        internal abstract void ConfigureGenerator(OperationGenerator attrb);
    }
}