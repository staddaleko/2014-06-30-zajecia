using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrotkiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var wynik = from o in Osoba.Utworz() select new { Nazwa = o.Imie + " " + o.Nazwisko }; //Wypisać z listy osób imię, nazwisko oddzielone spacja i pole nazywa sie "nazwa".

            var wynik = Osoba.Utworz().Select(o => new { Nazwa = o.Imie + " " + o.Nazwisko }); //jest to alternatywny sposób uzyskania tej samej odpowiedzi co powyżej
            var i = 1;
            foreach (var o in wynik)
            {

                Console.WriteLine("{0} -- {1}", i++, o.Nazwa);
            }

            IEnumerable<Osoba> osoby = Osoba.Utworz();
            var os2 = from o in osoby where o.Rok > 1918 && o.Pensja > 3000 select o; //wszystkie osoby urodzone po roku 1918 i zarabiajace wiecej niz 3000.
            Console.WriteLine("\n*********** drugie zapytanie ************");
            foreach (var o in os2)
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("\n*********** trzecie zapytanie ************");

            var os2alt = osoby.Where(o => o.Rok > 1918 && o.Pensja > 3000).Select(p => p);//select jest tu niepotrzebny

            

            foreach (var oAlt in os2alt)
            {
                Console.WriteLine(oAlt);
            }
            Console.ReadKey();
        }
    }
    class Osoba
    {
        public string Imie { set; get; }
        public string Nazwisko { set; get; }
        public int Rok { set; get; }
        public decimal Pensja { set; get; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Imie, Nazwisko, Rok, Pensja);
        }

        public static List<Osoba> Utworz()
        {
            return new List<Osoba>()
            {
                new Osoba(){Imie="Bob", Nazwisko="Budownicz", Rok=1980, Pensja=4000},
                new Osoba(){Imie="Kubuś", Nazwisko="Puchatek", Rok=1936, Pensja=1000},
                new Osoba(){Imie="Kaczor", Nazwisko="Donald", Rok=1930, Pensja=3000},
                new Osoba(){Imie="Papa", Nazwisko="Smerf", Rok=1990, Pensja=5000},
                new Osoba(){Imie="Maja", Nazwisko="Pszczólka", Rok=1979, Pensja=7000},
                new Osoba(){Imie="Królewna", Nazwisko="Śnieżka", Rok=1910, Pensja=2000},
                new Osoba(){Imie="Jaga", Nazwisko="Baba", Rok=1780, Pensja=9000}
            };
        }
    }
}
