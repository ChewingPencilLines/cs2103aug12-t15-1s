using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    [TestClass]
    public class StringParserUnitTest
    {
        StringParser testStrParser = new StringParser();
        string input;
        List<Token> result;

        [TestMethod]
        public void AddSimpleTimedTaskTest()
        {
            input = "add morning 8AM TASK";
            result = testStrParser.ParseStringIntoTokens(input);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenTimeRange);
            Assert.IsTrue(result[2] is TokenTime);
            Assert.IsTrue(result[3] is TokenLiteral);
            return;
        }
    }
}

