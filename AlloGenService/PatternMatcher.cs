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
        //public Pattern Pattern { get; set; }
        LcmCache Cache { get; set; }
        public IEnumerable<ILexEntry> AllEntries { get; set; }
        public IEnumerable<ILexEntry> EntriesWithNoAllomorphs { get; set; }
        public IEnumerable<ILexEntry> MultiAllomorphEntries { get; set; }
        public IMoMorphType morphType { get; set; }
        public List<IMoMorphType> morphTypes { get; set; } = new List<IMoMorphType>();
        public string ErrorMessage { get; set; } = "";
        List<WritingSystem> WritingSystems { get; set; } = new List<WritingSystem>();

        public PatternMatcher(LcmCache cache, List<WritingSystem> writingSystems)
        {
            Cache = cache;
            AllEntries = Cache.LanguageProject.LexDbOA.Entries;
            EntriesWithNoAllomorphs = AllEntries.Where(e => e.AlternateFormsOS.Count == 0);
            MultiAllomorphEntries = AllEntries.Where(e => e.AlternateFormsOS.Count > 0);
            WritingSystems = writingSystems;
        }

        public IEnumerable<ILexEntry> MatchMorphTypes(IEnumerable<ILexEntry> lexEntries, Pattern pattern)
        {
            if (pattern.MorphTypes.Count > 0)
            {
                morphTypes.Clear();
                foreach (IMoMorphType mt in Cache.LangProject.LexDbOA.MorphTypesOA.PossibilitiesOS)
                {
                    if (pattern.MorphTypes.Where(m => m.Guid == mt.Guid.ToString()).Count() > 0)
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

        public IEnumerable<ILexEntry> MatchCategory(IEnumerable<ILexEntry> lexEntries, Pattern pattern)
        {
            var lexEntriesForCategory = new List<ILexEntry>();
            if (pattern.Category != null && pattern.Category.Active && pattern.Category.Guid.Length > 0)
            {
                IPartOfSpeech patternPos = GetPatternsPartOfSpeech(pattern);
                if (patternPos != null)
                {
                    foreach (ILexEntry entry in lexEntries)
                    {
                        foreach (IMoMorphSynAnalysis msa in entry.MorphoSyntaxAnalysesOC)
                        {
                            IMoStemMsa stemMsa = msa as IMoStemMsa;
                            if (stemMsa != null && stemMsa.PartOfSpeechRA != null)
                            {
                                if (stemMsa.PartOfSpeechRA == patternPos || patternPos.ReallyReallyAllPossibilities.Contains(stemMsa.PartOfSpeechRA))
                                {
                                    {
                                        lexEntriesForCategory.Add(entry);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return lexEntriesForCategory;
            }
            return lexEntries;
        }

        private IPartOfSpeech GetPatternsPartOfSpeech(Pattern pattern)
        {
            foreach (IPartOfSpeech pos in Cache.LangProject.AllPartsOfSpeech)
            {
                if (pos.Guid.ToString() == pattern.Category.Guid)
                {
                    return pos;
                }
            }
            return null;
        }

        public IEnumerable<ILexEntry> MatchMatchString(IEnumerable<ILexEntry> lexEntries, Pattern pattern)
        {
            int ws = Cache.DefaultVernWs;
            Matcher agMatcher = pattern.Matcher;
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

        public IEnumerable<ILexEntry> MatchEntriesWithAllosPerPattern(Operation operation, Pattern pattern)
        {
            var lexEntriesThatMatch = MatchMatchString(MultiAllomorphEntries, pattern);
            if (lexEntriesThatMatch == null)
                return null;
            lexEntriesThatMatch = MatchCategory(lexEntriesThatMatch, pattern);
            lexEntriesThatMatch = MatchMorphTypes(lexEntriesThatMatch, pattern);

            AlloGenModel.Action action = operation.Action;
            Replacer replacer = new Replacer(action.ReplaceOps);
            IList<ILexEntry> lexEntriesWithAllosThatDoNotMatch = new List<ILexEntry>();
            foreach (ILexEntry entry in lexEntriesThatMatch)
            {
                if (!HaveSameAllomorphs(replacer, entry, action))
                {
                    lexEntriesWithAllosThatDoNotMatch.Add(entry);
                }
            }
            return lexEntriesWithAllosThatDoNotMatch;
        }

        private bool HaveSameAllomorphs(Replacer replacer, ILexEntry entry, AlloGenModel.Action action)
        {
            foreach (IMoStemAllomorph allo in entry.AlternateFormsOS)
            {
                if (HasSameAllomorph(replacer, entry, action, allo))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasSameAllomorph(Replacer replacer, ILexEntry entry, AlloGenModel.Action action, IMoStemAllomorph allo)
        {
            if (allo.StemNameRA!= null &&  allo.StemNameRA.Guid.ToString() != action.StemName.Guid)
            {
                return false;
            }
            if (!HaveSameEnvironments(allo.AllomorphEnvironments, action.Environments))
            {
                return false;
            }
            string citationForm = entry.CitationForm.VernacularDefaultWritingSystem.Text;
            foreach (WritingSystem ws in WritingSystems)
            {
                if (!HaveSameAllomorphForm(replacer, citationForm, allo, ws))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HaveSameAllomorphForm(Replacer replacer, string citationForm, IMoStemAllomorph allo, WritingSystem ws)
        {
            string previewForm = replacer.ApplyReplaceOpToOneWS(citationForm, ws.Name);
            if (ws.Handle == -1)
            {
                return false;
            }
            string alloForm = allo.Form.get_String(ws.Handle).Text;
            if (alloForm != previewForm)
            {
                return false;
            }
            return true;
        }

        private bool HaveSameEnvironments(ILcmReferenceCollection<IPhEnvironment> allosEnvs, List<AlloGenModel.Environment> actionsEnvs)
        {
            if (allosEnvs.Count != actionsEnvs.Count)
                return false;
            foreach (IPhEnvironment env in allosEnvs)
            {
                if (actionsEnvs.FindIndex(e => e.Guid == env.Guid.ToString()) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<ILexEntry> MatchPattern(IEnumerable<ILexEntry> lexEntries, Pattern pattern)
        {
            var lexEntriesThatMatch = MatchMatchString(lexEntries, pattern);
            if (lexEntriesThatMatch == null)
                return null;
            lexEntriesThatMatch = MatchCategory(lexEntriesThatMatch, pattern);
            lexEntriesThatMatch = MatchMorphTypes(lexEntriesThatMatch, pattern);
            return lexEntriesThatMatch;
        }
    }
}
