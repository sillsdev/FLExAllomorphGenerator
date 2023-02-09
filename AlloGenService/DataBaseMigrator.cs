// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.AlloGenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenService
{
    public class DatabaseMigrator
    {
        const int latestVersion = 2;

        public AllomorphGenerators Migrate(AllomorphGenerators oldDatabase)
        {
            AllomorphGenerators newDatabase = new AllomorphGenerators();
            int version = oldDatabase.DbVersion;
            // special case since we did not add a db version until version 2
            if (oldDatabase.ReplaceOperations.Count == 0)
                version = 1;
            bool didMigration = false;
            while (version < latestVersion)
            {
                switch (version)
                {
                    case 1:
                        newDatabase = Migrate01to02(oldDatabase);
                        didMigration = true;
                        break;
                    default:
                        Console.WriteLine("Migrator: version=" + version);
                        break;
                }
                version++;
            }
            if (didMigration)
                return newDatabase;
            else
                return oldDatabase;
        }

        AllomorphGenerators Migrate01to02(AllomorphGenerators oldDatabase)
        {
            AllomorphGenerators newDatabase = new AllomorphGenerators();
            newDatabase.DbVersion = 2;
            newDatabase.Operations = oldDatabase.Operations;
            foreach (Operation operation in newDatabase.Operations)
            {
                AlloGenModel.Action action = operation.Action;
                foreach (Replace replace in action.ReplaceOps)
                {
                    if (String.IsNullOrEmpty(replace.Guid))
                        replace.Guid = Guid.NewGuid().ToString();
                    newDatabase.ReplaceOperations.Add(replace);
                    action.ReplaceOpRefs.Add(replace.Guid);
                }
                action.ReplaceOps.Clear();
            }
            return newDatabase;
        }
    }
}
