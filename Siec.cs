using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Siec
    {
        public int liczbaWezlow;
        public int liczbaKrawedzi;

        public int[] macierz_poprzednikow_etykiety;

        //tablice dla algorytmu Floyda
        public int[,] macierz_poprzednikow;
        public double[,] macierz_sasiedztwa;

        public Siec()
        {

        }
      
    }
}
