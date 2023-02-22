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

namespace AlloGenModelTests
{
    class ReplaceOpsTest
    {
        XmlBackEndProvider provider = new XmlBackEndProvider();
        string TestDataDir { get; set; }
        string AlloGenFile { get; set; }
        string AlloGenExpected { get; set; }

        [SetUp]
        public void Setup()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf("AlloGenModelTests");
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, "AlloGenModelTests", "TestData");
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenExpected.agf");
        }

        [Test]
        public void ReplaceOpsDictTest()
        {
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            Assert.NotNull(allomorphGenerators);
            List<Replace> masterReplaceOps = allomorphGenerators.ReplaceOperations;
            Assert.NotNull(masterReplaceOps);
            Assert.AreEqual(4, masterReplaceOps.Count);
            Replace replace = allomorphGenerators.FindReplaceOp("5e8b9b79-0269-4dee-bfb0-be8ed4f4dc5d");
            Assert.NotNull(replace);
            Assert.AreEqual("*", replace.From);
            Assert.AreEqual("", replace.To);
            Assert.AreEqual(true, replace.Akh);
            Assert.AreEqual(true, replace.Akl);
            Assert.AreEqual(true, replace.Ame);
            Assert.AreEqual(true, replace.Ach);
            Assert.AreEqual(true, replace.Acl);
            Assert.AreEqual(false, replace.Mode);
            replace = allomorphGenerators.FindReplaceOp("34e77406-d2fe-4526-9bf9-3bc8fa653190");
            Assert.NotNull(replace);
            Assert.AreEqual("+", replace.From);
            Assert.AreEqual("", replace.To);
            Assert.AreEqual(true, replace.Akh);
            Assert.AreEqual(true, replace.Akl);
            Assert.AreEqual(true, replace.Ame);
            Assert.AreEqual(true, replace.Ach);
            Assert.AreEqual(true, replace.Acl);
            Assert.AreEqual(false, replace.Mode);
            replace = allomorphGenerators.FindReplaceOp("0f853476-407d-40e9-a8f3-803792f4f83e");
            Assert.NotNull(replace);
            Assert.AreEqual(":", replace.From);
            Assert.AreEqual("", replace.To);
            Assert.AreEqual(true, replace.Akh);
            Assert.AreEqual(true, replace.Akl);
            Assert.AreEqual(false, replace.Ame);
            Assert.AreEqual(true, replace.Ach);
            Assert.AreEqual(true, replace.Acl);
            Assert.AreEqual(false, replace.Mode);
            replace = allomorphGenerators.FindReplaceOp("4c3f43c6-f130-4767-9a5a-f2a93b1c6222");
            Assert.NotNull(replace);
            Assert.AreEqual(":", replace.From);
            Assert.AreEqual("a", replace.To);
            Assert.AreEqual(true, replace.Ame);
            Assert.AreEqual(false, replace.Akh);
            Assert.AreEqual(false, replace.Akl);
            Assert.AreEqual(false, replace.Ach);
            Assert.AreEqual(false, replace.Acl);
            Assert.AreEqual(false, replace.Mode);
            replace = allomorphGenerators.FindReplaceOp("34e7XYZ7406-d2fe-4526-9bf9-3bc8fa653190");
            Assert.Null(replace);
        }
    }
}
