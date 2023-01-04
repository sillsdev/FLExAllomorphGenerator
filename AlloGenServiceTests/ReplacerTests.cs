// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenServiceTest
{
    public class ReplacerTests
    {
        XmlBackEndProvider provider = new XmlBackEndProvider();
        string TestDataDir { get; set; }
        string AlloGenFile { get; set; }
        string AlloGenExpected { get; set; }
        Replacer replacer { get; set; }

        [SetUp]
        public void Setup()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf("AlloGenServiceTests");
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, "AlloGenServiceTests", "TestData");
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenReplace.agf");
        }

        [Test]
        public void ReplaceTest()
        {
            provider.LoadDataFromFile(AlloGenExpected); 
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            Assert.NotNull(allomorphGenerators);
            Operation operation = allomorphGenerators.Operations[0];
            replacer = new Replacer(operation.Action.ReplaceOps);
            string input = "";
            input = "*a:";
            compareResult(input, "a", Dialect.Ach);
            compareResult(input, "a", Dialect.Acl);
            compareResult(input, "a", Dialect.Akh);
            compareResult(input, "a", Dialect.Akl);
            compareResult(input, "aa", Dialect.Ame);
            input = "*arka:";
            compareResult(input, "arka", Dialect.Ach);
            compareResult(input, "arka", Dialect.Acl);
            compareResult(input, "arka", Dialect.Akh);
            compareResult(input, "arka", Dialect.Akl);
            compareResult(input, "arkaa", Dialect.Ame);
            input = "*chillinya:";
            compareResult(input, "chillinya", Dialect.Ach);
            compareResult(input, "chillinya", Dialect.Acl);
            compareResult(input, "chillinya", Dialect.Akh);
            compareResult(input, "chillinya", Dialect.Akl);
            compareResult(input, "chillinyaa", Dialect.Ame);
            input = "*yarqa:.v2";
            compareResult(input, "yarqa", Dialect.Ach);
            compareResult(input, "yarqa", Dialect.Acl);
            compareResult(input, "yarqa", Dialect.Akh);
            compareResult(input, "yarqa", Dialect.Akl);
            compareResult(input, "yarqaa", Dialect.Ame);
            input = "+yusulpa:";
            compareResult(input, "yusulpa", Dialect.Ach);
            compareResult(input, "yusulpa", Dialect.Acl);
            compareResult(input, "yusulpa", Dialect.Akh);
            compareResult(input, "yusulpa", Dialect.Akl);
            compareResult(input, "yusulpaa", Dialect.Ame);
        }

        private void compareResult(string input, string expected, Dialect dialect)
        {
            string result = replacer.ApplyReplaceOpToOneDialect(input, dialect);
            Assert.AreEqual(expected, result);

        }
    }
}