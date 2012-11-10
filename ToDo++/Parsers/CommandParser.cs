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
        StringParser stringParser;

        public CommandParser(StringParser stringParser)
        {
            this.stringParser = stringParser;
        }

        public Operation ParseOperation(string input)
        {
            List<Token> tokens = stringParser.ParseStringIntoTokens(input);
            return GenerateOperation(tokens);       
        }

        private static Operation GenerateOperation(List<Token> tokens)
        {            
            OperationGenerator factory = new OperationGenerator();
            foreach (Token token in tokens)
            {
               token.ConfigureGenerator(factory);
            }
            // implement? ReleaseUnusedTokens();
            factory.SetTimes();
            Operation newOperation = factory.CreateOperation();
            return newOperation;
        }

    }
}
