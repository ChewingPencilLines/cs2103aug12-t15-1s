using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace CommandParserTest
{
    [TestClass]
    public class CommandParserTest
    {
        CommandParser testParser = new CommandParser();

        [TestMethod]
        public void OperationParseTest()
        {
            testParser.ParseOperation("task do stuff add by 9 pm");
            return;
        }

        /*
        [TestMethod]
        public void IndexSortTest()
        {
            List<int[]> testIndexes = new List<int[]>();
            testIndexes.Add(new int[2] { 6, 10 });
            testIndexes.Add(new int[2] { 0, 4 });
            testIndexes.Add(new int[2] { 3, 7 });
            testIndexes.Add(new int[2] { 1, 2 });
            List<int[]> expectedResult = new List<int[]>{
                new int[2] { 0, 4 },
                new int[2] { 1, 2 },
                new int[2] { 3, 7 },
                new int[2] { 6, 10 }
            };            
            testParser.SortIndexes(ref testIndexes);
            CollectionAssert.AreEqual(
                testIndexes.SelectMany(x => x).ToList(),
                expectedResult.SelectMany(x => x).ToList()
                );
        }

        [TestMethod]
        public void IndexRemoveTest()
        {
            List<int[]> testIndexes = new List<int[]>();
            testIndexes.Add(new int[2] { 0, 4 });
            testIndexes.Add(new int[2] { 1, 2 });
            testIndexes.Add(new int[2] { 3, 7 });
            testIndexes.Add(new int[2] { 6, 10 });
            List<int[]> expectedResult = new List<int[]>{
                new int[2] { 0, 4 },
                new int[2] { 3, 7 }
            };
            testParser.RemoveBadIndexes(ref testIndexes);
            CollectionAssert.AreEqual(
                testIndexes.SelectMany(x => x).ToList(),
                expectedResult.SelectMany(x => x).ToList()
                );
        }
        */
    }
}
