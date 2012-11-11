﻿using System;
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
        TokenGenerator tokenFactory;
        OperationGenerator operationFactory;

        public CommandParser()
        {
            this.stringParser = new StringParser();
            this.tokenFactory = new TokenGenerator();
            this.operationFactory = new OperationGenerator();
        }

        public Operation ParseOperation(string input)
        {
            List<string> words = stringParser.ParseStringIntoTokens(input);
            List<Token> tokens = tokenFactory.GenerateAllTokens(words);
            return GenerateOperation(tokens);       
        }

        /// <summary>
        /// This method uses the given list of tokens to generate a corresponding Operation.
        /// </summary>
        /// <param name="tokens">The list of tokens from which the generated operation will be based on.</param>
        /// <returns>The generated Operation.</returns>
        private Operation GenerateOperation(List<Token> tokens)
        {            
            // reset factory configuration
            operationFactory.InitializeNewConfiguration();
            foreach (Token token in tokens)
            {
                token.ConfigureGenerator(operationFactory);
            }
            // implement? ReleaseUnusedTokens();
            operationFactory.FinalizeGenerator();
            Operation newOperation = operationFactory.CreateOperation();
            return newOperation;
        }

    }
}
