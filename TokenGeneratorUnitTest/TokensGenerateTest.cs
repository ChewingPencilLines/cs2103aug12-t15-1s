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
        public void GenerateAllTokensTest1()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("morning");
            input.Add("8AM");
            input.Add("TASK");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenTimeRange);
            Assert.IsTrue(result[2] is TokenTime);
            Assert.IsTrue(result[3] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest2()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("morning"); 
            input.Add("tmr");
            input.Add("8AM");
            input.Add("TASK");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenTimeRange);
            Assert.IsTrue(result[2] is TokenDay);
            Assert.IsTrue(result[3] is TokenTime);
            Assert.IsTrue(result[4] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest3()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("3.5.2013"); 
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate); 
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest4()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("03/05/2013");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate);       
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest5()
        {
            input = new List<string>();
            input.Add("postpone");
            input.Add("tmr");
            input.Add("to");
            input.Add("wed");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDay);
            Assert.IsTrue(result[2] is TokenContext);
            Assert.IsTrue(result[3] is TokenDay);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest6()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("jan 13th");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest7()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("1-5");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenIndexRange);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest8()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("-1");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest9()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaaaa");
            input.Add("1/1/2013");
            input.Add("morning");
            input.Add("to");
            input.Add("3/2/2013");
            input.Add("evening");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenTimeRange);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenDate);
            Assert.IsTrue(result[6] is TokenTimeRange);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest10()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaaaa");
            input.Add("feb");
            input.Add("13th");
            input.Add("to");
            input.Add("jun");
            input.Add("22nd");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenDate);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenDate);
            Assert.IsTrue(result[6] is TokenDate);
            return;
        }

        [TestMethod]
        public void GenerateAllTokensTest11()
        {
            input = new List<string>();
            input.Add("sort");
            input.Add("date");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenSortType);
            return;
        }
    }
}
