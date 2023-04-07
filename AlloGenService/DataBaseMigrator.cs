// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.AlloGenModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenService
{
    public class DatabaseMigrator
    {
        // is public and can be changed for testing purposes
        public int LatestVersion { get; set; } = 3;

        public AllomorphGenerators Migrate(AllomorphGenerators oldDatabase, string file)
        {
            AllomorphGenerators newDatabase = new AllomorphGenerators();
            int version = oldDatabase.DbVersion;
            // special case since we did not add a db version until version 2
            if (oldDatabase.ReplaceOperations.Count == 0)
                version = 1;
            bool didMigration = false;
            while (version < LatestVersion)
            {
                MakeBackupOfFile(file);
                switch (version)
                {
                    case 1:
                        newDatabase = Migrate01to02(oldDatabase);
                        didMigration = true;
                        break;
                    case 2:
                        if (didMigration)
                            newDatabase = Migrate02to03(newDatabase);
                        else
                            newDatabase = Migrate02to03(oldDatabase);
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

        void MakeBackupOfFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string backupName = CreateBackupFileName(fileName);
                File.Copy(fileName, backupName, true);
            }
        }

        public string CreateBackupFileName(string fileName)
        {
            string backupName = "";
            if (fileName.Length > 4)
            {
                int iAgf = fileName.LastIndexOf(".agf");
                if (iAgf > -1)
                {
                    backupName = fileName.Substring(0, iAgf) + ".bak";
                }
                else
                {
                    backupName = fileName + ".bak";
                }
            }
            return backupName;
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

        AllomorphGenerators Migrate02to03(AllomorphGenerators oldDatabase)
        {
            AllomorphGenerators newDatabase = new AllomorphGenerators();
            newDatabase.DbVersion = 3;
            newDatabase.Operations = oldDatabase.Operations;
            foreach (Replace replace in oldDatabase.ReplaceOperations)
            {
                int index = newDatabase.ReplaceOperations.IndexOf(replace);
                if (replace.Ach)
                {
                    replace.WritingSystemRefs.Add("qvm-ach");
                }
                if (replace.Acl)
                {
                    replace.WritingSystemRefs.Add("qvm-acl");
                }
                if (replace.Akh)
                {
                    replace.WritingSystemRefs.Add("qvm-akh");
                }
                if (replace.Akl)
                {
                    replace.WritingSystemRefs.Add("qvm-akl");
                }
                if (replace.Ame)
                {
                    replace.WritingSystemRefs.Add("qvm-ame");
                }
                newDatabase.ReplaceOperations.Add(replace);
            }
            return newDatabase;
        }

    }
}
