using SQLBuilder.Extensions;
using SQLBuilder.Querries;
using SQLBuilder.Querries.InsertQueryNS;
using SQLBuilder.Querries.SelectQueryNS;
using SQLBuilder.Querries.UpdateQueryNS;
using SQLBuilder.Types.CaseWhen;
using System;
using System.Linq;
using System.Collections.Generic;
using SQLBuilder.Schemas.CDNXL;

namespace SQLBuilder
{
    internal static class TestClass
    {

        public static void TestSelectCDN()
        {
            // Krotki mogą być conajmniej 2-elementowe, to jest minus całości

            var atkRunner = new Query().From<AtrybutyKlasy>(out var atk)
                .Where(atk.AtK_Nazwa.Eq("Jakiś atr"))
                .Select((atk.AtK_ID, dummy: ""))
                .GetRunner();

            var atkId = atkRunner.ResultRows.Count > 0 ? atkRunner.ResultRows[0].AtK_ID.Val : 0;

            var runner = new Query().From<KntKarty>(out var knt)
                .AllWithNolock()
                .LeftJoin<KntOsoby>(out var oso)
                .On(knt.Knt_GIDNumer.Eq(oso.KnS_KntNumer))
                .LeftJoin<Atrybuty>(out var atk1)
                .On(atk1.Atr_AtkId.Eq(atkId))
                .Select((
                    knt.Knt_Akronim,
                    knt.Knt_Nazwa1,
                    oso.KnS_Nazwa,
                    oso.KnS_Telefon,
                    oso.KnS_TelefonK,
                    oso.KnS_EMail,
                    oso.KnS_ESklep,
                    atrWartosc: new CaseWhen(atk1.Atr_Wartosc.Eq("TAK")).Then(1).End()
                )).GetRunner();
            runner.Run(Globals.XLTestConn);

            foreach (var tup in runner.ResultRows)
            {
                //robię co chcę
                var test1 = tup.Knt_Akronim.Val;
                var test2 = tup.Knt_Nazwa1.Val;
                var test3 = tup.KnS_Nazwa.Val;
                var test4 = tup.KnS_Telefon.Val;
                var test5 = tup.KnS_TelefonK.Val;
                var test6 = tup.KnS_EMail.Val;
                var test7 = tup.KnS_ESklep.Val;
                var test8 = tup.atrWartosc.Val;
            }
        }
    }
}
