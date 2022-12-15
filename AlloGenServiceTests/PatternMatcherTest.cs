// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIL.LCModel.Core.Text;
using SIL.LCModel;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using SIL.WritingSystems;
using SIL.AlloGenService;
using SIL.AlloGenModel;

namespace SIL.AlloGenServiceTest
{
    [TestFixture]
    class PatternMatcherTest : MemoryOnlyBackendProviderTestBase
    {
        string TestDataDir { get; set; }
        string FieldWorksTestFile { get; set; }
        LcmCache myCache { get; set; }
        // Following three needed to get cache
        private ILcmUI m_ui;
        private string m_projectsDirectory;
        private ILcmDirectories m_lcmDirectories;
        XmlBackEndProvider provider = new XmlBackEndProvider();
        string AlloGenExpected { get; set; }
        Pattern pattern { get; set; }
        PatternMatcher pm { get; set; }

        public override void FixtureSetup()
        {
            Sldr.Initialize();
            base.FixtureSetup();
            m_projectsDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(m_projectsDirectory);
            m_ui = new DummyLcmUI();
            m_lcmDirectories = new TestLcmDirectories(m_projectsDirectory);
        }

        [SetUp]
        public void Setup()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf("AlloGenServiceTests");
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, "AlloGenServiceTests", "TestData");
            FieldWorksTestFile = Path.Combine(TestDataDir, "Quechua MYL CausDeriv.fwdata");
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenExpected.agf");

            var projectId = new TestProjectId(BackendProviderType.kXML, FieldWorksTestFile);
            // following came from LcmCacheTests.cs
            myCache = LcmCache.CreateCacheFromExistingData(projectId, "en", m_ui, m_lcmDirectories, new LcmSettings(),
                new DummyProgressDlg());

            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators allomorphGenerators = provider.AlloGens;
            pattern = allomorphGenerators.Operations[0].Pattern;
            pm = new PatternMatcher(pattern, myCache);

        }

        [Test]
        public void LoadFWDatabaseTest()
        {
            Assert.NotNull(myCache);
            var lexEntries = myCache.LangProject.LexDbOA.Entries;
            Assert.AreEqual(4530, lexEntries.Count());
            var lexEntriesWithOneAllomorph = lexEntries.Where(e => e.AllAllomorphs.Length == 1);
            Assert.AreEqual(4480, lexEntriesWithOneAllomorph.Count());
        }

        [Test]
        public void LexEntriesPerMorphTypesTest()
        {
            var lexEntriesPerMorphTypes = pm.MatchMorphTypes(pm.SingleAllomorphs);
            Assert.AreEqual(4360, lexEntriesPerMorphTypes.Count());
        }

        [Test]
        public void LexEntriesPerCategoriesTest()
        {
            // transitive verb
            pattern.Category.Guid = "54712931-442f-42d5-8634-f12bd2e310ce";
            pm.Pattern = pattern;
            var lexEntriesPerCategory = pm.MatchCategory(pm.SingleAllomorphs);
            Assert.AreEqual(664, lexEntriesPerCategory.Count());
            // Intranitive verb
            pattern.Category.Guid = "4459ff09-9ee0-4b50-8787-ae40fd76d3b7";
            pm.Pattern = pattern;
            lexEntriesPerCategory = pm.MatchCategory(pm.SingleAllomorphs);
            Assert.AreEqual(827, lexEntriesPerCategory.Count());
            // Verb
            pattern.Category.Guid = "86ff66f6-0774-407a-a0dc-3eeaf873daf7";
            pm.Pattern = pattern;
            lexEntriesPerCategory = pm.MatchCategory(pm.SingleAllomorphs);
            Assert.AreEqual(0, lexEntriesPerCategory.Count());
        }
    }
}
