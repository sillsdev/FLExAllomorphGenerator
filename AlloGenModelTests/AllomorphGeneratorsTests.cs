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
            Operation op = alloGens.CreateNewOperation(0);
            Assert.AreEqual(1, alloGens.Operations.Count);
            Assert.AreEqual(1, alloGens.ReplaceOperations.Count);
            Assert.AreEqual(1, op.Action.ReplaceOpRefs.Count);
            Replace replace = alloGens.ReplaceOperations[0];
            Assert.AreEqual(replace.Guid, op.Action.ReplaceOpRefs[0]);
            Assert.AreEqual(0, op.GenerationType);

            Operation op1 = alloGens.CreateNewOperation(1);
            Assert.AreEqual(2, alloGens.Operations.Count);
            Assert.AreEqual(2, alloGens.ReplaceOperations.Count);
            Assert.AreEqual(1, op1.Action.ReplaceOpRefs.Count);
            Replace replace2 = alloGens.ReplaceOperations[1];
            Assert.AreEqual(replace2.Guid, op1.Action.ReplaceOpRefs[0]);
            Assert.AreEqual(1, op1.GenerationType);
        }

        [Test]
        public void RemoveEmptyReplaceOperationsTest()
        {
            Replace rep1 = new Replace();
            rep1.From = "abc";
            rep1.To = "cba";
            alloGens.ReplaceOperations.Add(rep1);
            Replace rep2 = new Replace();
            alloGens.ReplaceOperations.Add(rep2);
            Replace rep3 = new Replace();
            rep3.Name = "my name";
            alloGens.ReplaceOperations.Add(rep3);
            Assert.AreEqual(3, alloGens.ReplaceOperations.Count);
            Operation op = new Operation(0);
            op.Action.ReplaceOpRefs.Add(rep1.Guid);
            op.Action.ReplaceOpRefs.Add(rep2.Guid);
            op.Action.ReplaceOpRefs.Add(rep3.Guid);
            Assert.AreEqual(3, op.Action.ReplaceOpRefs.Count);
            alloGens.DeleteEmptyReplaceOperationsFromAnOperation(op);
            Assert.AreEqual(2, alloGens.ReplaceOperations.Count);
            Assert.AreEqual("abc", alloGens.ReplaceOperations.ElementAt(0).From);
            Assert.AreEqual("my name", alloGens.ReplaceOperations.ElementAt(1).Name);
        }
    }
}
