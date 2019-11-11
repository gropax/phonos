using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Latin
{
    public enum PhonemeType
    {
        CONSONANTIC,
        SEMI_CONSONANTIC,
        VOCALIC,
    }

    public enum PhonemeQuantity
    {
        SHORT,
        LONG
    }

    public class Phoneme
    {
        public string Quality { get; private set; }
        public PhonemeType Type { get; private set; }
        public PhonemeQuantity Quantity { get; private set; }

        public Phoneme(string quality, PhonemeType type = PhonemeType.CONSONANTIC, PhonemeQuantity quantity = PhonemeQuantity.SHORT)
        {
            Quality = quality;
            Type = type;
            Quantity = quantity;
        }
    }


    public static class Phonemes
    {
        public static Phoneme a = new Phoneme("a", PhonemeType.VOCALIC);
        public static Phoneme LONG_a = new Phoneme("a", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme b = new Phoneme("b");
        public static Phoneme d = new Phoneme("d");
        public static Phoneme e = new Phoneme("e", PhonemeType.VOCALIC);
        public static Phoneme LONG_e = new Phoneme("e", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme f = new Phoneme("f");
        public static Phoneme g = new Phoneme("g");
        public static Phoneme gw = new Phoneme("gʷ");
        public static Phoneme h = new Phoneme("h");
        public static Phoneme i = new Phoneme("i", PhonemeType.VOCALIC);
        public static Phoneme LONG_i = new Phoneme("i", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme j = new Phoneme("j", PhonemeType.SEMI_CONSONANTIC);
        public static Phoneme k = new Phoneme("k");
        public static Phoneme kh = new Phoneme("kʰ");
        public static Phoneme kw = new Phoneme("kʷ");
        public static Phoneme l = new Phoneme("l");
        public static Phoneme m = new Phoneme("m");
        public static Phoneme n = new Phoneme("n");
        public static Phoneme ng = new Phoneme("ŋ");
        public static Phoneme o = new Phoneme("o", PhonemeType.VOCALIC);
        public static Phoneme LONG_o = new Phoneme("o", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme p = new Phoneme("p");
        public static Phoneme ph = new Phoneme("pʰ");
        public static Phoneme r = new Phoneme("r");
        public static Phoneme s = new Phoneme("s");
        public static Phoneme t = new Phoneme("t");
        public static Phoneme th = new Phoneme("tʰ");
        public static Phoneme u = new Phoneme("u", PhonemeType.VOCALIC);
        public static Phoneme LONG_u = new Phoneme("u", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme v = new Phoneme("v");
        public static Phoneme w = new Phoneme("w", PhonemeType.SEMI_CONSONANTIC);
        public static Phoneme y = new Phoneme("y", PhonemeType.VOCALIC);
        public static Phoneme LONG_y = new Phoneme("y", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme z = new Phoneme("z");

        public static Phoneme a_i = new Phoneme("ai̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme a_u = new Phoneme("au̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme e_i = new Phoneme("ei̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme e_u = new Phoneme("eu̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme o_i = new Phoneme("oi̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme u_i = new Phoneme("ui̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
        public static Phoneme y_i = new Phoneme("yi̯", PhonemeType.VOCALIC, PhonemeQuantity.LONG);
    }
}
