// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SIL.AlloGenModel;
using SIL.AlloGenService;

namespace SIL.AlloGenServiceTest
{
    [TestFixture]
    class DatabaseMigratorTest : AlloGenTestBase
    {
        string AlloGenProduced { get; set; }
        DatabaseMigrator migrator = new DatabaseMigrator();

#if Marks
        [Test]
        public void Migrate01to02Test()
        {
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenVersion01.agf");
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators oldAlloGen = provider.AlloGens;
            Assert.NotNull(oldAlloGen);
            Assert.AreEqual(3, oldAlloGen.DbVersion);
            Assert.AreEqual(0, oldAlloGen.ReplaceOperations.Count);

            migrator.LatestVersion = 2;
            Assert.AreEqual(2, migrator.LatestVersion);
            AllomorphGenerators newAlloGen = migrator.Migrate(oldAlloGen, AlloGenExpected);
            Assert.AreEqual(2, newAlloGen.DbVersion);
            Assert.AreEqual(4, newAlloGen.ReplaceOperations.Count);
            Operation operation = oldAlloGen.Operations[0];
            AlloGenModel.Action action = operation.Action;
            Assert.AreEqual(4, action.ReplaceOpRefs.Count);
            string replaceOpRef = action.ReplaceOpRefs[0];
            Replace replace = newAlloGen.ReplaceOperations[0];
            CheckReplaceValues(replace, "*", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[1];
            replace = newAlloGen.ReplaceOperations[1];
            CheckReplaceValues(replace, "+", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[2];
            replace = newAlloGen.ReplaceOperations[2];
            CheckReplaceValues(replace, ":", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[3];
            replace = newAlloGen.ReplaceOperations[3];
            CheckReplaceValues(replace, ":", "a", replaceOpRef);
            Assert.AreEqual(0, action.ReplaceOps.Count);
        }

        [Test]
        public void Migrate02to03Test()
        {
            AlloGenExpected = Path.Combine(TestDataDir, "AlloGenVersion02.agf");
            provider.LoadDataFromFile(AlloGenExpected);
            AllomorphGenerators oldAlloGen = provider.AlloGens;
            Assert.NotNull(oldAlloGen);
            Assert.AreEqual(2, oldAlloGen.DbVersion);
            Assert.AreEqual(4, oldAlloGen.ReplaceOperations.Count);

            migrator.LatestVersion = 3;
            Assert.AreEqual(3, migrator.LatestVersion);
            AllomorphGenerators newAlloGen = migrator.Migrate(oldAlloGen, AlloGenExpected);
            Assert.AreEqual(3, newAlloGen.DbVersion);
            Assert.AreEqual(4, newAlloGen.ReplaceOperations.Count);
            Operation operation = oldAlloGen.Operations[0];
            AlloGenModel.Action action = operation.Action;
            Assert.AreEqual(4, action.ReplaceOpRefs.Count);
            string replaceOpRef = action.ReplaceOpRefs[0];
            Replace replace = newAlloGen.ReplaceOperations[0];
            CheckReplaceValues(replace, "*", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[1];
            replace = newAlloGen.ReplaceOperations[1];
            CheckReplaceValues(replace, "+", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[2];
            replace = newAlloGen.ReplaceOperations[2];
            CheckReplaceValues(replace, ":", "", replaceOpRef);
            replaceOpRef = action.ReplaceOpRefs[3];
            replace = newAlloGen.ReplaceOperations[3];
            CheckReplaceValues(replace, ":", "a", replaceOpRef);
            Assert.AreEqual(0, action.ReplaceOps.Count);
        }

        private static void CheckReplaceValues(Replace replace, string from, string to, string opRef)
        {
            Assert.AreEqual(from, replace.From);
            Assert.AreEqual(to, replace.To);
            Assert.AreEqual(36, replace.Guid.Length);
            Assert.AreEqual(opRef, replace.Guid);
        }
#endif
        [Test]
        public void CreateFileNameTest()
        {
            string fileName = Path.Combine(TestDataDir, "AlloGenVersion01.agf");
            string backup = migrator.CreateBackupFileName(fileName);
            Assert.AreEqual(fileName.Substring(0, fileName.Length - 3), backup.Substring(0, backup.Length - 3));
            Assert.AreEqual("bak", backup.Substring(backup.Length - 3));

            fileName = Path.Combine(TestDataDir, "AlloGenVersion01.myext");
            backup = migrator.CreateBackupFileName(fileName);
            Assert.AreEqual(fileName.Substring(0, fileName.Length), backup.Substring(0, backup.Length - 4));
            Assert.AreEqual(".bak", backup.Substring(backup.Length - 4));
        }
    }
}
