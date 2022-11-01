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
    public class Pattern
    {
        public string Match { get; set; }
        public List<MorphType> MorphTypes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
