﻿using System;
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
            ZapytaniePierwsze();
            IEnumerable<Osoba> osoby = Osoba.Utworz();
            ZapytanieDrugie(osoby);
            ZapytanieTrzecie(osoby);
            ZapytanieCzwarte(osoby);
            ZapytaniePiate(osoby);

            ZapytanieSzoste(osoby);

            Console.ReadKey();
        }

        private static void ZapytanieSzoste(IEnumerable<Osoba> osoby)
        {
            Console.WriteLine("\n*********** szóste zapytanie (po dużych zmianach w bazach danych) ************");
            var zawody = Zawod.Utworz();
            var os5 = from o in osoby join z in zawody on o.IdZawodu equals z.IdZawodu select new { o.Imie, o.Nazwisko, z.Nazwa }; //typowy inner join
            foreach (var o in os5)
            {
                Console.WriteLine("{0} {1} -- zawód: {2}", o.Imie, o.Nazwisko, o.Nazwa);
            }
        }


        private static void ZapytaniePiate(IEnumerable<Osoba> osoby)
        {
            int nrStrony = 2, ilosc = 2; //pominelismy dwa pierwsze wpisy, ilosc wpisow na stronie = 2
            var os4 = osoby.Skip((nrStrony - 1) * ilosc).Take(ilosc);//pomijamy pierwsza strone
            Console.WriteLine("\n*********** piate zapytanie ************");
            foreach (var o in os4)
            {
                Console.WriteLine(o);
            }
        }
        private static void ZapytanieCzwarte(IEnumerable<Osoba> osoby)
        {
            var os3alt = osoby.Where(o => o.Rok > 1918 && o.Pensja > 3000).OrderByDescending(p => p.Pensja).ThenByDescending(p => p.Imie);//sortujemy po pensji i pozniej po imieniu
            Console.WriteLine("\n*********** czwarte zapytanie ************");

            foreach (var o in os3alt)
            {
                Console.WriteLine(o);
            }
        }
        private static void ZapytanieTrzecie(IEnumerable<Osoba> osoby)
        {
            Console.WriteLine("\n*********** trzecie zapytanie ************");

            var os2alt = osoby.Where(o => o.Rok > 1918 && o.Pensja > 3000).Select(p => p);//select jest tu niepotrzebny 

            foreach (var oAlt in os2alt)
            {
                Console.WriteLine(oAlt);
            }
        }
        private static void ZapytanieDrugie(IEnumerable<Osoba> osoby)
        {
            var os2 = from o in osoby where o.Rok > 1918 && o.Pensja > 3000 select o; //wszystkie osoby urodzone po roku 1918 i zarabiajace wiecej niz 3000.
            Console.WriteLine("\n*********** drugie zapytanie ************");
            foreach (var o in os2)
            {
                Console.WriteLine(o);
            }
        }
        private static void ZapytaniePierwsze()
        {
            //var wynik = from o in Osoba.Utworz() select new { Nazwa = o.Imie + " " + o.Nazwisko }; //Wypisać z listy osób imię, nazwisko oddzielone spacja i pole nazywa sie "nazwa".

            var wynik = Osoba.Utworz().Select(o => new { Nazwa = o.Imie + " " + o.Nazwisko }); //jest to alternatywny sposób uzyskania tej samej odpowiedzi co powyżej
            var i = 1;
            foreach (var o in wynik)
            {

                Console.WriteLine("{0} -- {1}", i++, o.Nazwa);
            }
        }
    }
    class Osoba
    {
        public string Imie { set; get; }
        public string Nazwisko { set; get; }
        public int Rok { set; get; }
        public decimal Pensja { set; get; }
        public int? IdZawodu { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Imie, Nazwisko, Rok, Pensja);
        }

        public static List<Osoba> Utworz()
        {
            return new List<Osoba>()
            {
                new Osoba(){Imie="Bob", Nazwisko="Budownicz", Rok=1980, Pensja=4000, IdZawodu=null},
                new Osoba(){Imie="Kubuś", Nazwisko="Puchatek", Rok=1936, Pensja=1000, IdZawodu=4},
                new Osoba(){Imie="Kaczor", Nazwisko="Donald", Rok=1930, Pensja=3000, IdZawodu=4},
                new Osoba(){Imie="Papa", Nazwisko="Smerf", Rok=1990, Pensja=5000, IdZawodu=4},
                new Osoba(){Imie="Maja", Nazwisko="Pszczólka", Rok=1979, Pensja=7000, IdZawodu=3},
                new Osoba(){Imie="Królewna", Nazwisko="Śnieżka", Rok=1910, Pensja=2000, IdZawodu=1},
                new Osoba(){Imie="Jaga", Nazwisko="Baba", Rok=1780, Pensja=9000, IdZawodu=1}
            };
        }
    }
    class Zawod
    {
        public int IdZawodu { get; set; }
        public String Nazwa { get; set; }

        public static List<Zawod> Utworz()
        {
            return new List<Zawod>()
            {
                new Zawod(){IdZawodu=1, Nazwa="Najstarszy zawód świata"},
                new Zawod(){IdZawodu=2, Nazwa="Programista"},
                new Zawod(){IdZawodu=3, Nazwa="Galerianka"},
                new Zawod(){IdZawodu=4, Nazwa="Polityk"}
            };
        }
    }
}
