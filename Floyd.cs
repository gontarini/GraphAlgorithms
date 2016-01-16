using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AlgorytmyGrafowe
{
    class Floyd:IWyznaczanieSciezek
    {
        #region zmienne i kolekcje
        private List<string> sciezki;
        public Siec siec;
        public Wezel[] wezly;
        public Lacze[] lacza;
        private double nieskonczonosc = Double.PositiveInfinity;

        #endregion
        public Floyd()
        {
            sciezki = new List<string>();
        }
        public void generacjaFloyda()
        {

            for(int i =1;i<=siec.liczbaWezlow;i++)
                for(int j = 1; j<= siec.liczbaWezlow;j++)
                {
                   siec.macierz_sasiedztwa[i, j] = nieskonczonosc;        
                }

            for (int i = 1; i <= siec.liczbaKrawedzi; i++)
            {
                siec.macierz_sasiedztwa[lacza[i].wezelPoczatkowy, lacza[i].wezelKoncowy] = lacza[i].koszt;
                siec.macierz_poprzednikow[lacza[i].wezelPoczatkowy, lacza[i].wezelKoncowy] = lacza[i].wezelPoczatkowy;
            }

            //procedura wyliczajaca najdluzsze sciezki miedzy wezlami
            for(int k =1; k <= siec.liczbaWezlow;k++)
                for(int i = 1; i <= siec.liczbaWezlow;i++)
                    for(int j = 1; j <= siec.liczbaWezlow;j++)
                    {

                        if (siec.macierz_sasiedztwa[i, k] + siec.macierz_sasiedztwa[k, j] < siec.macierz_sasiedztwa[i, j])// && siec.macierz_najdluzszych_sciezek[i, j]!= nieskonczonosc)
                        {
                            if (i != j)
                            {
                                siec.macierz_sasiedztwa[i, j] = siec.macierz_sasiedztwa[i, k] + siec.macierz_sasiedztwa[k, j];
                                siec.macierz_poprzednikow[i, j] = siec.macierz_poprzednikow[k, j]; //zmiana poprzednika
                            }
                        }
                    }
        }

        public void sciezkaFloyd()
        {
            int next2, j =1;
            string sciezka = "",sciezka2 ="";
            for (int u = 1; u <= siec.liczbaWezlow; u++)
                for (int v = 1; v <= siec.liczbaWezlow; v++)
                {
                    j = 1;
                    sciezka2 = "";
                    if ((siec.macierz_poprzednikow[u, v] != 0))
                    {
                        next2 = v;
                        sciezka = "(F) Ścieżka z " + u.ToString() + " do " + v.ToString() + ": ";
                        while (siec.macierz_poprzednikow[u, next2] != 0)
                        {
                            sciezka2 += next2.ToString();
                            next2 = siec.macierz_poprzednikow[u, next2];
                            j++;                   
                        }

                        sciezka2 += u.ToString();
                    }
                    if (j > 1)
                    {
                        for (int i = j - 1; i >= 0; i--)
                            sciezka += sciezka2[i];

                        sciezki.Add(sciezka2);
                    }
                }
        }
        public string sciezkaMiedzyWezlami(int wezelPocz, int wezelKoncowy, int czyyes)
        {
          int j = 1;
          string sciezka2 = "";
          string sciezka = "";
          if (siec.macierz_poprzednikow[wezelPocz, wezelKoncowy] != 0)
          {
              sciezka = "(F) Ścieżka z " + wezelPocz.ToString() + " do " + wezelKoncowy.ToString() + ": ";
              int next = wezelKoncowy;
              while (siec.macierz_poprzednikow[wezelPocz,next]!=0)
              {
                  sciezka2 += next.ToString();
                  next = siec.macierz_poprzednikow[wezelPocz, next];
                  j++;
              }
              sciezka2 += wezelPocz;

              if (j > 1)
              {
                  for (int i = j - 1; i >= 0; i--)
                      sciezka += sciezka2[i];
              }
          }
          return sciezka;
        }
        public string sciezkaMiedzyWszystkimiWezlami(int wezelPocz)
        {
            string sciezka = "";
            for (int k = 1; k <= siec.liczbaWezlow; k++)
            {
                sciezka += sciezkaMiedzyWezlami(wezelPocz, k,0) + "\n";
            }
            return sciezka;
        }
        public double sciezkaAll()
        {
            Stopwatch watchf = new Stopwatch();
            watchf.Start();

            generacjaFloyda();
            sciezkaFloyd();

            //Console.WriteLine(sciezkaMiedzyWszystkimiWezlami(1));
            //foreach (string s in sciezki)
            //    Console.WriteLine(s);

            watchf.Stop();
            return (watchf.ElapsedMilliseconds);
        }
    }
}
