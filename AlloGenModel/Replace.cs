// Copyright (c) 2022 SIL International
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
    public class Replace : AlloGenBase
    {
        public string From { get; set; }
        public List<ReplaceTo> To { get; set; }
        // mode: false = plain; true = regular expression
        [XmlAttribute("mode")]
        public bool Mode { get; set; } = false;

        public Replace()
        {
            To = new List<ReplaceTo>();
        }
    }
}
