using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Tester
    {
        private Siec siec;
        private Lacze[] lacze;
        private Wezel[] wezly;
        private string sciezkaWejscia;
        private int liczbaWezlow;
        private int liczbaLaczy;

        public Tester() // na razie jest roboczo
        {
            string sciezka;
            siec = new Siec();
            wczytywanie();
            PoprawianieEtykiet poprawianie_etykiet = new PoprawianieEtykiet(liczbaWezlow);
            poprawianie_etykiet.siec = siec;
            poprawianie_etykiet.lacza = lacze;
            poprawianie_etykiet.wezly = wezly;
            sciezka = poprawianie_etykiet.wszystkieSciezki();
            Console.WriteLine(sciezka);
            Console.ReadLine();
            
        }
        public void wczytywanie()
        {
             
            bool ok = false;
            Random rnd = new Random();
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
