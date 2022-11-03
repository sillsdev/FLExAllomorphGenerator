// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenExpected.xml");
        }

        [Test]
        public void LoadTest()
        {
            Assert.Pass();
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
            string xmlFile = Path.Combine(Path.GetTempPath(), "AlloGen.xml");
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