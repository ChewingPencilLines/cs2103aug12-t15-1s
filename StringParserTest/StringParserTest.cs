using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    [TestClass]
    public class StringParserTest
    {
        [TestMethod]
        public void SimpleSearchForAddCommandTest()
        {
            string [] testWords = {"rubbish", "morerubbish", "add", "stuff"};
            // index of the command keyword "add" is 2
            Assert.AreEqual(2, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void NullSearchForAddCommandTest()
        {
            string[] testWords = { "rubbish", "morerubbish", "dontadd", "stuff" };
            // index of the command keyword "add" is 2
            Assert.AreEqual(null, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void SimpleSearchForModifyCommandTest()
        {
            string[] testWords = { "rubbish", "modify", "nothing", "stuff" };
            // index of the command keyword "modify" is 1
            Assert.AreEqual(1, StringParser.SearchForCommandKeyword(testWords));
        }

        [TestMethod]
        public void MultipleCommandKeywordTest()
        {
            string[] testWords = { "rubbish", "modify", "add", "stuff" };
            // index of the command keyword "modify" is 1
            Assert.AreEqual(-2, StringParser.SearchForCommandKeyword(testWords));
        }
    }
}
