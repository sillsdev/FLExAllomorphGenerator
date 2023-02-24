// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.AlloGenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloGenModelTests
{
    class AllomorphGeneratorsTests
    {
        AllomorphGenerators alloGens { get; set; }

        [SetUp]
        public void Setup()
        {
            alloGens = new AllomorphGenerators();
        }

        [Test]
        public void NewOperationTest()
        {
            Assert.AreEqual(0, alloGens.Operations.Count);
            Operation op = alloGens.CreateNewOperation();
            Assert.AreEqual(1, alloGens.Operations.Count);
            Assert.AreEqual(1, alloGens.ReplaceOperations.Count);
            Assert.AreEqual(1, op.Action.ReplaceOpRefs.Count);
            Replace replace = alloGens.ReplaceOperations[0];
            Assert.AreEqual(replace.Guid, op.Action.ReplaceOpRefs[0]);
        }
    }
}
