using NUnit.Framework;
using SIL.AlloGenService;
using SIL.LCModel;
using SIL.LCModel.Core.WritingSystems;
using SIL.LCModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.AlloGenServiceTest
{
    [TestFixture]
    class AllomorphCreatorTests : FwTestBase
    {
        protected AllomorphCreator ac;
        int wsForAkh = 999000005;
        int wsForAcl = 999000004;
        int wsForAkl = 999000006;
        int wsForAch = 999000003;
        int wsForAme = 999000007;

        [SetUp]
        override public void Setup()
        {
            base.Setup();
            GetVernacularWritingSystemCodes();
            ac = new AllomorphCreator(Cache, wsForAkh, wsForAcl, wsForAkl, wsForAch, wsForAme);
        }

        void GetVernacularWritingSystemCodes()
        {
            IList<CoreWritingSystemDefinition> vernWses = Cache.LangProject.CurrentVernacularWritingSystems;
            foreach (CoreWritingSystemDefinition def in vernWses)
            {
                //This is not found for some reason; therefore we set the wses by hand above
                // TODO: consider using a dictionary for these
                switch (def.Abbreviation.Substring(def.Abbreviation.Length - 3))
                {
                    case "akh":
                        wsForAkh = def.Handle;
                        break;
                    case "acl":
                        wsForAcl = def.Handle;
                        break;
                    case "akl":
                        wsForAkl = def.Handle;
                        break;
                    case "ach":
                        wsForAch = def.Handle;
                        break;
                    case "ame":
                        wsForAme = def.Handle;
                        break;
                }
            }
        }

        [Test]
        public void AddAllomorphToEntryTest()
        {
            Assert.NotNull(myCache);
            // following gives a "not in the right state to register a change" message
            // Not sure what to do; we'd have to manuall create an entry with the five vernacular writing systems
            //NonUndoableUnitOfWorkHelper.DoUsingNewOrCurrentUOW(Cache.ActionHandlerAccessor, () =>
            //{
            //    ILexEntry entry = pm.SingleAllomorphs.ElementAt(96);
            //    Assert.NotNull(entry);
            //    Assert.AreEqual(0, entry.AlternateFormsOS.Count);
            //    Console.WriteLine("entry cf='" + entry.CitationForm.VernacularDefaultWritingSystem.Text);
            //    string akh = "chillinyakh";
            //    string acl = "chillinyacl";
            //    string akl = "chillinyakl";
            //    string ach = "chillinyach";
            //    string ame = "chillinyaa";
            //    IMoStemAllomorph form = ac.CreateAllomorph(entry, akh, acl, akl, ach, ame);
            //    Assert.NotNull(form);
            //    Assert.AreEqual(1, entry.AlternateFormsOS.Count);
            //    Assert.AreEqual(akh, form.Form.get_String(wsForAkh));
            //    Assert.AreEqual(acl, form.Form.get_String(wsForAcl));
            //    Assert.AreEqual(akl, form.Form.get_String(wsForAkl));
            //    Assert.AreEqual(ach, form.Form.get_String(wsForAch));
            //    Assert.AreEqual(ame, form.Form.get_String(wsForAme));

            //});

        }
    }
}
