// Copyright (c) 2022-2023 SIL International
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
using Environment = SIL.AlloGenModel.Environment;

namespace SIL.AlloGenServiceTest
{
    public class XmlBackEndProviderTests : AlloGenTestBase
    {
        string AlloGenProduced { get; set; }

        [Test]
        public void LoadTest()
        {
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            Assert.NotNull(allomorphGenerators);
            Assert.AreEqual(1, allomorphGenerators.Operations.Count);
            Operation operation = allomorphGenerators.Operations[0];
            Assert.AreEqual(true, operation.Active);
            Assert.AreEqual("foreshortening", operation.Name);
            Assert.AreEqual("Add allomorphs for entries which undergo foreshortening", operation.Description);
            Pattern pattern = operation.Pattern;
            Assert.NotNull(pattern);
            Matcher matcher = pattern.Matcher;
            Assert.AreEqual(MatcherType.RegularExpression, matcher.Type);
            Assert.AreEqual(@":$|:\..+$", matcher.Pattern);
            Assert.AreEqual(true, matcher.MatchCase);
            Assert.AreEqual(false, matcher.MatchDiacritics);
            Assert.AreEqual(@":$|:\..+$", matcher.Pattern);
            Assert.AreEqual(4, pattern.MorphTypes.Count);
            string guid = pattern.MorphTypes[0].Guid;
            Assert.AreEqual("d7f713e4-e8cf-11d3-9764-00c04f186933", guid);
            string name = pattern.MorphTypes[0].Name;
            Assert.AreEqual("bound root", name);
            guid = pattern.Category.Guid;
            Assert.AreEqual("54712931-442f-42d5-8634-f12bd2e310ce", guid);
            name = pattern.Category.Name;
            Assert.AreEqual("Transitive verb", name);
            Action action = operation.Action;
            Assert.NotNull(action);
            List<Replace> replaceOps = action.ReplaceOps;
            Assert.NotNull(replaceOps);
            Assert.AreEqual(0, replaceOps.Count);
            List<string> replaceOpRefs = action.ReplaceOpRefs;
            Assert.NotNull(replaceOpRefs);
            Assert.AreEqual(4, replaceOpRefs.Count);
            guid = replaceOpRefs[0];
            Replace replace = allomorphGenerators.FindReplaceOp(guid);
            Assert.NotNull(replace);
            bool mode = replace.Mode;
            Assert.AreEqual(false, mode);
            Assert.AreEqual("*", replace.From);
            Assert.AreEqual("", replace.To);
            Assert.True(replace.Ach);
            Assert.True(replace.Acl);
            Assert.True(replace.Akh);
            Assert.True(replace.Akl);
            Assert.True(replace.Ame);
            guid = replaceOpRefs[2];
            replace = allomorphGenerators.FindReplaceOp(guid);
            Assert.NotNull(replace);
            Assert.AreEqual(":", replace.From);
            Assert.AreEqual("", replace.To);
            Assert.True(replace.Ach);
            Assert.True(replace.Acl);
            Assert.True(replace.Akh);
            Assert.True(replace.Akl);
            Assert.False(replace.Ame);
            guid = replaceOpRefs[3];
            replace = allomorphGenerators.FindReplaceOp(guid);
            Assert.NotNull(replace);
            Assert.AreEqual(":", replace.From);
            Assert.AreEqual("a", replace.To);
            Assert.False(replace.Ach);
            Assert.False(replace.Acl);
            Assert.False(replace.Akh);
            Assert.False(replace.Akl);
            Assert.True(replace.Ame);
            List<Environment> envs = action.Environments;
            Assert.NotNull(envs);
            Assert.AreEqual(2, envs.Count);
            Environment env = envs[0];
            Assert.AreEqual("d7f7123-e8cf-11d3-9733-00c04f186933", env.Guid);
            Assert.AreEqual("/ _ #", env.Name);
            env = envs[1];
            Assert.AreEqual("d7f7456-e8cf-11d3-9733-00c04f186933", env.Guid);
            Assert.AreEqual("/ ([C])[C]_ [V]", env.Name);
            StemName sn = action.StemName;
            Assert.NotNull(sn);
            Assert.AreEqual("d2cf436-e8cf-11d3-9733-00c04f186933", sn.Guid);
            Assert.AreEqual("no lowering", sn.Name);
        }

        [Test]
        public void SaveTest()
        {
            AllomorphGenerators allomorphGenerators = new AllomorphGenerators();
            Operation operation = new Operation();
            operation.Active = true;
            operation.Name = "foreshortening";
            operation.Description = "Add allomorphs for entries which undergo foreshortening";
            Pattern pattern = MakePattern();
            operation.Pattern = pattern;
            AlloGenModel.Action action = MakeAction(allomorphGenerators);
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

        private static AlloGenModel.Action MakeAction(AllomorphGenerators allomorphGenerators)
        {
            AlloGenModel.Action action = new AlloGenModel.Action();
            Replace replace1 = new Replace();
            replace1.From = "*";
            replace1.To = "";
            replace1.Guid = "5e8b9b79-0269-4dee-bfb0-be8ed4f4dc5d";
            allomorphGenerators.ReplaceOpAdd(replace1);
            action.ReplaceOpRefs.Add(replace1.Guid);
            Replace replace2 = new Replace();
            replace2.From = "+";
            replace2.To = "";
            replace2.Guid = "34e77406-d2fe-4526-9bf9-3bc8fa653190";
            allomorphGenerators.ReplaceOpAdd(replace2);
            action.ReplaceOpRefs.Add(replace2.Guid);
            Replace replace3 = new Replace();
            replace3.From = ":";
            replace3.To = "";
            replace3.Ame = false;
            replace3.Guid = "0f853476-407d-40e9-a8f3-803792f4f83e";
            allomorphGenerators.ReplaceOpAdd(replace3);
            action.ReplaceOpRefs.Add(replace3.Guid);
            Replace replace4 = new Replace();
            replace4.From = ":";
            replace4.To = "a";
            replace4.Ach = false;
            replace4.Acl = false;
            replace4.Akh = false;
            replace4.Akl = false;
            replace4.Ame = true;
            replace4.Guid = "4c3f43c6-f130-4767-9a5a-f2a93b1c6222";
            allomorphGenerators.ReplaceOpAdd(replace4);
            action.ReplaceOpRefs.Add(replace4.Guid);
            Environment env1 = new Environment();
            env1.Guid = "d7f7123-e8cf-11d3-9733-00c04f186933";
            env1.Name = "/ _ #";
            action.Environments.Add(env1);
            Environment env2 = new Environment();
            env2.Guid = "d7f7456-e8cf-11d3-9733-00c04f186933";
            env2.Name = "/ ([C])[C]_ [V]";
            action.Environments.Add(env2);
            StemName sn = new StemName();
            sn.Guid = "d2cf436-e8cf-11d3-9733-00c04f186933";
            sn.Name = "no lowering";
            action.StemName = sn;
            return action;
        }

        private static Pattern MakePattern()
        {
            Pattern pattern = new Pattern();
            Matcher matcher = new Matcher(MatcherType.RegularExpression);
            matcher.MatchCase = true;
            matcher.MatchDiacritics = false;
            matcher.Pattern = @":$|:\..+$";
            pattern.Matcher = matcher;
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
            Category cat = new Category();
            cat.Guid = "54712931-442f-42d5-8634-f12bd2e310ce";
            cat.Name = "Transitive verb";
            pattern.Category = cat;
            return pattern;
        }
    }
}