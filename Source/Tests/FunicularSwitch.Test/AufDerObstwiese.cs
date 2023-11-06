using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunicularSwitch.Test.AufDerWiese;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace FunicularSwitch.Test
{
    [TestClass]
    public class Baumschnitt
    {
        [TestMethod]
        public void FehlerAufsammeln()
        {
            var ergebnis = new AufDerWiese()
                .SchneideApfelBaum(
                    new Apfelbaum(Apfelsorte.Bonapfel, 6),
                    new List<Baumpfleger>
                    {
                        new Baumpfleger("Horst", 55, Fähigkeit.Fortgeritten, false),
                        new Baumpfleger("Hans", 102, Fähigkeit.Profi),
                        new Baumpfleger("Alex", 40, Fähigkeit.Ahnungslos)
                    },
                    new Ausrüstung(leiter: null, scheren: new List<Schere> { new Schere(scharf: false) }),
                    new Wetter(sonnig: true, windstärke: 6));

            ergebnis.IsOk.Should().BeFalse();
            Console.WriteLine(ergebnis.GetErrorOrDefault());
        }
    }

    public class AufDerWiese
    {
        readonly Welt _welt;

        public AufDerWiese() => _welt = new Welt();

        public Result<GepflegterBaum> SchneideApfelBaum(Baum baum, ICollection<Baumpfleger> gärtner, Ausrüstung ausrüstung, Wetter wetter) =>
            BestimmeApfel(baum)
                .Bind(apfelSorte =>
                {
                    var geeignetesWetter = PasstDasWetter(wetter);
                    var geeigneterPfleger = WerSchneidet(gärtner, apfelSorte, baum.HöheInMetern, geeignetesWetter);
                    var geeigneteAusrüstung = PasstDieAusrüstung(baum, ausrüstung);

                    return geeigneterPfleger.Aggregate(geeigneteAusrüstung, geeignetesWetter);
                })
                .Bind(wasManSoBraucht =>
                {
                    var (baumpfleger, (leiter, schere), _) = wasManSoBraucht;
                    return Schneiden(baumpfleger, leiter, schere);
                });

        static Result<GutesWetter> PasstDasWetter(Wetter wetter)
        {
            if (!wetter.Sonnig)
                return Result.Error<GutesWetter>("Die Sonne scheint nicht");

            if (wetter.Windstärke >= 7)
                return Result.Error<GutesWetter>("Es stürmt");

            return new GutesWetter(wetter.Windstärke);
        }

        static Result<(Leiter? leiter, Schere schere)> PasstDieAusrüstung(Baum baum, Ausrüstung ausrüstung)
        {
            var leiter = baum.HöheInMetern < 2
                ? (Leiter?)null
                :
                ausrüstung.Leiter == null
                    ? Result.Error<Leiter?>("Wir haben keine Leiter")
                    :
                    ausrüstung.Leiter.LängeInMetern < baum.HöheInMetern - 2
                        ?
                        Result.Error<Leiter?>("Die Leiter ist zu kurz")
                        : ausrüstung.Leiter;

            var schere = ausrüstung.Scheren
                .First(s => s.Scharf, () => "Es gibt keine scharfe Schere");

            return leiter.Aggregate(schere);
        }

        static Result<Baumpfleger> WerSchneidet(IEnumerable<Baumpfleger> kandidaten, Apfelsorte sorte,
            double baumHöhe, Result<GutesWetter> geeignetesWetter)
        {
            IEnumerable<string> IstGeignet(Baumpfleger baumpfleger)
            {
                if (baumpfleger.Fähigkeit == Fähigkeit.Ahnungslos)
                {
                    yield return $"{baumpfleger.Name} muss erst in den Schnittkurs";
                }

                if (baumHöhe > 2 && (baumpfleger.AlterInJahren > 100 || baumpfleger.AlterInJahren > 80 &&
                    geeignetesWetter.Map(wetter => wetter.Windstärke).GetValueOrDefault(() => 0) > 5))
                {
                    yield return "Für {baumpfleger.Name} wirds schwierig mit der Leiter";
                }

                if (!baumpfleger.HatSchonmalGehört(sorte))
                {
                    yield return $"{baumpfleger.Name} kennt die Sorte {sorte} überhaupt nicht";
                }
            }

            return kandidaten.FirstOk(IstGeignet, () => "Kein Kandidat")
                .MapError(error => $"Kein geeigneter Baumpfleger anwesend: {error}");
        }

        static Result<Apfelsorte> BestimmeApfel(Baum baum) =>
            baum is Apfelbaum apfelbaum
            ? apfelbaum.Sorte
            : Result.Error<Apfelsorte>($"Das ist gar kein Apfelbaum, sondern ein {baum.GetType().Name}");

        Result<GepflegterBaum> Schneiden(Baumpfleger pfeger, Leiter? leiter, Schere schere)
        {
            try
            {
                return _welt.SchneideBaum(pfeger, leiter, schere);
            }
            catch (VomBaumGefallen)
            {
                return Result.Error<GepflegterBaum>("Ups, runtergefallen.");
            }
            catch (VesperPause)
            {
                return Result.Error<GepflegterBaum>("Brotzeit kam dazwischen");
            }
            catch (Exception e)
            {
                return Result.Error<GepflegterBaum>($"Irgendwas ist beim Schneiden schief gelaufen: {e}");
            }
        }

        public class Welt
        {
            readonly bool _guterTag;

            public Welt() => _guterTag = new Random().Next(10) > 2;

            public Result<GepflegterBaum> SchneideBaum(Baumpfleger pfeger, Leiter? leiter, Schere schere) =>
                _guterTag ? new GepflegterBaum() : throw new VomBaumGefallen();
        }

        public class Wetter
        {
            public bool Sonnig { get; }
            public int Windstärke { get; }

            public Wetter(bool sonnig, int windstärke)
            {
                Sonnig = sonnig;
                Windstärke = windstärke;
            }
        }

        public class Ausrüstung
        {
            public Leiter? Leiter { get; }
            public IReadOnlyCollection<Schere> Scheren { get; }

            public Ausrüstung(Leiter? leiter, IReadOnlyCollection<Schere> scheren)
            {
                Leiter = leiter;
                Scheren = scheren;
            }
        }

        public class GutesWetter
        {
            public int Windstärke { get; }

            public GutesWetter(int windstärke) => Windstärke = windstärke;
        }

        public class Leiter
        {
            public double LängeInMetern { get; }

            public Leiter(double längeInMetern) => LängeInMetern = längeInMetern;
        }

        public class Baumpfleger
        {
            readonly bool _kenntApfelSorten;
            public string Name { get; }
            public int AlterInJahren { get; }
            public Fähigkeit Fähigkeit { get; }

            public Baumpfleger(string name, int alterInJahren, Fähigkeit fähigkeit, bool kenntApfelSorten = true)
            {
                _kenntApfelSorten = kenntApfelSorten;
                Name = name;
                AlterInJahren = alterInJahren;
                Fähigkeit = fähigkeit;
            }

            public bool HatSchonmalGehört(Apfelsorte sorte) => _kenntApfelSorten;
        }

        public class GepflegterBaum
        {
        }

        public enum Fähigkeit
        {
            Ahnungslos,
            Änfänger,
            Fortgeritten,
            Profi
        }

        public abstract class Baum
        {
            public double HöheInMetern { get; }

            protected Baum(double höheInMetern) => HöheInMetern = höheInMetern;
        }

        public class Apfelbaum : Baum
        {
            public Apfelsorte Sorte { get; }

            public Apfelbaum(Apfelsorte sorte, double höheInMetern) : base(höheInMetern) => Sorte = sorte;
        }

        public enum Apfelsorte
        {
            Topaz,
            Bonapfel,
            Renette
        }


        class VomBaumGefallen : Exception
        {
        }

        class VesperPause : Exception
        {
        }

        public class Schere
        {
            public bool Scharf { get; }

            public Schere(bool scharf) => Scharf = scharf;
        }
    }
}
