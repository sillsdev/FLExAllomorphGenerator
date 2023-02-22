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
        public List<Operation> Operations { get; set; } = new List<Operation>();
        public List<Replace> ReplaceOperations { get; set; } = new List<Replace>();

        public AllomorphGenerators()
        {
        }

        public bool ReplaceOpExists(Replace replace)
        {
            if (replace == null)
            {
                return false;
            }
            return ReplaceOperations.Contains(replace);
        }

        public void ReplaceOpAdd(Replace replace)
        {
            if (replace != null)
            {
                if (ReplaceOpExists(replace))
                {
                    ReplaceOperations.Remove(replace);
                }
                ReplaceOperations.Add(replace);
            }
        }

        public Replace FindReplaceOp(string guid)
        {
            Replace replace = ReplaceOperations.FirstOrDefault(r => r.Guid == guid);
            return replace;
        }
    }
}
