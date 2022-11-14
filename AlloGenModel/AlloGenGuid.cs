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
    abstract public class AlloGenGuid : AlloGenBase
    {
        [XmlAttribute("guid")]
        public string Guid { get; set; } = "";
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        override public string ToString()
        {
            return Name;
        }

    }
}
