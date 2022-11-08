// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Action = SIL.AlloGenModel.Action;

namespace SIL.AlloGenServiceTest
{
    public class XmlBackEndProviderTests
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
            int i = rootdir.LastIndexOf("AlloGenServiceTests");
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, "AlloGenServiceTests", "TestData");
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenExpected.agf");
        }

        [Test]
        public void LoadTest()
        {
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            Assert.NotNull(allomorphGenerators);
            Assert.AreEqual(1, allomorphGenerators.Operations.Count);
            Operation operation = allomorphGenerators.Operations[0];
            Assert.AreEqual("foreshortening", operation.Name);
            Assert.AreEqual("Add allomorphs for entries which undergo foreshortening", operation.Description);
            Pattern pattern = operation.Pattern;
            Assert.NotNull(pattern);
            Assert.AreEqual(@":$|:\..+$", pattern.Match);
            Assert.AreEqual(4, pattern.MorphTypes.Count);
            string guid = pattern.MorphTypes[0].Guid;
            Assert.AreEqual("d7f713e4-e8cf-11d3-9764-00c04f186933", guid);
            string name = pattern.MorphTypes[0].Name;
            Assert.AreEqual("bound root", name);
            Action action = operation.Action;
            Assert.NotNull(action);
            List<Replace> replaceOps = action.ReplaceOps;
            Assert.NotNull(replaceOps);
            Assert.AreEqual(3, replaceOps.Count);
            Replace replace = replaceOps[0];
            Assert.NotNull(replace);
            bool mode = replace.Mode;
            Assert.AreEqual(true, mode);
            Assert.AreEqual("*", replace.From);
            ReplaceTo replaceTo = replace.To[0];
            Assert.NotNull(replaceTo);
            Assert.AreEqual("", replaceTo.To);
            Assert.True(replaceTo.Ach);
            Assert.True(replaceTo.Acl);
            Assert.True(replaceTo.Akh);
            Assert.True(replaceTo.Akl);
            Assert.True(replaceTo.Ame);
            replace = replaceOps[2];
            Assert.NotNull(replace);
            Assert.AreEqual(":", replace.From);
            replaceTo = replace.To[0];
            Assert.NotNull(replaceTo);
            Assert.AreEqual("", replaceTo.To);
            Assert.True(replaceTo.Ach);
            Assert.True(replaceTo.Acl);
            Assert.True(replaceTo.Akh);
            Assert.True(replaceTo.Akl);
            Assert.False(replaceTo.Ame);

        }

        [Test]
        public void SaveTest()
        {
            AllomorphGenerators allomorphGenerators = new AllomorphGenerators();
            Operation operation = new Operation();
            operation.Name = "foreshortening";
            operation.Description = "Add allomorphs for entries which undergo foreshortening";
            Pattern pattern = MakePattern();
            operation.Pattern = pattern;
            AlloGenModel.Action action = MakeAction();
            operation.Action = action;
            allomorphGenerators.Operations.Add(operation);
            string xmlFile = Path.Combine(Path.GetTempPath(), "AlloGen.agf");
            provider.AlloGens = allomorphGenerators;
            provider.SaveDataToFile(xmlFile);
            using (var streamReader = new StreamReader(AlloGenExpected, Encoding.UTF8))
            {
                AlloGenExpected = streamReader.ReadToEnd().Replace("\r", "");
            }
            using (var streamReader = new StreamReader(xmlFile, Encoding.UTF8))
            {
                AlloGenProduced = streamReader.ReadToEnd().Replace("\r", "");
            }
            Assert.AreEqual(AlloGenExpected, AlloGenProduced);
        }

        private static AlloGenModel.Action MakeAction()
        {
            AlloGenModel.Action action = new AlloGenModel.Action();
            Replace replace1 = new Replace();
            replace1.From = "*";
            ReplaceTo replaceTo11 = new ReplaceTo();
            replaceTo11.To = "";
            replace1.To.Add(replaceTo11);
            action.ReplaceOps.Add(replace1);
            Replace replace2 = new Replace();
            replace2.From = "+";
            ReplaceTo replaceTo21 = new ReplaceTo();
            replaceTo21.To = "";
            replace2.To.Add(replaceTo21);
            action.ReplaceOps.Add(replace2);
            Replace replace3 = new Replace();
            replace3.From = ":";
            ReplaceTo replaceTo31 = new ReplaceTo();
            replaceTo31.To = "";
            replaceTo31.Ame = false;
            ReplaceTo replaceTo32 = new ReplaceTo();
            replaceTo32.To = "a";
            replaceTo32.Ach = false;
            replaceTo32.Acl = false;
            replaceTo32.Akh = false;
            replaceTo32.Akl = false;
            replaceTo32.Ame = true;
            replace3.To.Add(replaceTo31);
            replace3.To.Add(replaceTo32);
            action.ReplaceOps.Add(replace3);
            return action;
        }

        private static Pattern MakePattern()
        {
            Pattern pattern = new Pattern();
            pattern.Match = @":$|:\..+$";
            MorphType morphType1 = new MorphType();
            morphType1.Guid = "d7f713e4-e8cf-11d3-9764-00c04f186933";
            morphType1.Name = "bound root";
            MorphType morphType2 = new MorphType();
            morphType2.Guid = "d7f713e7-e8cf-11d3-9764-00c04f186933";
            morphType2.Name = "bound stem";
            MorphType morphType3 = new MorphType();
            morphType3.Guid = "d7f713e5-e8cf-11d3-9764-00c04f186933";
            morphType3.Name = "root";
            MorphType morphType4 = new MorphType();
            morphType4.Guid = "d7f713e8-e8cf-11d3-9764-00c04f186933";
            morphType4.Name = "stem";
            pattern.MorphTypes.Add(morphType1);
            pattern.MorphTypes.Add(morphType2);
            pattern.MorphTypes.Add(morphType3);
            pattern.MorphTypes.Add(morphType4);
            return pattern;
        }
    }
}