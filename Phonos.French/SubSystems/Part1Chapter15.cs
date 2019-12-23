﻿using Phonos.Core;
using Phonos.Core.RuleBuilder;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.French.SubSystems
{
    public static class Part1Chapter15
    {
        public static IRule[] Rules()
        {
            return RuleComponents();
            //return new IRule[] { };
        }

        public static IRule[] RuleComponents()
        {
            return new[]
            {
                Rule1a(), Rule1b(), Rule1c(), Rule1d(), Rule1e(), Rule1f(),
                Rule2a(), Rule2b(), Rule2c(), Rule2d(),
                Rule3a(), Rule3b(), Rule3c(), Rule3d(), Rule3e(), Rule3f(), Rule3g(), Rule3h(), Rule3i(), Rule3j(), Rule3k(),
                Rule4a(), Rule4b(), Rule4c(), Rule4d(),
                //Rule5a(), Rule5b(), Rule5c(), Rule5d(), Rule5e(),
                //Rule6a(), Rule6b(), Rule6c(), Rule6d(), Rule6e(), Rule6f(), Rule6g(), Rule6h(), Rule6i(),
            };
        }

        public static Rule Rule1a()
        {
            return R.Rule(c => c
                .Id("p1c15r1a")
                .From(600).To(700)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("j"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Dégémination de /jj/ intervocalique")
                    .Phono(px => new[] { px[0] })));
        }

        public static Rule Rule1b()
        {
            return R.Rule(c => c
                .Id("p1c15r1b")
                .From(800).To(900)
                .Query(q => q
                    .Scope("syllable")
                    .Match(m =>
                        m.Seq(
                            s => s.Phon(IPA.IsVowel),
                            s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Vocalisation de /j/ et formation d'une diphtongue de coalescence")
                    .Phono(px => new[] { px[0] + "i̯" })));
        }

        public static Rule Rule1c()
        {
            return R.Rule(c => c
                .Id("p1c15r1c")
                .From(200).To(250)
                .Query(q => q
                    .Before(Q.Start)
                    .Match(m => m.Phon("j")))
                .Rules(r => r
                    .Named("Palatalisation de /j/ initial")
                    .Phono(px => new[] { "dʲ" })));
        }

        public static Rule Rule1d()
        {
            return R.Rule(c => c
                .Id("p1c15r1d")
                .From(250).To(300)
                .Query(q => q .Match(m => m.Phon("dʲ")))
                .Rules(r => r
                    .Named("Assibilation de /dʲ/")
                    .Phono(px => new[] { "ʤʲ" })));
        }

        public static Rule Rule1e()
        {
            return R.Rule(c => c
                .Id("p1c15r1e")
                .From(600).To(700)
                .Query(q => q.Match(m => m.Phon("ʤʲ")))
                .Rules(r => r
                    .Named("Dépalatalisation de /ʤʲ/")
                    .Phono(px => new[] { "ʤ" })));
        }

        public static Rule Rule1f()
        {
            return R.Rule(c => c
                .Id("p1c15r1f")
                .From(1200).To(1300)
                .Query(q => q.Match(m => m.Phon("ʤ")))
                .Rules(r => r
                    .Named("Réduction de /ʤ/")
                    .Phono(px => new[] { "ʒ" })
                    .Rewrite(g =>
                    {
                        if (g.StartsWith("g"))
                            return "ge";
                        else
                            return "j";
                    })));
        }



        public static Rule Rule2a()
        {
            return R.Rule(c => c
                .Id("p1c15r2a")
                .From(0).To(100)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("d", "g"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Assimilation de /dj/ et /gj/ intervocalique")
                    .Phono(px => new[] { "j", "j" })
                    .Rewrite(g => "i")));
        }

        public static Rule Rule2b()
        {
            return R.Rule(c => c
                .Id("p1c15r2b")
                .From(200).To(250)
                .Query(q => q
                    .Scope("syllable")
                    .Before(Q.Start)
                    .Match(m => m.Seq(s => s.Phon("d"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Palatalisation de /dj/ en position forte")
                    .Phono(px => new[] { "dʲ" })));
        }

        public static Rule Rule2c()
        {
            return R.Rule(c => c
                .Id("p1c15r2c")
                .From(200).To(225)
                .Query(q => q
                    .Scope("syllable")
                    .Before(Q.Start)
                    .Match(m => m.Seq(s => s.Phon("g"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Palatalisation de /gj/ en position forte")
                    .Phono(px => new[] { "gʲ" })));
        }

        public static Rule Rule2d()
        {
            return R.Rule(c => c
                .Id("p1c15r2d")
                .From(225).To(250)
                .Query(q => q.Match(m => m.Phon("gʲ")))
                .Rules(r => r
                    .Named("Dentalisation de /gʲ/")
                    .Phono(px => new[] { "dʲ" })));
        }



        public static Rule Rule3a()
        {
            return R.Rule(c => c
                .Id("p1c15r3a")
                .From(100).To(150)
                .Query(q => q
                    .Scope("syllable")
                    .Before(Q.Start)
                    .Match(m => m.Seq(s => s.Phon("t"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Palatalisation de /tj/ en position forte")
                    .Phono(px => new[] { "tʲ" })));
        }

        public static Rule Rule3b()
        {
            return R.Rule(c => c
                .Id("p1c15r3b")
                .From(150).To(200)
                .Query(q => q
                    .Match(m => m.Phon("tʲ")))
                .Rules(r => r
                    .Named("Assibilation de /tʲ/")
                    .Phono(px => new[] { "ʦʲ" })
                    .Rewrite(_ => "tsi")));
        }

        public static Rule Rule3c()
        {
            return R.Rule(c => c
                .Id("p1c15r3c")
                .From(600).To(700)
                .Query(q => q
                    .Match(m => m.Phon("ʦʲ")))
                .Rules(r => r
                    .Named("Dépalatalisation de /ʦʲ/")
                    .Phono(px => new[] { "ʦ" })
                    .Rewrite(_ => "ts")));
        }

        public static Rule Rule3d()
        {
            return R.Rule(c => c
                .Id("p1c15r3d")
                .From(100).To(125)
                .Query(q => q
                    .Scope("syllable")
                    .Before(Q.Start)
                    .Match(m => m.Seq(s => s.Phon("k"), s => s.Phon("j"))))
                .Rules(r => r
                    .Named("Palatalisation de /kj/ en position forte")
                    .Phono(px => new[] { "kʲ" })));
        }

        public static Rule Rule3e()
        {
            return R.Rule(c => c
                .Id("p1c15r3e")
                .From(125).To(150)
                .Query(q => q
                    .Match(m => m.Phon("kʲ")))
                .Rules(r => r
                    .Named("Dentalisation de /kʲ/")
                    .Phono(px => new[] { "tʲ" })));
        }

        public static Rule Rule3f()
        {
            return R.Rule(c => c
                .Id("p1c15r3f")
                .From(100).To(150)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("t"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Palatalisation de /tj/ intervocalique")
                    .Phono(px => new[] { "j", "tʲ" })
                    .Rewrite(_ => "iti")));
        }

        public static Rule Rule3g()
        {
            return R.Rule(c => c
                .Id("p1c15r3g")
                .From(150).To(200)
                .Query(q => q
                    .Before(b => b.Phon("j"))
                    .Match(m => m.Phon("tʲ"))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Assibilation de /tʲ/ intervocalique")
                    .Phono(px => new[] { "ʦʲ" })
                    .Rewrite(_ => "tsi")));
        }

        public static Rule Rule3h()
        {
            return R.Rule(c => c
                .Id("p1c15r3h")
                .From(300).To(400)
                .Query(q => q
                    .Before(b => b.Phon("j"))
                    .Match(m => m.Phon("ʦʲ"))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /ʦʲ/ intervocalique")
                    .Phono(px => new[] { "ʣʲ" })
                    .Rewrite(_ => "dzi")));
        }

        public static Rule Rule3i()
        {
            return R.Rule(c => c
                .Id("p1c15r3i")
                .From(600).To(700)
                .Query(q => q
                    .Before(b => b.Phon("j"))
                    .Match(m => m.Phon("ʣʲ")))
                .Rules(r => r
                    .Named("Dépalatalisation de /ʣʲ/ intervocalique")
                    .Phono(px => new[] { "ʣ" })
                    .Rewrite(_ => "dz")));
        }

        public static Rule Rule3j()
        {
            return R.Rule(c => c
                .Id("p1c15r3j")
                .From(100).To(125)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("k"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Palatalisation et gémination de /kj/ intervocalique")
                    .Phono(px => new[] { "k", "kʲ" })));
        }

        public static Rule Rule3k()
        {
            return R.Rule(c => c
                .Id("p1c15r3k")
                .From(125).To(150)
                .Query(q => q
                    .Match(m => m.Seq(s => s.Phon("k"), s => s.Phon("kʲ"))))
                .Rules(r => r
                    .Named("Dentalisation de /kkʲ/")
                    .Phono(px => new[] { "t", "tʲ" })));
        }



        public static Rule Rule4a()
        {
            return R.Rule(c => c
                .Id("p1c15r4a")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("s"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Palatalisation de /sj/ intervocalique")
                    .Phono(px => new[] { "j", "sʲ" })
                    .Rewrite(_ => "isi")));
        }

        public static Rule Rule4b()
        {
            return R.Rule(c => c
                .Id("p1c15r4b")
                .From(300).To(400)
                .Query(q => q
                    .Before(b => b.Phon("j"))
                    .Match(m => m.Phon("sʲ"))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Sonorisation de /sʲ/ intervocalique")
                    .Phono(px => new[] { "zʲ" })));
        }

        public static Rule Rule4c()
        {
            return R.Rule(c => c
                .Id("p1c15r4c")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("s"), s => s.Phon("s"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Palatalisation de /ssj/ intervocalique")
                    .Phono(px => new[] { "j", "s", "sʲ" })
                    .Rewrite(_ => "issi")));
        }

        public static Rule Rule4d()
        {
            return R.Rule(c => c
                .Id("p1c15r4d")
                .From(200).To(300)
                .Query(q => q
                    .Before(b => b.Phon(IPA.IsVowel))
                    .Match(m => m.Seq(s => s.Phon("r"), s => s.Phon("j")))
                    .After(a => a.Phon(IPA.IsVowel)))
                .Rules(r => r
                    .Named("Palatalisation de /rj/ intervocalique")
                    .Phono(px => new[] { "j", "rʲ" })
                    .Rewrite(_ => "iri")));
        }
    }
}
