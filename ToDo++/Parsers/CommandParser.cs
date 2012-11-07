using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    class CommandParser
    {
        const int START_INDEX = 0;
        const int END_INDEX = 1;
        StringParser stringParser;

        public CommandParser(StringParser stringParser)
        {
            this.stringParser = stringParser;
        }

        public Operation ParseOperation(string input)
        {
            // Get position of delimiters so we can treat those substrings as a single word.
            List<Token> tokens = stringParser.ParseStringIntoTokens(input);
            return GenerateOperation(tokens);            
        }

        private static Operation GenerateOperation(List<Token> tokens)
        {            
            OperationGenerator opAttributes = new OperationGenerator();
            foreach (Token token in tokens)
            {
               token.UpdateAttributes(opAttributes);
            }
            // implement?: 
            // ReleaseUnusedTokens();
            opAttributes.SetTimes();
            Operation newOperation = opAttributes.CreateOperation();
            return newOperation;
        }

    }
}
