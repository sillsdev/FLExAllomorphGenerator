﻿// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FieldWorks.Common.ViewsInterfaces;
using SIL.FieldWorks.Filters;
using SIL.LCModel.Core.Text;
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
            IVwPattern fwPattern = CreateFwPattern(ws);
            if (fwMatcher is BeginMatcher)
                fwMatcher = new BeginMatcher(fwPattern);
            else if (fwMatcher is EndMatcher)
                fwMatcher = new EndMatcher(fwPattern);
            else if (fwMatcher is ExactMatcher)
                fwMatcher = new ExactMatcher(fwPattern);
            else if (fwMatcher is RegExpMatcher)
                fwMatcher = new RegExpMatcher(fwPattern);
            else
                fwMatcher = new AnywhereMatcher(fwPattern);
            return fwMatcher;
        }

        IVwPattern CreateFwPattern(int ws)
        {
            IVwPattern fwPattern = VwPatternClass.Create();
            fwPattern.MatchCase = MatchCase;
            fwPattern.MatchDiacritics = MatchDiacritics;
            fwPattern.MatchOldWritingSystem = false;
            fwPattern.Pattern = TsStringUtils.MakeString(Pattern, ws);
            switch (Type)
            {
                case MatcherType.Anywhere:
                    fwPattern.MatchWholeWord = false;
                    fwPattern.UseRegularExpressions = false;
                    break;
                case MatcherType.Begin:
                    fwPattern.MatchWholeWord = false;
                    fwPattern.UseRegularExpressions = false;
                    break;
                case MatcherType.End:
                    fwPattern.MatchWholeWord = false;
                    fwPattern.UseRegularExpressions = false;
                    break;
                case MatcherType.Exact:
                    fwPattern.MatchWholeWord = true;
                    fwPattern.UseRegularExpressions = false;
                    break;
                case MatcherType.RegularExpression:
                    fwPattern.MatchWholeWord = false;
                    fwPattern.UseRegularExpressions = true;
                    break;
            }
            return fwPattern;
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