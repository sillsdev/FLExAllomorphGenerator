// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.AlloGenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIL.AlloGenService
{
    public class Replacer
    {
        public List<Replace> ReplaceOps { get; set; }

        public Replacer(List<Replace> replaceOps)
        {
            ReplaceOps = replaceOps;
        }

        public string ApplyReplaceOpToOneDialect(string input, Dialect dialect)
        {
            string result = input;
            foreach (Replace replace in ReplaceOps)
            {
                if (!replace.Active || replace.From.Length == 0)
                    continue;
                switch (dialect)
                {
                    case Dialect.Ach:
                        if (!replace.Ach)
                            continue;
                        break;
                    case Dialect.Acl:
                        if (!replace.Acl)
                            continue;
                        break;
                    case Dialect.Akh:
                        if (!replace.Akh)
                            continue;
                        break;
                    case Dialect.Akl:
                        if (!replace.Akl)
                            continue;
                        break;
                    case Dialect.Ame:
                        if (!replace.Ame)
                            continue;
                        break;
                }
                if (replace.Mode)
                {
                    // regular expression
                    result = Regex.Replace(result, replace.From, replace.To);
                }
                else
                {
                    // plain
                    result = result.Replace(replace.From, replace.To);
                }
            }
            if (result.Length == 0)
            {
                // Use a non-breaking space when it is empty to avoid FLEx using a non-empty value in some other writing system
                result = "\u00A0";
            }
            return result;
        }
    }

    public enum Dialect
    {
        Ach,
        Acl,
        Akh,
        Akl,
        Ame,
    }
}
