// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.FieldWorks.Filters;

namespace AlloGenModelTests
{
    public class MatcherTests
    {
        int ws = 999000001;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FwMatcherXmlTest()
        {
            Matcher matcher = new Matcher(MatcherType.Begin);
            matcher.Pattern = "to";
            matcher.MatchCase = true;
            matcher.MatchDiacritics = false;
            string sXml = matcher.CreateFwXmlString(ws);
            Assert.AreEqual("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.BeginMatcher\" label=\"\" pattern=\"to\" ws=\"999000001\" matchCase=\"True\" matchDiacritics=\"False\"/>",
                sXml);

            matcher.Pattern = "away";
            matcher.MatchCase = false;
            matcher.MatchDiacritics = true;
            matcher.Type = MatcherType.Anywhere;
            sXml = matcher.CreateFwXmlString(ws);
            Assert.AreEqual("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.AnywhereMatcher\" label=\"\" pattern=\"away\" ws=\"999000001\" matchCase=\"False\" matchDiacritics=\"True\"/>",
                sXml);

            matcher.Type = MatcherType.End;
            sXml = matcher.CreateFwXmlString(ws);
            Assert.AreEqual("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.EndMatcher\" label=\"\" pattern=\"away\" ws=\"999000001\" matchCase=\"False\" matchDiacritics=\"True\"/>",
                sXml);

            matcher.Type = MatcherType.Exact;
            sXml = matcher.CreateFwXmlString(ws);
            Assert.AreEqual("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.ExactMatcher\" label=\"\" pattern=\"away\" ws=\"999000001\" matchCase=\"False\" matchDiacritics=\"True\"/>",
                sXml);

            matcher.Type = MatcherType.RegularExpression;
            sXml = matcher.CreateFwXmlString(ws);
            Assert.AreEqual("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.RegExpMatcher\" label=\"\" pattern=\"away\" ws=\"999000001\" matchCase=\"False\" matchDiacritics=\"True\"/>",
                sXml);
        }

        [Test]
        public void FwMatcherTest()
        {
            Matcher matcher = new Matcher(MatcherType.Begin);
            matcher.Pattern = "to";
            matcher.MatchCase = true;
            matcher.MatchDiacritics = false;
            // for some reason, the test file is not finding the Filters.dll
            //IMatcher fwMatcher = matcher.GetFwMatcher(ws);
            //Console.WriteLine(fwMatcher);
            //Assert.AreEqual("xyz", fwMatcher.ToString());
            //Assert.AreEqual(ws, fwMatcher.WritingSystem);
        }
    }
}
