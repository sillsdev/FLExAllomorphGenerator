// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.LCModel;
using SIL.LCModel.Core.KernelInterfaces;
using SIL.LCModel.Core.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenService
{
    public class AllomorphCreator
    {
        LcmCache Cache { get; set; }
        int wsForAkh = -1;
        int wsForAcl = -1;
        int wsForAkl = -1;
        int wsForAch = -1;
        int wsForAme = -1;

        public AllomorphCreator(LcmCache cache, int wsAkh, int wsAcl, int wsAkl, int wsAch, int wsAme)
        {
            Cache = cache;
            wsForAch = wsAch;
            wsForAcl = wsAcl;
            wsForAkh = wsAkh;
            wsForAkl = wsAkl;
            wsForAme = wsAme;
        }

        public IMoStemAllomorph CreateAllomorph(ILexEntry entry, string sAkh, string sAcl, string sAkl, string sAch, string sAme)
        {
            IMoStemAllomorph form = entry.Services.GetInstance<IMoStemAllomorphFactory>().Create();
            entry.AlternateFormsOS.Add(form);
            form.Form.set_String(wsForAkh, sAkh);
            form.Form.set_String(wsForAcl, sAcl);
            form.Form.set_String(wsForAkl, sAkl);
            form.Form.set_String(wsForAch, sAch);
            form.Form.set_String(wsForAme, sAme);
            return form;
        }

        public void AddEnvironments(IMoStemAllomorph form, List<AlloGenModel.Environment> envs)
        {
            foreach (AlloGenModel.Environment env in envs)
            {
                 var phEnv = Cache.ServiceLocator.ObjectRepository.GetObjectOrIdWithHvoFromGuid(new Guid(env.Guid));
                if (phEnv != null)
                {
                    form.AllomorphEnvironments.Add((IPhEnvironment)phEnv);
                }
            }
        }

        public void AddStemName(IMoStemAllomorph form, string stemNameGuid)
        {
            var stemName = Cache.ServiceLocator.ObjectRepository.GetObjectOrIdWithHvoFromGuid(new Guid(stemNameGuid));
            if (stemName != null)
            {
                form.StemNameRA = (IMoStemName)stemName;
            }
        }
    }
}
