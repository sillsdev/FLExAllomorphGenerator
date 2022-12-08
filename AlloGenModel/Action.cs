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
        public StemName StemName { get; set; } = new StemName();

        public Action()
        {
            ReplaceOps = new List<Replace>();
            Environments = new List<Environment>();
        }

        public Action Duplicate()
        {
            Action newAction = new Action();
            List<Replace> newReplaceOps = new List<Replace>();
            foreach (Replace rep in ReplaceOps)
            {
                newReplaceOps.Add(rep.Duplicate());
            }
            newAction.ReplaceOps = newReplaceOps;
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
            return newAction;
        }
    }
}
