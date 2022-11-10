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
    public class Pattern
    {
        public string Match { get; set; }
        // match mode: false = plain; true = regular expression
        [XmlAttribute("matchmode")]
        public bool MatchMode { get; set; } = false;
        public List<MorphType> MorphTypes { get; set; }
        public List<Category> Categories { get; set; }
        public Pattern()
        {
            MorphTypes = new List<MorphType>();
            Categories = new List<Category>();
        }
    }
}
