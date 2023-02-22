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
    public class Replace : AlloGenGuid
    {
        // mode: false = plain; true = regular expression
        [XmlAttribute("mode")]
        public bool Mode { get; set; } = false;
        public string From { get; set; } = "";
        public string To { get; set; } = "";
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

        public string Description { get; set; } = "";

        public Replace()
        {
            if (Guid == "")
            {  // make sure it has a value
                Guid = System.Guid.NewGuid().ToString();
            };
        }

        public Replace Duplicate()
        {
            Replace newReplace = new Replace();
            newReplace.Ach = Ach;
            newReplace.Ach = Ach;
            newReplace.Acl = Acl;
            newReplace.Akh = Akh;
            newReplace.Akl = Akl;
            newReplace.Ame = Ame;
            newReplace.From = From;
            newReplace.Mode = Mode;
            newReplace.To = To;
            newReplace.Active = Active;
            newReplace.Description = Description;
            newReplace.Name = Name;
            return newReplace;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(": ");
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

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Replace replace = (Replace)obj;
                return base.Equals(obj)
                    && (Description == replace.Description)
                    && (Mode == replace.Mode)
                    && (From == replace.From)
                    && (To == replace.To)
                    && (Ach == replace.Ach)
                    && (Acl == replace.Acl)
                    && (Akh == replace.Akh)
                    && (Akl == replace.Akl)
                    && (Ame == replace.Ame)
                    ;
            }
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode() + Tuple.Create(Description, Mode).GetHashCode();
            return result + Tuple.Create(From, To, Ach, Acl, Akh, Akl, Ame).GetHashCode();
        }

    }
}
