// Copyright (c) 2022-2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.AlloGenModel;
using SIL.FieldWorks.Filters;
using SIL.LCModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenService
{
    public class PatternMatcher
    {
        public Pattern Pattern { get; set; }
        LcmCache Cache { get; set; }
        public IEnumerable<ILexEntry> AllEntries { get; set; }
        public IEnumerable<ILexEntry> SingleAllomorphs { get; set; }
        public IMoMorphType morphType { get; set; }
        public List<IMoMorphType> morphTypes { get; set; } = new List<IMoMorphType>();
        public string ErrorMessage { get; set; } = "";

        public PatternMatcher(Pattern pattern, LcmCache cache)
        {
            Pattern = pattern;
            Cache = cache;
            AllEntries = Cache.LanguageProject.LexDbOA.Entries;
            SingleAllomorphs = AllEntries.Where(e => e.AllAllomorphs.Length == 1);
        }

        public IEnumerable<ILexEntry> MatchMorphTypes(IEnumerable<ILexEntry> lexEntries)
        {
            if (Pattern.MorphTypes.Count > 0)
            {
                morphTypes.Clear();
                foreach (IMoMorphType mt in Cache.LangProject.LexDbOA.MorphTypesOA.PossibilitiesOS)
                {
                    if (Pattern.MorphTypes.Where(m => m.Guid == mt.Guid.ToString()).Count() > 0)
                    {
                        morphTypes.Add(mt);
                    }
                }
                var lexEntriesForMorphTypes = new List<ILexEntry>();
                foreach (ILexEntry e in lexEntries)
                {
                    foreach (IMoMorphType mt in morphTypes)
                    {
                        if (e.MorphTypes.Contains(mt))
                        {
                            lexEntriesForMorphTypes.Add(e);
                            break;
                        }
                    }
                }
                return lexEntriesForMorphTypes;
            }
            return lexEntries;
        }

        public IEnumerable<ILexEntry> MatchCategory(IEnumerable<ILexEntry> lexEntries)
        {
            var lexEntriesForCategory = new List<ILexEntry>();
            if (Pattern.Category != null && Pattern.Category.Active && Pattern.Category.Guid.Length > 0)
            {
                foreach (ILexEntry e in lexEntries)
                {
                    foreach (IMoMorphSynAnalysis msa in e.MorphoSyntaxAnalysesOC)
                    {
                        IMoStemMsa stemMsa = msa as IMoStemMsa;
                        if (stemMsa != null && stemMsa.PartOfSpeechRA != null)
                        {
                            if (stemMsa.PartOfSpeechRA.Guid.ToString() == Pattern.Category.Guid)
                            {
                                lexEntriesForCategory.Add(e);
                                break;
                            }
                        }
                    }
                }
                return lexEntriesForCategory;
            }
            return lexEntries;
        }

        public IEnumerable<ILexEntry> MatchMatchString(IEnumerable<ILexEntry> lexEntries)
        {
            int ws = Cache.DefaultVernWs;
            Matcher agMatcher = Pattern.Matcher;
            string errorMessage = "";
            IMatcher fwMatcher = agMatcher.GetFwMatcher(ws, out errorMessage);
            ErrorMessage = errorMessage;
            if (fwMatcher == null)
                return null;
            var lexEntriesPerMatchString = new List<ILexEntry>();
            foreach (ILexEntry e in lexEntries)
            {
                if (fwMatcher.Matches(e.CitationForm.VernacularDefaultWritingSystem))
                {
                    lexEntriesPerMatchString.Add(e);
                }
            }
            return lexEntriesPerMatchString;
        }

        public IEnumerable<ILexEntry> MatchPattern(IEnumerable<ILexEntry> lexEntries)
        {
            var lexEntriesThatMatch = MatchMatchString(lexEntries);
            if (lexEntriesThatMatch == null)
                return null;
            lexEntriesThatMatch = MatchCategory(lexEntriesThatMatch);
            lexEntriesThatMatch = MatchMorphTypes(lexEntriesThatMatch);
            return lexEntriesThatMatch;
        }
    }
}
