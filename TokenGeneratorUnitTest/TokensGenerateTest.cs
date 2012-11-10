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
        public void SimpleAddTest()
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
        public void SimpleDaterangeAddTest()
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
        public void DateTimeParseTest1()
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
        public void DateTimeParseTest2()
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
        public void DateTimeParseTest3()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("jan");
            input.Add("13th");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenDate);
            Assert.IsTrue(result[2] is TokenDate);
            return;
        }

        [TestMethod]
        public void SimplePostponeTest()
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
        public void IndexRangeTest()
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
        public void IndexRangeFailTest()
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
        public void CombinedTest1()
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
        public void CombinedTest2()
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
        public void SortTypeTest()
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

        [TestMethod]
        public void TimeRangeTest()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaa");
            input.Add("aa");
            input.Add("tmr");
            input.Add("13:00");
            input.Add("-");
            input.Add("19:00");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(6, result.Count);
            Assert.IsTrue(result[0] is TokenCommand); 
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDay);
            Assert.IsTrue(result[3] is TokenTime);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenTime);
            return;
        }

        [TestMethod]
        public void DateRangeTest()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaa");
            input.Add("1/6");
            input.Add("-");
            input.Add("5/6");
            input.Add("2013");
            result = gene.GenerateAllTokens(input);
            Assert.AreEqual(6, result.Count);
            Assert.IsTrue(result[0] is TokenCommand); 
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDate);
            Assert.IsTrue(result[5] is TokenTime);
            return;
        }
    }
}
