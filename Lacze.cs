using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Lacze
    {
        public int idLacza;
        public int wezelPoczatkowy;
        public int wezelKoncowy;
        public double koszt;

        public Lacze(int Id, int wPocz, int wKonc, double kossst)
        {
            wezelPoczatkowy = wPocz;
            wezelKoncowy = wKonc;
            idLacza = Id;
            koszt = kossst;
        }     

    }
}
