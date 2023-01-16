// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using SIL.LCModel;
using SIL.WritingSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenServiceTest
{
    class FwTestBase : MemoryOnlyBackendProviderTestBase
    {
        protected string TestDataDir { get; set; }
        protected string FieldWorksTestFile { get; set; }
        protected LcmCache myCache { get; set; }
        // Following three needed to get cache
        protected ILcmUI m_ui;
        protected string m_projectsDirectory;
        protected ILcmDirectories m_lcmDirectories;

        protected XmlBackEndProvider provider = new XmlBackEndProvider();
        protected string AlloGenExpected { get; set; }
        protected AllomorphGenerators allomorphGenerators;
        protected Operation operation;
        protected Pattern pattern { get; set; }
        protected PatternMatcher patternMatcher { get; set; }

        protected int wsForAkh = 999000005;
        protected int wsForAcl = 999000004;
        protected int wsForAkl = 999000006;
        protected int wsForAch = 999000003;
        protected int wsForAme = 999000007;
        Dictionary<Dialect, int> dictWritingSystems = new Dictionary<Dialect, int>();

        public override void FixtureSetup()
        {
            if (!Sldr.IsInitialized)
                Sldr.Initialize();
            base.FixtureSetup();
            m_projectsDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(m_projectsDirectory);
            m_ui = new DummyLcmUI();
            m_lcmDirectories = new TestLcmDirectories(m_projectsDirectory);
            CreateWritingSystemDictionary();
        }

        private void CreateWritingSystemDictionary()
        {
            dictWritingSystems.Add(Dialect.Akh, wsForAkh);
            dictWritingSystems.Add(Dialect.Acl, wsForAcl);
            dictWritingSystems.Add(Dialect.Akl, wsForAkl);
            dictWritingSystems.Add(Dialect.Ach, wsForAch);
            dictWritingSystems.Add(Dialect.Ame, wsForAme);
        }

        [SetUp]
        virtual public void Setup()
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
            allomorphGenerators = provider.AlloGens;
            operation = allomorphGenerators.Operations[0];
            pattern = operation.Pattern;
            patternMatcher = new PatternMatcher(myCache, dictWritingSystems);
        }
    }
}
