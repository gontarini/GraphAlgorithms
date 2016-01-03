using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyGrafowe
{
    class Wezel
    {
        public int Id;
        public double etykieta;
        public bool odwiedzone;
        
        public Wezel()
        {
            Id = 0;
            etykieta = 0;
            odwiedzone = false;
        }
        public Wezel(int id, double etyk)
        { 
            Id = id;
            etykieta = etyk;
            odwiedzone = false;
        }
    }
}
