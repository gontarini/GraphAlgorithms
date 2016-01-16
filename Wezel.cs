using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Wezel
    {
        #region zmienne
        public int Id;
        public double etykieta;
        public int[] tablica_sasiadow;
        public int[] idLacza;
        public int liczba_sasiadow;
        #endregion
        public Wezel()
        {
            Id = 0;
            etykieta = 0;
            liczba_sasiadow = 1;
        }
        public Wezel(int id, double etyk)
        { 
            Id = id;
            etykieta = etyk;
            tablica_sasiadow = new int[100]; //korygować jeśli chcemy wczytywać duży graf
            idLacza = new int[100]; //
            liczba_sasiadow = 1;
        }
    }
}
