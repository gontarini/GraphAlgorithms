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
            Lacze[] lacza_current;
            fifo = new Queue<int>();
            int wezelKonca,wezelPoczatku;
            if(ifyes == 0)
            siec.tablica_wierzcholkow = new int[siec.liczbaWezlow+1, siec.liczbaKrawedzi + 1];
            int tmp,tmp2=0;
            bool czyokej = true;
            string sciezka = "Najgrubsza sciezka z wierzcholka " + wezelPocz + " do wierzcholka " + wezelKoncowy + " : ";
            #region Reset danych
            for (int i = 1; i <= siec.liczbaWezlow; i++) //Reset wezlow
            {
                wezly[i].etykieta = 0;
                wezly[i].odwiedzone = false;
            }

                siec.liczba_odwiedzonych_wezlow = 0;
            #endregion

            wezly[wezelPocz].etykieta = nieskonczonosc;
            fifo.Enqueue(wezelPocz);
            
            while(fifo.Count != 0 && wezly[wezelKoncowy].odwiedzone == false) // warunek konca poszukiwania najgrubszej sciezki 
            {
                czyokej = true;
                tmp = fifo.Dequeue();
                wezly[tmp].odwiedzone = true;
                if (tmp == tmp2)
                    czyokej = false;

                
                if (czyokej == true)
                {
                    lacza_current = zwracamPolaczenia(tmp); //zwracam możliwe drogi z danego wezla do innych   
                    lacza_current = sortowanie(lacza_current); //sortowanie w celu sprawdzania najpierw najgrubszych polaczen

                    while (wskaznik_ilosci_dostepnych_drog != 1) //dopóki istnieja zwrócone wcześniej drogi, zmieniam etykiety     
                    {
                        wezelKonca = lacza_current[wskaznik_ilosci_dostepnych_drog - 1].wezelKoncowy;
                        wezelPoczatku = lacza_current[wskaznik_ilosci_dostepnych_drog - 1].wezelPoczatkowy;

                        if (tmp != wezelPoczatku) //korygowanie wezla poczatkowego
                        {
                            tmp2 = wezelPoczatku;
                            wezelPoczatku = wezelKonca;
                            wezelKonca = tmp2;
                        }
                        if (wezly[wezelKonca].etykieta <= Math.Min(lacza_current[wskaznik_ilosci_dostepnych_drog - 1].koszt, wezly[wezelPoczatku].etykieta))
                        {
                            wezly[wezelKonca].etykieta = Math.Min(lacza_current[wskaznik_ilosci_dostepnych_drog - 1].koszt, wezly[wezelPoczatku].etykieta);
                            fifo.Enqueue(wezelKonca);
                        }
                        wskaznik_ilosci_dostepnych_drog--;
                    }
                    siec.liczba_odwiedzonych_wezlow++; ///zahaszowane, bo się program sypie, spróbować rozwiązać sprawę powtarzających się wezlow sciezki
                    tmp2 = tmp;
                    //siec.tablica_wierzcholkow[wezelPocz, siec.liczba_odwiedzonych_wezlow] = tmp;
                    //if (tmp != siec.tablica_wierzcholkow[wezelPocz, siec.liczba_odwiedzonych_wezlow - 1])
                    sciezka += tmp + " ";
                    if (fifo.Count() != 0)
                        kosztSciezki[wezelPocz, wezelKoncowy] += wezly[fifo.Peek()].etykieta;
                    else
                        kosztSciezki[wezelPocz, wezelKoncowy] += wezly[wezelKoncowy].etykieta;
                }
            }            
            sciezki.Add(sciezka);
            return sciezka;
        }

        private Lacze[] sortowanie(Lacze[] lacze)
        {
            int j = 1;
            while (j != wskaznik_ilosci_dostepnych_drog - 1)
            {
                j++;
                for (int i = 1; i < wskaznik_ilosci_dostepnych_drog - 1; i++)
                {
                    if (lacze[i].koszt.CompareTo(lacze[i + 1].koszt) > 0)
                    {
                        Lacze temp;
                        temp = lacze[i + 1];
                        lacze[i + 1] = lacze[i];
                        lacze[i] = temp;
                    }
                }
            }

            return lacze;
        } //sortuje wagi laczy, zeby zaczynac zawsze od najgrubszej (mysle, ze to ma znaczenie w FIFO)
        private Lacze[] zwracamPolaczenia(int indeksPoczatku)
        {
            int j = 1;
            Lacze[] polaczenia_z_poczatkowym = new Lacze[siec.liczbaKrawedzi + 1];
            for(int i = 0; i < siec.liczbaKrawedzi; i++)
            {
                if(lacza[i + 1].wezelPoczatkowy == indeksPoczatku || lacza[i+1].wezelKoncowy == indeksPoczatku) // tu byla zmiana
                {
                    polaczenia_z_poczatkowym[j] = lacza[i + 1];
                    j++;
                }
            }
            wskaznik_ilosci_dostepnych_drog = j;
            return polaczenia_z_poczatkowym;
        } //funkcja odpowiadajaca za zwracanie polaczen danego wezla z pozostalymi
        
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

            for (int i = 1; i <= siec.liczbaWezlow; i++)
            {
                for (int j = 1; j <= siec.liczbaWezlow; j++)
                {
                    if (i != j)
                    {
                        if (i == 1 && j == 2)
                            sciezka += sciezkaMiedzyWezlami(i, j, 0) + "\n";
                        else
                            sciezka += sciezkaMiedzyWezlami(i, j, 1) + "\n";
          
                    }
                }
            }
            return sciezka;
        }

        public double sciezkaAll()
        {
            //string sciezka = "";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            wszystkieSciezki();
            //Console.WriteLine(sciezka);
            stopwatch.Stop();
            return (stopwatch.ElapsedMilliseconds);

        }
    }
}
