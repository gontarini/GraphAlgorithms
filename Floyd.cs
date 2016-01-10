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
                    if (i == j)
                    {
                        siec.macierz_sasiedztwa[i, j] = 0;
                        siec.macierz_poprzednikow[i, j] = i;
                    }
                    else
                        siec.macierz_sasiedztwa[i, j] = nieskonczonosc;
                            
                }

            for (int i = 1; i <= siec.liczbaKrawedzi; i++)
            {
                siec.macierz_sasiedztwa[lacza[i].wezelPoczatkowy, lacza[i].wezelKoncowy] = lacza[i].koszt;
                siec.macierz_sasiedztwa[lacza[i].wezelKoncowy,lacza[i].wezelPoczatkowy] = lacza[i].koszt;
                siec.macierz_poprzednikow[lacza[i].wezelPoczatkowy, lacza[i].wezelKoncowy] = lacza[i].wezelKoncowy;
                siec.macierz_poprzednikow[lacza[i].wezelKoncowy,lacza[i].wezelPoczatkowy] = lacza[i].wezelPoczatkowy;
            }
            
            for (int i = 1; i <= siec.liczbaWezlow; i++)
                for (int j = 1; j <= siec.liczbaWezlow; j++)
                    siec.macierz_najdluzszych_sciezek[i, j] = siec.macierz_sasiedztwa[i, j];

            //procedura wyliczajaca najdluzsze sciezki miedzy wezlami
            for(int k =1; k <= siec.liczbaWezlow;k++)
                for(int i = 1; i <= siec.liczbaWezlow;i++)
                    for(int j = 1; j <= siec.liczbaWezlow;j++)
                    {

                        if (siec.macierz_najdluzszych_sciezek[i, k] + siec.macierz_najdluzszych_sciezek[k, j] < siec.macierz_najdluzszych_sciezek[i, j] && siec.macierz_najdluzszych_sciezek[i, j]!= nieskonczonosc)
                        {
                            siec.macierz_najdluzszych_sciezek[i, j] = siec.macierz_najdluzszych_sciezek[i, k] + siec.macierz_najdluzszych_sciezek[k, j];
                            siec.macierz_poprzednikow[i, j] = k; //zmiana poprzednika
                        }
                    }
        }

        public void sciezkaFloyd()
        {
            string sciezka = "";
            for (int u = 1; u <= siec.liczbaWezlow; u++)
                for (int v = 1; v <= siec.liczbaWezlow; v++)
                {
                    if ((siec.macierz_poprzednikow[u, v] != 0))
                    {
                        sciezka = "(F) Ścieżka z " + u.ToString() + " do " + v.ToString() + ": " + u.ToString();
                        int next = u;
                        while ((next != v) && (next != 0))
                        {
                            next = siec.macierz_poprzednikow[next, v];
                            sciezka += "o" + next.ToString();
                        }
                        sciezki.Add(sciezka);
                    }
                }
        }
        public string sciezkaMiedzyWezlami(int wezelPocz, int wezelKoncowy, int czyyes)
        {
          string sciezka = "[Floyd]Najgrubsza sciezka z wierzcholka " + wezelPocz + " do wierzcholka " + wezelKoncowy + " : ";
          if (siec.macierz_poprzednikow[wezelPocz, wezelKoncowy] != 0)
          {
              sciezka = "(F) Ścieżka z " + wezelPocz.ToString() + " do " + wezelKoncowy.ToString() + ": " + wezelPocz.ToString();
              int next = wezelPocz;
              while ((next != wezelKoncowy) && (next != 0))
              {
                  next = siec.macierz_poprzednikow[next, wezelKoncowy];
                  sciezka += "o" + next.ToString();
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

            watchf.Stop();
            return (watchf.ElapsedMilliseconds);
        }
    }
}
