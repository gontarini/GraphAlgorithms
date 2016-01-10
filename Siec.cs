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
        public int liczba_odwiedzonych_wezlow;

        
        public int[,] macierz_poprzednikow;
        public double[,] macierz_sasiedztwa;
        public double[,] macierz_najdluzszych_sciezek;

        public int[,] tablica_wierzcholkow;

        public Siec()
        {

        }
      
    }
}
