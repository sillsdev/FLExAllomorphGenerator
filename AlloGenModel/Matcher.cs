// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FieldWorks.Filters;
using SIL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SIL.AlloGenModel
{
    public class Matcher
    {
        public MatcherType Type { get; set; } = MatcherType.Anywhere;
        public string Pattern { get; set; } = "";
        public bool MatchCase { get; set; } = false;
        public bool MatchDiacritics { get; set; } = false;

        public Matcher()
        {

        }

        public Matcher(MatcherType type)
        {
            Type = type;
        }

        public string CreateFwXmlString(int ws)
        {
            StringBuilder sb = new StringBuilder();
            //< matcher assemblyPath = "Filters.dll" class="SIL.FieldWorks.Filters.BeginMatcher" 
            //label ="&amp;lt;Str&amp;gt;&amp;#xD;&amp;#xA;&amp;lt;Run ws=&amp;quot;en&amp;quot;&amp;gt;to&amp;lt;/Run&amp;gt;&amp;#xD;&amp;#xA;&amp;lt;/Str&amp;gt;"
            // pattern ="to" ws="999000001" matchCase="False" matchDiacritics="False" />
            sb.Append("<matcher assemblyPath=\"Filters.dll\" class=\"SIL.FieldWorks.Filters.");
            sb.Append(GetMatcherTypeName());
            sb.Append("\" label=\"\"");
            sb.Append(" pattern=\"");
            sb.Append(Pattern);
            sb.Append("\" ws=\"");
            sb.Append(ws.ToString());
            sb.Append("\" matchCase=\"");
            sb.Append(MatchCase.ToString());
            sb.Append("\" matchDiacritics=\"");
            sb.Append(MatchDiacritics);
            sb.Append("\"/>");
            return sb.ToString();
        }

        string GetMatcherTypeName()
        {
            StringBuilder sb = new StringBuilder();
            switch (Type)
            {
                case MatcherType.Begin:
                    sb.Append("Begin");
                    break;
                case MatcherType.End:
                    sb.Append("End");
                    break;
                case MatcherType.Exact:
                    sb.Append("Exact");
                    break;
                case MatcherType.RegularExpression:
                    sb.Append("RegExp");
                    break;
                default:
                    sb.Append("Anywhere");
                    break;
            }
            sb.Append("Matcher");
            return sb.ToString();
        }

        public IMatcher GetFwMatcher(int ws)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(CreateFwXmlString(ws));
            IMatcher fwMatcher = DynamicLoader.RestoreObject(doc.FirstChild) as IMatcher;
            return fwMatcher;
        }
    }

    public enum MatcherType
    {
        Anywhere,
        Begin,
        End,
        Exact,
        RegularExpression,
    }
}
