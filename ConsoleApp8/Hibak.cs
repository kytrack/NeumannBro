using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class Hibak
    {
        int szamjegy;
        int osszhibaszam;
        int gyakorisag;

        public Hibak(int szamjegy, int osszhibaszam, int gyakorisag)
        {
            this.Szamjegy = szamjegy;
            this.Osszhibaszam = osszhibaszam;
            this.Gyakorisag = gyakorisag;
        }

        public int Szamjegy { get => szamjegy; set => szamjegy = value; }
        public int Osszhibaszam { get => osszhibaszam; set => osszhibaszam = value; }
        public int Gyakorisag { get => gyakorisag; set => gyakorisag = value; }
    }
}
