using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    [TestClass]
    public class StringParserTest
    {
        [TestMethod]
        public void Simple_SearchForCommandTest_Add()
        {
            string [] testWords = {"rubbish", "morerubbish", "add", "date"};
            // index of the command keyword "add" is 2
            Assert.AreEqual(2, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void Simple_SearchForCommandTest_Modify()
        {
            string[] testWords = { "stuff", "modify", "date", "time" };
            // index of the command keyword "modify" is 1
            Assert.AreEqual(1, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void Null_SearchForCommandTest_Add()
        {
            string[] testWords = { "rubbish", "morerubbish", "dontadd", "stuff" };
            // index of the command keyword "add" is 2
            Assert.AreEqual(null, StringParser.SearchForCommandKeyword(testWords));
        }        

        [TestMethod]
        public void MultipleCommandSearchTest()
        {
            string[] testWords = { "deadline", "modify", "add", "stuff" };
            // index of the command keyword "modify" is 1
            Assert.AreEqual(-2, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void SplitCommandTest()
        {
            string[] testWords = { "aaa", "bbb", "add", "january" };
            string[] expectedResult = { "aaa", "bbb", "january" };
            // index of the command keyword "modify" is 1
            Assert.AreEqual(CommandType.ADD, StringParser.SplitCommandFromSentence(ref testWords,2));
            Assert.AreEqual(testWords,expectedResult);
        }
    }
}
