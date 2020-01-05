using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    /// <summary>
    /// [G. Zink, Phonétique historique du français, ch. VI]
    /// </summary>
    public static class Part1Chapter13
    {
        public static IRule[] Rules()
        {
            return new IRule[]
            {
                RuleSet1a(),
                Rule1c(), Rule1d(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(),
                Rule3h(),
                RuleSet3(),
            };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(),
                RuleSet1a(),
                RuleSet3(),
            };
        }

        public static IRule RuleSet1a()
        {
            return new FirstRule("p1c13s1a",
                Rule1a(),  // monosyllabic
                Rule1b()); // other
        }

        public static IRule RuleSet3()
        {
            return new FirstRule("p1c13s3",
                Rule3a(),
                Rule3c(),
                Rule3d(),
                Rule3e(),
                Rule3f(),
                Rule3g(),
                Rule3b());
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c13r1a")
                .Group("")
                .From(-200).To(-100)
                .Query(q => q
                    .Match(m => m.Phon("m").With("accent", "tonic"))
                    .After(Q.End))
                .Rules(
                    r => r
                        .Named("Conservation du /m/ finale dans les monosyllabes")
                        .Phono(P.Nothing),
                    r => r
                        .Named("Chute du /m/ final")
                        .Phono(P.Erase)
                        .Rewrite(G.Erase)));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c13r1b")
                .Group("")
                .From(-200).To(-100)
                .Query(q => q
                    .Match(m => m.Phon("m"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Chute du /m/ final")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c13r1c")
                .From(-200).To(-100)
                .Query(q => q
                    .Match(m => m.Phon("n").Without("accent", "tonic"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Chute du /n/ finale")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c13r1d")
                .From(0).To(100)
                .Query(q => q
                    .Match(m => m.Phon("c"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Chute du /c/ finale")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c13r2a")
                .From(650).To(700)
                .Query(q => q
                    .Match(m => m.Phon("b", "d", "ð", "g", "v", "z", "ʣ"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Assourdissement des consonnes sonores")
                    .Phono(P.Deafen)
                    .Rewrite(g =>
                    {
                        if (g == "v")
                            return "f";
                        else
                            return g;
                    })));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c13r2b")
                .From(600).To(700)
                .Optional()
                .Query(q => q
                    .Match(m => m.Phon("m"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Dentalisation de /m/ final")
                    .Phono(px => new[] { "n" })
                    .Rewrite(_ => "n")));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c13r2c")
                .From(650).To(700)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("t"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Spirantisation de /t/ final après voyelle simple")
                    .Phono(px => new[] { "θ" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c13r2d")
                .From(1000).To(1100)
                .Query(q => q
                    .Match(m => m.Phon("θ"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /θ/ final")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c13r3a")
                .From(1200).To(1300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsConsonant))
                    .Match(m => m.Phon(IPA.IsConsonant))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de la consonne finale après consonne")
                    .Phono(P.Erase)
                    .Liaison()));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c13r3b")
                .From(1200).To(1300)
                .Optional()
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon(IPA.IsConsonant))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de la consonne finale après voyelle")
                    .Phono(P.Erase)
                    .Liaison()));
        }

        /// <summary>
        /// Amuïssement de /r/ final après voyelle
        /// </summary>
        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c13r3c")
                .From(1200).To(1300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("r"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /r/ final après voyelle")
                    .Phono(P.Erase)
                    .Liaison("r")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c13r3d")
                .From(1200).To(1300)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon("ɔ"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /s/ final après /ɔ/, fermeture de /ɔ/")
                    .Phono(_ => new[] { "oː" })
                    .Liaison("s")
                    .Rewrite(_ => "o s:0")));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c13r3e")
                .From(1200).To(1300)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon("a"),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /s/ final après /a/, vélarisation de /a/")
                    .Phono(_ => new[] { "ɑː" })
                    .Liaison("s")
                    .Rewrite(_ => "a s:0")));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c13r3f")
                .From(1200).To(1300)
                .Query(q => q
                    .Before(b => b.Phon("ə"))
                    .Match(m => m.Phon("s"))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /s/ final après /ə/")
                    .Phono(P.Erase)
                    .Liaison()));
        }

        public static Rule Rule3g()
        {
            return R.Rule(c => c
                .Id("p1c13r3g")
                .From(1200).To(1300)
                .Query(q => q
                    .Match(m => m.Seq(
                        s => s.Phon(IPA.IsVowel),
                        s => s.Phon("s")))
                    .After(Q.End))
                .Rules(r => r
                    .Named("Amuïssement de /s/ final après voyelle, qui s'allonge")
                    .Phono(px => new[] { $"{px[0]}ː" })
                    .Liaison("s")
                    .Rewrite(g => $"{g[0]} s:0")));
        }

    }
}
