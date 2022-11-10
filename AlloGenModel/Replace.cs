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
        // mode: false = plain; true = regular expression
        [XmlAttribute("mode")]
        public bool Mode { get; set; } = false;
        public string From { get; set; }
        public string To { get; set; }
        [XmlAttribute("ach")]
        public bool Ach { get; set; } = true;
        [XmlAttribute("acl")]
        public bool Acl { get; set; } = true;
        [XmlAttribute("akh")]
        public bool Akh { get; set; } = true;
        [XmlAttribute("akl")]
        public bool Akl { get; set; } = true;
        [XmlAttribute("ame")]
        public bool Ame { get; set; } = true;

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Replace '");
            sb.Append(From);
            sb.Append("' with '");
            sb.Append(To);
            sb.Append("' for");
            if (Ach)
                sb.Append(" ach");
            if (Acl)
                sb.Append(" acl");
            if (Akh)
                sb.Append(" akh");
            if (Akl)
                sb.Append(" akl");
            if (Ame)
                sb.Append(" ame");
            sb.Append(".");
            return sb.ToString();
        }

    }
}
