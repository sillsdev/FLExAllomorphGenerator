// Copyright (c) 2022 SIL International
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
using Action = SIL.AlloGenModel.Action;

namespace AlloGenModelTests
{
    class DuplicateTests
    {
        XmlBackEndProvider provider = new XmlBackEndProvider();
        string TestDataDir { get; set; }
        string AlloGenFile { get; set; }
        string AlloGenExpected { get; set; }
        string AlloGenProduced { get; set; }

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
        public void DuplicateTest()
        {
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            Assert.NotNull(allomorphGenerators);
            Operation operation = allomorphGenerators.Operations[0];
            Operation opCopy = operation.Duplicate();
            Assert.AreEqual(operation.Name, opCopy.Name);
            Assert.AreEqual(operation.Description, opCopy.Description);
            Assert.AreEqual(operation.Active, opCopy.Active);
            Pattern pattern = operation.Pattern;
            Pattern patCopy = opCopy.Pattern;
            Assert.AreEqual(pattern.Category.Name, patCopy.Category.Name);
            Action action = operation.Action;
            Action actCopy = opCopy.Action;
            Assert.AreEqual(action.StemName.Name, actCopy.StemName.Name);
        }
    }
}
