using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Tester
    {
        #region zmienne
        private Siec siec;
        private Lacze[] lacze;
        private Wezel[] wezly;
        private string sciezkaWejscia;
        private int liczbaWezlow;
        private int liczbaLaczy;
        private int A;
        private double czasEtykiety;
        private double czasFloyd;
        #endregion
        public Tester()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            double losowa2;
            czasEtykiety = 0; czasFloyd = 0;
            bool okej = false;
            //string sciezka;
            while (okej != true)
            {
                try
                {
                    siec = new Siec();
                    wczytywanie();

                    #region inicjacje
                    //generacja macierzy poprzednikow
                    siec.macierz_poprzednikow = new int[siec.liczbaWezlow + 1, siec.liczbaWezlow + 1];
                    //generacja macierzy sasiedztwa
                    siec.macierz_sasiedztwa = new double[siec.liczbaWezlow + 1, siec.liczbaWezlow + 1];
                    siec.macierz_najdluzszych_sciezek = new double[siec.liczbaWezlow + 1, siec.liczbaWezlow + 1];
                    
                    PoprawianieEtykiet poprawianie_etykiet = new PoprawianieEtykiet(liczbaWezlow);
                    poprawianie_etykiet.siec = siec;
                    poprawianie_etykiet.lacza = lacze;
                    poprawianie_etykiet.wezly = wezly;

                    Floyd floyd = new Floyd();
                    floyd.siec = siec;
                    floyd.lacza = lacze;
                    floyd.wezly = wezly;
                    #endregion

                    Console.WriteLine("Podaj liczbę wykonań operacji znajdowania sciezek: [A]");
                    A = int.Parse(Console.ReadLine());
                    okej = true;

                    for (int i = 0 ; i < A; i++)
                    {
                        czasEtykiety += poprawianie_etykiet.sciezkaAll();
                        czasFloyd += floyd.sciezkaAll();

                        for(int j = 0; j < liczbaLaczy;j++) //randomizacja łaczy
                        {
                            losowa2 = rnd.NextDouble();
                            lacze[j + 1] = new Lacze(j+1, lacze[j+1].wezelPoczatkowy, lacze[j+1].wezelKoncowy, losowa2);
                        }

                    }
                }
                catch (Exception except)
                {
                    Console.WriteLine(except.Message);
                    okej = false;
                }
                Console.WriteLine("Laczny czas operacji Floyda: " + czasFloyd.ToString() + "ms");
                Console.WriteLine("Laczny czas operacji Etykiety: " + czasEtykiety.ToString() + "ms");
                Console.WriteLine("X: " + (czasFloyd / (double)A).ToString() + "ms");
                Console.WriteLine("Y: " + (czasEtykiety/ (double)A).ToString() + "ms");
            }
            Console.ReadLine();
        }
        public void wczytywanie()
        {
             
            bool ok = false;
            Random rnd = new Random(DateTime.Now.Millisecond);
            double losowa;
            while (!ok)
            {
                System.IO.StreamReader sr;
                string[] wyrazy;
                ok = true;
                try
                {
                    Console.WriteLine("Przeciagnij tu plik wejsciowy i wcisnij ENTER...");
                    sciezkaWejscia = Console.ReadLine();
                    if (sciezkaWejscia[0] == '\"') sciezkaWejscia = sciezkaWejscia.Substring(1, sciezkaWejscia.Length - 2);
                    Console.WriteLine(" ");
                    sr = new System.IO.StreamReader(sciezkaWejscia);
                    String linia = "";
                    #region wezly
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "WEZLY" && wyrazy[2] != "") { liczbaWezlow = int.Parse(wyrazy[2]); siec.liczbaWezlow = liczbaWezlow; }
                    else throw (new Exception("Zla liczba wezlow"));
                    #endregion
                    #region lacza
                    linia = "";
                    while (linia.Length < 2 || linia[0] == '#')
                    {
                        linia = sr.ReadLine();
                    }
                    wyrazy = linia.Split(' ');
                    if (wyrazy[0] == "LACZA" && wyrazy[2] != "") { liczbaLaczy = int.Parse(wyrazy[2]); siec.liczbaKrawedzi = liczbaLaczy; }
                    else throw (new Exception("Zla liczba pojemności kolejki"));
                    #endregion
         
                    #region wczytywanie konfiguracji laczy
                
                    lacze = new Lacze[liczbaLaczy + 1];
                    wezly = new Wezel[liczbaWezlow + 1];
                    for (int i = 1; i <=liczbaWezlow; i++)
                    {
                        wezly[i] = new Wezel(i, 0); // wezel[0] jest 0
                    }
                    wezly[0] = null;

                        for (int i = 0; i < liczbaLaczy; i++)
                        {
                            losowa = rnd.NextDouble();
                            linia = "";
                            while (linia.Length < 2 || linia[0] == '#')
                            {
                                linia = sr.ReadLine();
                            }
                            wyrazy = linia.Split(' ');
                            
                            lacze[i+1] = new Lacze(int.Parse(wyrazy[0]), int.Parse(wyrazy[1]),int.Parse(wyrazy[2]),losowa); // losowa - zmienna okreslajaca przepustowosc

                        }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zla sciezka. Sprobuj jeszcze raz.");
                    Console.WriteLine(e.Message);
                    ok = false;
                }
            }
            
        }
    }
}
