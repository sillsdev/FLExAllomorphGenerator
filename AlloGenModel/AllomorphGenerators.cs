// Copyright (c) 2022-2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIL.AlloGenModel
{
    public class AllomorphGenerators
    {
        [XmlAttribute("dbVersion")]
        public int DbVersion { get; set; } = 2;
        public List<Operation> Operations { get; set; }
        public List<Replace> ReplaceOperations { get; set; } = new List<Replace>();
        public AllomorphGenerators()
        {
            Operations = new List<Operation>();
        }
    }
}
