using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Nje program i perpiluar*/
namespace Beaufort_Cipher
{
    class Beaufort
    {
        public static string Alfabeti { get; set; }

        private static string zhvendosja(char shkronja)
        {

            string reversi = "";
            int gjatesia = 0;

            gjatesia = Alfabeti.Length - 1;
            while (gjatesia >= 0)
            {
                reversi = reversi + Alfabeti[gjatesia];
                gjatesia--;
            }

            int pozicioni = reversi.IndexOf(shkronja);
            return reversi.Remove(0, pozicioni) + reversi.Remove(pozicioni, 26 - pozicioni);
        }
        private static string zvogloCelesin(string celesi)
        {
            for (int i = 1; i < celesi.Length; i++)
            {
                string celesiZvogluar = celesi.Remove(i);
                string krahasimi = "";

                while (krahasimi.Length < celesi.Length)
                {
                    krahasimi += celesiZvogluar;
                }
                if (krahasimi.Length > celesi.Length)
                    krahasimi = krahasimi.Remove(celesi.Length);

                if (krahasimi == celesi) return celesiZvogluar;
            }

            return celesi;
        }
        public static string Encode(string hyrja, string celesi, bool reversi)
        {
            string rezultati = "";


            while (celesi.Length < hyrja.Length) celesi += celesi;
            if (celesi.Length > hyrja.Length) celesi.Remove(hyrja.Length);


            for (int i = 0; i < hyrja.Length; i++)
            {
                string shiftedAlphabet = zhvendosja(celesi[i]);

                int position = (!reversi) ? Alfabeti.IndexOf(hyrja[i]) : shiftedAlphabet.IndexOf(hyrja[i]);
                rezultati += (!reversi) ? shiftedAlphabet[position] : Alfabeti[position];
            }

            return rezultati;
        }
    }
}
