using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter10
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(), Rule2e(), Rule2f(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(),
                Rule4a(), Rule4b(), Rule4c(),
                Rule5a(), Rule5b(), Rule5c(), Rule5d(),
                Rule6(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c10r1a")
                .From(0).To(100)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("b"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Spirantisation de /b/ intervocalique")
                    .Phono(px => new[] { "β" })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c10r1b")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(p => IPA.IsVowel(p) && !IPA.IsVelarVowel(p)))
                    .Match(m => m.Phon("β"))
                    .After(b => b.Phon(IPA.IsPalatalVowel)))
                .Rules(r => r
                    .Named("Renforcement de /β/ entre voyelles palatales")
                    .Phono(px => new[] { "v" })
                    .Rewrite(_ => "v")));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c10r1c")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(p => IPA.IsVowel(p) && !IPA.IsPalatalVowel(p)))
                    .Match(m => m.Phon("β"))
                    .After(b => b.Phon(IPA.IsVelarVowel)))
                .Rules(r => r
                    .Named("Vélarisation de /β/ entre voyelles vélaires")
                    .Phono(px => new[] { "w" })
                    .Rewrite(_ => "w")));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c10r1d")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("β"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(
                    r => r
                        .Named("Renforcement de /β/ entre voyelles hétérogènes")
                        .Phono(px => new[] { "v" })
                        .Rewrite(_ => "v"),
                    r => r
                        .Named("Vélarisation de /β/ entre voyelles hétérogènes")
                        .Phono(px => new[] { "w" })
                        .Rewrite(_ => "w")));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c10r1e")
                .From(0).To(100)
                .Query(
                    q => q
                        .Before(b => b.Phon(p => IPA.IsVowel(p) && !IPA.IsVelarVowel(p)))
                        .Match(m => m.Phon("w"))
                        .After(b => b.Phon(IPA.IsPalatalVowel)),
                    q => q
                        .Before(b => b.Phon(IPA.IsPalatalVowel))
                        .Match(m => m.Phon("w"))
                        .After(a => a.Phon(p => IPA.IsVowel(p) && !IPA.IsVelarVowel(p))))
                .Rules(r => r
                    .Named("Renforcement de /w/ intervocalique avec entourage palatal")
                    .Phono(px => new[] { "β" })
                    .Rewrite(_ => "b")));
        }

        public static Rule Rule1f()
        {
            return R.Rule(c => c
                .Id("p1c10r1f")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("w"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Amuïssement de /w/ intervocalique avec entourage vélaire")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c10r2a")
                .From(350).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("p"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /p/ intervocalique")
                    .Phono(_ => new[] { "b" })
                    .Rewrite(_ => "b")));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c10r2b")
                .From(400).To(450)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVelarVowel))
                    .Match(m => m.Phon("b"))
                    .After(b => b.Phon(IPA.IsVelarVowel)))
                .Rules(r => r
                    .Named("Vélarisation de /b/ entre voyelles vélaires")
                    .Phono(_ => new[] { "w" })
                    .Rewrite(_ => "w")));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c10r2c")
                .From(400).To(450)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("b"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Spirantisation de /b/ entre voyelles mixtes")
                    .Phono(_ => new[] { "v" })
                    .Rewrite(_ => "v")));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c10r2d")
                .From(350).To(400)
                .Query(
                    q => q
                        .Before(b => b.Phon(p => IPA.IsVowel(p) && !IPA.IsVelarVowel(p)))
                        .Match(m => m.Phon("ɸ"))
                        .After(b => b.Phon(IPA.IsPalatalVowel)),
                    q => q
                        .Before(b => b.Phon(IPA.IsPalatalVowel))
                        .Match(m => m.Phon("ɸ"))
                        .After(a => a.Phon(p => IPA.IsVowel(p) && !IPA.IsVelarVowel(p))))
                .Rules(r => r
                    .Named("Sonorisation de /ɸ/ intervocalique avec entourage palatal")
                    .Phono(_ => new[] { "β" })
                    .Rewrite(_ => "b")));
        }

        public static Rule Rule2e()
        {
            return R.Rule(c => c
                .Id("p1c10r2e")
                .From(350).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("ɸ"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Vélarisation de /ɸ/ intervocalique")
                    .Phono(_ => new[] { "w" })
                    .Rewrite(_ => "w")));
        }

        public static Rule Rule2f()
        {
            return R.Rule(c => c
                .Id("p1c10r2f")
                .From(450).To(500)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("w"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Amuïssement de /w/ intervocalique")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }


        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c10r3a")
                .From(300).To(325)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("g"))
                    .After(b => b.Phon(IPA.IsVelarVowel)))
                .Rules(r => r
                    .Named("Spirantisation de /g/ intervocalique devant vélaire")
                    .Phono(_ => new[] { "ɣ" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c10r3b")
                .From(325).To(350)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("ɣ"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Vélarisation de /ɣ/ intervocalique")
                    .Phono(_ => new[] { "w" })
                    .Rewrite(_ => "w")));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c10r3c")
                .From(350).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("w"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Amuïssement de /w/ intervocalique")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c10r3d")
                .From(300).To(350)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("k"))
                    .After(b => b.Phon(IPA.IsVelarVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /k/ intervocalique devant vélaire")
                    .Phono(_ => new[] { "g" })
                    .Rewrite(_ => "g")));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c10r3e")
                .From(400).To(450)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("g"))
                    .After(b => b.Phon(IPA.IsVelarVowel)))
                .Rules(r => r
                    .Named("Spirantisation de /g/ intervocalique devant vélaire")
                    .Phono(_ => new[] { "ɣ" })));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c10r3f")
                .From(450).To(475)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("ɣ"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Vélarisation de /ɣ/ intervocalique")
                    .Phono(_ => new[] { "w" })
                    .Rewrite(_ => "w")));
        }

        public static Rule Rule3g()
        {
            return R.Rule(c => c
                .Id("p1c10r3g")
                .From(475).To(500)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("w"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Amuïssement de /w/ intervocalique")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c10r4a")
                .From(350).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("t"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /t/ intervocalique")
                    .Phono(_ => new[] { "d" })
                    .Rewrite(_ => "d")));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c10r4b")
                .From(500).To(600)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("d"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Spirantisation de /d/ intervocalique")
                    .Phono(_ => new[] { "ð" })));
        }

        public static Rule Rule4c()
        {
            return R.Rule(c => c
                .Id("p1c10r4c")
                .From(1000).To(1100)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("ð"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Amuïssement de /ð/ intervocalique")
                    .Phono(P.Erase)
                    .Rewrite(G.Erase)));
        }



        public static Rule Rule5a()
        {
            return R.Rule(c => c
                .Id("p1c10r5a")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("ɸ", "β"))
                    .After(b => b.Seq(
                        s => s.Phon("r"),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Renforcement en /vr/")
                    .Phono(_ => new[] { "v" })
                    .Rewrite(_ => "v")));
        }

        public static Rule Rule5b()
        {
            return R.Rule(c => c
                .Id("p1c10r5b")
                .From(300).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("t"))
                    .After(b => b.Seq(
                        s => s.Phon("r"),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Sonorisation de /t/")
                    .Phono(_ => new[] { "d" })
                    .Rewrite(_ => "d")));
        }

        public static Rule Rule5c()
        {
            return R.Rule(c => c
                .Id("p1c10r5c")
                .From(500).To(600)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("d"))
                    .After(b => b.Seq(
                        s => s.Phon("r"),
                        s => s.Phon(IPA.IsVowel))))
                .Rules(r => r
                    .Named("Spirantisation de /d/")
                    .Phono(_ => new[] { "ð" })));
        }

        public static Rule Rule5d()
        {
            return R.Rule(c => c
                .Id("p1c10r5d")
                .From(1000).To(1100)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(b => b.Seq(
                        s => s.Phon("ð"),
                        s => s.Phon("r")))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(
                    r => r
                        .Named("Assimilation de /ð/")
                        .Phono(_ => new[] { "r" })
                        .Rewrite(_ => "r"),
                    r => r
                        .Named("Assimilation de /ð/")
                        .Phono(_ => new[] { "r", "r" })
                        .Rewrite(_ => "r r")));
        }



        public static Rule Rule6()
        {
            return R.Rule(c => c
                .Id("p1c10r6")
                .From(300).To(400)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Phon("s"))
                    .After(b => b.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /s/ intervocalique")
                    .Phono(_ => new[] { "z" })));
        }
    }
}
