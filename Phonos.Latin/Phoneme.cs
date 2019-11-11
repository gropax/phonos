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
        public static string a = "a";
        public static string LONG_a = "aː";
        public static string b = "b";
        public static string d = "d";
        public static string e = "e";
        public static string LONG_e = "eː";
        public static string f = "f";
        public static string g = "g";
        public static string gw = "gʷ";
        public static string h = "h";
        public static string i = "i";
        public static string LONG_i = "iː";
        public static string j = "j";
        public static string k = "k";
        public static string kh = "kʰ";
        public static string kw = "kʷ";
        public static string l = "l";
        public static string m = "m";
        public static string n = "n";
        public static string ng = "ŋ";
        public static string o = "o";
        public static string LONG_o = "oː";
        public static string p = "p";
        public static string ph = "pʰ";
        public static string r = "r";
        public static string s = "s";
        public static string t = "t";
        public static string th = "tʰ";
        public static string u = "u";
        public static string LONG_u = "uː";
        public static string v = "v";
        public static string w = "w";
        public static string y = "y";
        public static string LONG_y = "yː";
        public static string z = "z";

        public static string a_i = "ai̯";
        public static string a_u = "au̯";
        public static string e_i = "ei̯";
        public static string e_u = "eu̯";
        public static string o_i = "oi̯";
        public static string u_i = "ui̯";
        public static string y_i = "yi̯";
    }
}
