using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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

        /// <summary>
        /// This method uses the given list of tokens to generate a corresponding Operation.
        /// </summary>
        /// <param name="tokens">The list of tokens from which the generated operation will be based on.</param>
        /// <returns>The generated Operation.</returns>
        private static Operation GenerateOperation(List<Token> tokens)
        {            
            // reset factory configuration / just create new factory.
            OperationGenerator factory = new OperationGenerator();
            foreach (Token token in tokens)
            {
               token.ConfigureGenerator(factory);
            }
            // implement? ReleaseUnusedTokens();
            factory.FinalizeGenerator();
            Operation newOperation = factory.CreateOperation();
            return newOperation;
        }

    }
}
