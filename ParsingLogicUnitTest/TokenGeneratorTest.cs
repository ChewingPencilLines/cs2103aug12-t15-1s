﻿using System;
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
        List<string> words;
        List<Token> result;

        [TestMethod]
        public void AddSimpleTimedTaskTest()
        {
            input = "add morning 8AM TASK";
            words = testStrParser.ParseStringIntoTokens(input);
            result = tokenGen.GenerateAllTokens(words);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenTimeRange);
            Assert.IsTrue(result[2] is TokenTime);
            Assert.IsTrue(result[3] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void ParseSortCommandTest()
        {
            input = "sort    name";
            words = testStrParser.ParseStringIntoTokens(input);
            result = tokenGen.GenerateAllTokens(words);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenSortType);
            return;
        }

        [TestMethod]
        public void AddComplicatedDatedTaskTest1()
        {
            input = "buy milk tmr 3 am to 12/12/12 5 pm add";
            words = testStrParser.ParseStringIntoTokens(input);
            result = tokenGen.GenerateAllTokens(words);
            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result[0] is TokenLiteral);
            Assert.IsTrue(result[1] is TokenDate);
            Assert.IsTrue(result[2] is TokenTime); 
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDate);
            Assert.IsTrue(result[5] is TokenTime);
            Assert.IsTrue(result[6] is TokenCommand);
            return;
        }

        [TestMethod]
        public void AddFringeCase()
        {
            input = "add task today to sunday";
            words = testStrParser.ParseStringIntoTokens(input);
            result = tokenGen.GenerateAllTokens(words);
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
            words = testStrParser.ParseStringIntoTokens(input);
            result = tokenGen.GenerateAllTokens(words);
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
