using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    [TestClass]
    public class StringParserTest
    {
        [TestMethod]
        // note: this test method does not test if the day-month-year format takes precedence
        public void IsValidNumericDate()
        {
            Assert.IsTrue(StringParser.IsValidNumericDate("20/1/2012")); // dmy
            Assert.IsTrue(StringParser.IsValidNumericDate("20-11-2016")); // dmy
            Assert.IsTrue(StringParser.IsValidNumericDate("20-11-16")); // dmy
            Assert.IsTrue(StringParser.IsValidNumericDate("2.1.2100")); // dmy or mdy
            Assert.IsTrue(StringParser.IsValidNumericDate("11/23/2012")); // mdy
            Assert.IsTrue(StringParser.IsValidNumericDate("11.23.2999")); // mdy
            Assert.IsTrue(StringParser.IsValidNumericDate("11/2012")); // my
            Assert.IsTrue(StringParser.IsValidNumericDate("15/12")); // dm or md (my??)
            Assert.IsTrue(StringParser.IsValidNumericDate("23/12")); // dm
            Assert.IsTrue(StringParser.IsValidNumericDate("12/23")); // md
            Assert.IsTrue(StringParser.IsValidNumericDate("1/1/0000")); // dmy or mdy
            Assert.IsFalse(StringParser.IsValidNumericDate("12/")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("23")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("11")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("23/23/2012")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("31/2022")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("4/55/2022")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("4/12/20222")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("2222")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("/1/2012")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("1/1/123")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("31/11-2222")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("11-2.2012")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("23.1-2112")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("2-1/2012")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("23.1.212")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("2.0.0000")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("0.2.0000")); // invalid
            Assert.IsFalse(StringParser.IsValidNumericDate("jan 2012")); // invalid
        }

        [TestMethod]
        // note: this test method does not test if the day-month-year format takes precedence
        public void IsValidAlphabeticDate()
        {
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("20 january 2012")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("20 jan 2012")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("20 jan 20")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("sept 2012")); // my
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("12 september")); // dm
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("12th apr")); // dm
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("23 feb 1022")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("23rd oct 1022")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("1st nov 1022")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("feb 23 1112")); // mdy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("jul 22nd 1112")); // mdy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("aug 30th")); // md
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("30th dec 2013")); // dmy
            Assert.IsTrue(StringParser.IsValidAlphabeticDate("february 22 12")); // mdy
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("2w oct 1022")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("122 oct 1022")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("23th oct 1022")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("0 jul 12")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("12nd dec 2012")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("23rd may 012")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("11st july 12")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("2013")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("12")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("8 novem 12")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("23 mar-23")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("feuary 0 12")); // invalid
            Assert.IsFalse(StringParser.IsValidAlphabeticDate("feuary 22 12")); // invalid
        }

        [TestMethod]
        public void MergeDateWords()
        {
            List<string> input1 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st", "Dec", "2012", "to", "30th", "Jan", "2013" };
            List<string> output1 = new List<string>();
            List<string> expectedOutput1 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st Dec 2012", "to", "30th Jan 2013" };
            output1 = StringParser.MergeDateWords(input1);
            CollectionAssert.AreEqual(expectedOutput1, output1);
            List<string> input2 = new List<string>() { "add", "30th", "Dec", "2013", "jenna's", "birthday" };
            List<string> output2 = new List<string>();
            List<string> expectedOutput2 = new List<string>() { "add", "30th Dec 2013", "jenna's", "birthday" };
            output2 = StringParser.MergeDateWords(input2);
            CollectionAssert.AreEqual(expectedOutput2, output2);
            List<string> input3 = new List<string>() { "add", "use", "kuishinbo", "3rd", "anniversary", "buffet", "voucher", "by", "9", "jan", "2013" };
            List<string> output3 = new List<string>();
            List<string> expectedOutput3 = new List<string>() { "add", "use", "kuishinbo", "3rd", "anniversary", "buffet", "voucher", "by", "9 jan 2013" };
            output3 = StringParser.MergeDateWords(input3);
            CollectionAssert.AreEqual(expectedOutput3, output3);
            List<string> input4 = new List<string>() { "add", "complete", "project", "by", "january", "2013" };
            List<string> output4 = new List<string>();
            List<string> expectedOutput4 = new List<string>() { "add", "complete", "project", "by", "january 2013" };
            output4 = StringParser.MergeDateWords(input4);
            CollectionAssert.AreEqual(expectedOutput4, output4);
            List<string> input5 = new List<string>() { "add", "extended", "family", "dinner", "12/10/12" };
            List<string> output5 = new List<string>();
            List<string> expectedOutput5 = new List<string>() { "add", "extended", "family", "dinner", "12/10/12" };
            output5 = StringParser.MergeDateWords(input5);
            CollectionAssert.AreEqual(expectedOutput5, output5);
        }

        [TestMethod]
        public void MergeWord_IfValidAlphabeticDate()
        {
            List<string> input1 = new List<string>() { "add", "buy", "new", "dog", "by", "22nd", "dec", "12" };
            List<string> output1 = new List<string>() { "add", "buy", "new", "dog", "by", "22nd" };
            List<string> expectedOutput1 = new List<string>() { "add", "buy", "new", "dog", "by", "22nd dec 12" };
            int position1 = 6, skipWords = 0; // "dec" is the 6th
            Assert.IsTrue(StringParser.MergeWord_IfValidAlphabeticDate(ref output1, input1, position1, ref skipWords));
            Assert.AreEqual(1, skipWords);
            Assert.AreEqual(expectedOutput1[5], output1[5]);
            CollectionAssert.AreEqual(expectedOutput1, output1);
            List<string> input2 = new List<string>() { "add", "complete", "project", "by", "january", "2013", "urgent" };
            List<string> output2 = new List<string>() { "add", "complete", "project", "by" };
            List<string> expectedOutput2 = new List<string>() { "add", "complete", "project", "by", "january 2013" };
            int position2 = 4; // "january" is the 4th
            Assert.IsTrue(StringParser.MergeWord_IfValidAlphabeticDate(ref output2, input2, position2, ref skipWords));
            Assert.AreEqual(1, skipWords);
            Assert.AreEqual(expectedOutput2[4], output2[4]);
            CollectionAssert.AreEqual(expectedOutput2, output2);
            List<string> input3 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st", "Dec", "2012", "to", "30th", "Dec", "2013" };
            List<string> output3 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st", "Dec", "2012", "to", "30th" };
            List<string> expectedOutput3 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st", "Dec", "2012", "to", "30th Dec 2013" };
            int position3 = 11; // "dec" is the 11th
            Assert.IsTrue(StringParser.MergeWord_IfValidAlphabeticDate(ref output3, input3, position3, ref skipWords));
            Assert.AreEqual(1, skipWords);
            CollectionAssert.AreEqual(expectedOutput3, output3);
        }

        [TestMethod]
        public void GenerateDateTokens()
        {
            List<string> input1 = new List<string>() { "add", "submit", "lucky", "draw", "entry", "from", "21st Dec 2012", "to", "30th Jan 2013" };
            List<TokenDate> output1 = new List<TokenDate>();
            TokenDate expectedDateToken1a = new TokenDate(6, DateTime.Parse("2012-12-21"), true);
            TokenDate expectedDateToken1b = new TokenDate(8, DateTime.Parse("2013-1-30"), true);
            List<TokenDate> expectedOutput1 = new List<TokenDate>() { expectedDateToken1a, expectedDateToken1b };
            output1 = StringParser.GenerateDateTokens(input1);
            Assert.AreEqual(expectedOutput1.Count, output1.Count);
            //CollectionAssert.AreEqual(expectedOutput1, output1);
            List<string> input2 = new List<string>() { "add", "30th Dec 2013", "jenna's", "birthday" };
            List<TokenDate> output2 = new List<TokenDate>();
            TokenDate expectedDateToken2a = new TokenDate(1, DateTime.Parse("2013-12-30"), true);
            List<TokenDate> expectedOutput2 = new List<TokenDate>() { expectedDateToken2a };
            output2 = StringParser.GenerateDateTokens(input2);
            Assert.AreEqual(expectedOutput2.Count, output2.Count);
            //CollectionAssert.AreEqual(expectedOutput2, output2);
            List<string> input3 = new List<string>() { "add", "use", "kuishinbo", "3rd", "anniversary", "buffet", "voucher", "by", "9 jan 2013" }; // erroneous detection of "3rd" as a date input
            List<TokenDate> output3 = new List<TokenDate>();
            TokenDate expectedDateToken3a = new TokenDate(8, DateTime.Parse("2012-11-3"), true);
            TokenDate expectedDateToken3b = new TokenDate(8, DateTime.Parse("2013-1-9"), true);
            List<TokenDate> expectedOutput3 = new List<TokenDate>() { expectedDateToken3a, expectedDateToken3b };
            output3 = StringParser.GenerateDateTokens(input3);
            Assert.AreEqual(expectedOutput3.Count, output3.Count);
            //CollectionAssert.AreEqual(expectedOutput3, output3);
            List<string> input4 = new List<string>() { "add", "complete", "project", "by", "january 2013" };
            List<TokenDate> output4 = new List<TokenDate>();
            TokenDate expectedDateToken4a = new TokenDate(4, DateTime.Parse("2013-1-1"), false);
            List<TokenDate> expectedOutput4 = new List<TokenDate>() { expectedDateToken4a };
            output4 = StringParser.GenerateDateTokens(input4);
            Assert.AreEqual(expectedOutput4.Count, output4.Count);
            //CollectionAssert.AreEqual(expectedOutput4, output4);
            List<string> input5 = new List<string>() { "add", "chalet", "from", "27th feb", "to", "30th feb" }; // invalid date 30 feb
            List<TokenDate> output5 = new List<TokenDate>();
            TokenDate expectedDateToken5a = new TokenDate(3, DateTime.Parse("2012-2-27"), false);
            List<TokenDate> expectedOutput5 = new List<TokenDate>() { expectedDateToken4a };
            output5 = StringParser.GenerateDateTokens(input5);
            Assert.AreEqual(expectedOutput5.Count, output5.Count);
            //CollectionAssert.AreEqual(expectedOutput5, output5);
            List<string> input6 = new List<string>() { "add", "moon", "cake", "festival", "15th" }; // note that this test method will fail after 3 days
            List<TokenDate> output6 = new List<TokenDate>();
            TokenDate expectedDateToken6a = new TokenDate(4, DateTime.Parse("2012-10-15"), false);
            List<TokenDate> expectedOutput6 = new List<TokenDate>() { expectedDateToken6a };
            output6 = StringParser.GenerateDateTokens(input6);
            Assert.AreEqual(expectedOutput6.Count, output6.Count);
            //CollectionAssert.AreEqual(expectedOutput6, output6);
            List<string> input7 = new List<string>() { "add", "i", "am", "going", "to", "make", "pc", "fall", "on", "his", "ass", "1st" };
            List<TokenDate> output7 = new List<TokenDate>();
            TokenDate expectedDateToken7a = new TokenDate(11, DateTime.Parse("2012-11-11"), false);
            List<TokenDate> expectedOutput7 = new List<TokenDate>() { expectedDateToken7a };
            output7 = StringParser.GenerateDateTokens(input7);
            Assert.AreEqual(expectedOutput7.Count, output7.Count);
            //CollectionAssert.AreEqual(expectedOutput7, output7);
        }

        [TestMethod]
        public void RemoveSuffixesIfRequired()
        {
            string day = StringParser.RemoveSuffixesIfRequired("23rd");
            Assert.AreEqual("23", day);
            day = StringParser.RemoveSuffixesIfRequired("11th");
            Assert.AreEqual("11", day);
            day = StringParser.RemoveSuffixesIfRequired("1st");
            Assert.AreEqual("1", day);
        }
    }
}