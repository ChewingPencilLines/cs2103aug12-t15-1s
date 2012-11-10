using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace TokenGeneratorTest
{
    [TestClass]
    public class TokensGenerateTest
    {
        TokenGenerator gene = new TokenGenerator();
        List<string> input;
        List<Token> result;

        [TestMethod]
        public void GenerateTokensTest1()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("morning");
            input.Add("8AM");
            input.Add("TASK");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenTime);
            Assert.IsTrue(result[3] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest2()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("morning"); 
            input.Add("tmr");
            input.Add("8AM");
            input.Add("TASK");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDay);
            Assert.IsTrue(result[3] is TokenTime);
            Assert.IsTrue(result[4] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest3()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("3.5.2013"); 
            result = gene.GenerateTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate); 
            return;
        }

        [TestMethod]
        public void GenerateTokensTest4()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("03/05/2013");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate);       
            return;
        }

        [TestMethod]
        public void GenerateTokensTest5()
        {
            input = new List<string>();
            input.Add("postpone");
            input.Add("tmr");
            input.Add("to");
            input.Add("wed");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDay);
            Assert.IsTrue(result[2] is TokenContext);
            Assert.IsTrue(result[3] is TokenDay);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest6()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("jan 13th");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest7()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("1-5");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenIndexRange);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest8()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("-1");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateTokensTest9()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaaaa");
            input.Add("1/1/2013");
            input.Add("morning");
            input.Add("to");
            input.Add("3/2/2013");
            input.Add("evening");
            result = gene.GenerateTokens(input);
            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenLiteral);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenDate);
            Assert.IsTrue(result[6] is TokenLiteral);
            return;
        }
    }
}
