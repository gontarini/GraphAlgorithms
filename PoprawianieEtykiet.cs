using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AlgorytmyGrafowe
{
    class PoprawianieEtykiet:IWyznaczanieSciezek
    {
        #region zmienne i kolejkce
        private Queue<int> fifo;
        private List<string> sciezki;
        public Siec siec;
        private double[,] kosztSciezki;
        public Wezel[] wezly;
        public Lacze[] lacza;
        private double nieskonczonosc = Double.PositiveInfinity;
        private int wskaznik_ilosci_dostepnych_drog;
        #endregion

        public PoprawianieEtykiet(int liczbaWezlow)
        {
            wskaznik_ilosci_dostepnych_drog = 0;
            sciezki = new List<string>();
            kosztSciezki = new double[liczbaWezlow+1, liczbaWezlow+1];//indeksy [0] będą zerowe (taka konwencja)
        }
        public string sciezkaMiedzyWezlami(int wezelPocz, int wezelKoncowy, int ifyes) 
        {
            string sciezka2 = "";
            fifo = new Queue<int>();
            int wezelKonca, liczbaSasiadow, j = 1;
            if (ifyes == 0)
            siec.macierz_poprzednikow_etykiety = new int[siec.liczbaWezlow + 1];
            int id,tmp;
            string sciezka = "Najgrubsza sciezka z wierzcholka " + wezelPocz + " do wierzcholka " + wezelKoncowy + " : ";
            if (ifyes == 0)
            {
                #region Reset danych
                for (int i = 1; i <= siec.liczbaWezlow; i++) //Reset wezlow
                {
                    wezly[i].etykieta = 0;
                }
                #endregion

                wezly[wezelPocz].etykieta = nieskonczonosc;
                fifo.Enqueue(wezelPocz);

                while (fifo.Count != 0)// warunek konca poszukiwania najgrubszej sciezki 
                {
                    tmp = fifo.Dequeue();

                    liczbaSasiadow = wezly[tmp].liczba_sasiadow;

                    while (j != liczbaSasiadow) //dopóki istnieja sasiedzi  
                    {
                        wezelKonca = wezly[tmp].tablica_sasiadow[j];
                        id = wezly[tmp].idLacza[j];
                        if (wezly[wezelKonca].etykieta < Math.Min(lacza[id].koszt, wezly[tmp].etykieta) && wezly[wezelKonca].etykieta != nieskonczonosc) //Math.Min(lacza[id].koszt, wezly[tmp].etykieta)
                        {
                            wezly[wezelKonca].etykieta = Math.Min(lacza[id].koszt, wezly[tmp].etykieta);
                            fifo.Enqueue(wezelKonca);
                            siec.macierz_poprzednikow_etykiety[wezelKonca] = tmp;
                        }
                        j++;
                    }
                    j = 1;
                }
            }
            if (siec.macierz_poprzednikow_etykiety[wezelKoncowy] != 0)
            {
                int next = wezelKoncowy;
                while (next != 0)
                {
                    kosztSciezki[wezelPocz, wezelKoncowy] += wezly[next].etykieta;
                    sciezka2 += "" + next.ToString();
                    next = siec.macierz_poprzednikow_etykiety[next];
                    j++;
                }
             }
            for (int i = j - 2; i >= 0; i--)
                sciezka += sciezka2[i];
                sciezki.Add(sciezka2);
            return sciezka;
        }
        
        public string sciezkaMiedzyWszystkimiWezlami(int wezelPocz) //wszystkie sciezki od danego wezla poczatkowego do kazdego wezla
        {
            string sciezka = "";

            for (int i = 1; i <= siec.liczbaWezlow; i++)
            {
                if (i == 1)
                {
                    if (i != wezelPocz)
                        sciezka += sciezkaMiedzyWezlami(wezelPocz, i,0) + "\n";
                }
                else
                {
                    if (i != wezelPocz)
                        sciezka += sciezkaMiedzyWezlami(wezelPocz, i, 1) + "\n";
                }
            }
            return sciezka;
        }

        public string wszystkieSciezki()
        {
            string sciezka = "";
            int k;
            for (int i = 1; i <= siec.liczbaWezlow; i++)
            {
                k = 2; //zmienna ograniczająca ilość rozwiązywań grafu
                for (int j = 1; j <= siec.liczbaWezlow; j++)
                {
                    if (i != j)
                    {
                        if (i == 1 && j == 2)
                        {
                            sciezka += sciezkaMiedzyWezlami(i, j, 0) + "\n";
                            k++;
                        }
                        else if (k % 2 == 0)
                        {
                            sciezka += sciezkaMiedzyWezlami(i, j, 0) + "\n";
                            k++;
                        }
                        else
                            sciezka += sciezkaMiedzyWezlami(i, j, 1) + "\n";
                    }
                }
            }
            return sciezka;
        }

        public double sciezkaAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            wszystkieSciezki();
            stopwatch.Stop();
            return (stopwatch.ElapsedMilliseconds);

        }
    }
}
