using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    interface IWyznaczanieSciezek
    {
        string sciezkaMiedzyWezlami(int wezelPocz, int wezelKoncowy, int czyyes);
        string sciezkaMiedzyWszystkimiWezlami(int wezelPocz);
        double sciezkaAll();
    }
}
