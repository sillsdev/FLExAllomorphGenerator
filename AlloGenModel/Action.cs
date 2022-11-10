// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenModel
{
    public class Action
    {
        public List<Replace> ReplaceOps { get; set; }
        public List<Environment> Environments { get; set; }
        public InflectionFeature InflectionFeatures { get; set; }
        public StemName StemName { get; set; }

        public Action()
        {
            ReplaceOps = new List<Replace>();
            Environments = new List<Environment>();
        }
    }
}
