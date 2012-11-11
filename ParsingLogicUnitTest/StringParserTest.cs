using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace TokenGeneratorTest
{
    [TestClass]
    public class TokenGeneratorTest
    {
        StringParser testStrParser = new StringParser();
        TokenGenerator tokenGen = new TokenGenerator();
        string input;
        List<string> output;
        List<Token> result;

        [TestMethod]
        public void AddSimpleTimedTaskTest()
        {
            input = "add morning 8AM TASK";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("add");
            outputCheck.Add("morning");
            outputCheck.Add("8AM");
            outputCheck.Add("TASK");
            Assert.IsTrue(output.Count()==outputCheck.Count);
            for (int i = 0; i < output.Count(); i++ )
            {
                Assert.AreEqual(output[i], outputCheck[i]);
            }
            return;
        }

        [TestMethod]
        public void ParseSortCommandTest()
        {
            input = "sort    name";
            output = testStrParser.ParseStringIntoWords(input); 
            List<string> outputCheck = new List<string>();
            outputCheck.Add("sort");
            outputCheck.Add("name");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(output[i], outputCheck[i]);
            }
            return;
        }

        [TestMethod]
        public void AddComplicatedDatedTaskTest1()
        {
            input = "buy milk tmr 3 am to 12/12/12 5 pm add";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("buy");
            outputCheck.Add("milk");
            outputCheck.Add("tmr");
            outputCheck.Add("3 am");
            outputCheck.Add("to");
            outputCheck.Add("12/12/12");
            outputCheck.Add("5 pm");
            outputCheck.Add("add");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(output[i], outputCheck[i]);
            }
            return;
        }

        [TestMethod]
        public void AddFringeCase()
        {
            input = "add task today to sunday";
            output = testStrParser.ParseStringIntoWords(input);
            result = tokenGen.GenerateAllTokens(output);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDay);
            return;
        }

        [TestMethod]
        public void AddComplicatedDatedTaskTest2()
        {
            input = "add buy - milk jan 23rd 2016 to feb 29th 2016";
            output = testStrParser.ParseStringIntoWords(input);
            result = tokenGen.GenerateAllTokens(output);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDate);
            return;
        }
    }
}

