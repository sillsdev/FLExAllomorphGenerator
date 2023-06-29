// Copyright (c) 2022-2023 SIL International
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
        public List<string> ReplaceOpRefs { get; set; }
        public List<Environment> Environments { get; set; }
        public StemName StemName { get; set; } = new StemName();
        public List<VariantType> VariantTypes { get; set; }

        public Action()
        {
            ReplaceOpRefs = new List<string>();
            Environments = new List<Environment>();
            VariantTypes = new List<VariantType>();
        }

        public Action Duplicate()
        {
            Action newAction = new Action();
            List<string> newReplaceOpRefs = new List<string>();
            foreach (string repRef in ReplaceOpRefs)
            {
                newReplaceOpRefs.Add(repRef);
            }
            newAction.ReplaceOpRefs = newReplaceOpRefs;
            List<Environment> newEnvironments = new List<Environment>();
            foreach (Environment env in Environments)
            {
                var newEnv = new Environment();
                newEnv.Active = env.Active;
                newEnv.Guid = env.Guid;
                newEnv.Name = env.Name;
                newEnvironments.Add(newEnv);
            }
            newAction.Environments = newEnvironments;
            var newSN = new StemName();
            newSN.Active = StemName.Active;
            newSN.Guid = StemName.Guid;
            newSN.Name = StemName.Name;
            newAction.StemName = newSN;
            List<VariantType> newVariantTypes = new List<VariantType>();
            foreach (VariantType vt in VariantTypes)
            {
                var newVT = new VariantType();
                newVT.Active = vt.Active;
                newVT.Guid = vt.Guid;
                newVT.Name = vt.Name;
                newVariantTypes.Add(newVT);
            }
            newAction.VariantTypes = newVariantTypes;
            return newAction;
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
                Action act = (Action)obj;
                return (ReplaceOpRefs.SequenceEqual(act.ReplaceOpRefs))
                    && (Environments.SequenceEqual(act.Environments))
                    && (VariantTypes.SequenceEqual(act.VariantTypes))
                    && (StemName.Equals(act.StemName));
            }
        }

        public override int GetHashCode()
        {
            return Tuple.Create(ReplaceOpRefs, Environments, StemName, VariantTypes).GetHashCode();
        }
    }
}
