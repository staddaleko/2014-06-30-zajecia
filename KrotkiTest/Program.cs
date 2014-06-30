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
            ZapytaniePierwsze();
            IEnumerable<Osoba> osoby = Osoba.Utworz();
            ZapytanieDrugie(osoby);
            ZapytanieTrzecie(osoby);
            ZapytanieCzwarte(osoby);
            ZapytaniePiate(osoby);
            ZapytanieSzoste(osoby);
            ZapytanieSiodme(osoby);
            ZapytanieOsme(osoby);
            ZapytanieDziewiate(osoby);
            var zawody = Zawod.Utworz();
            ZapytanieDzisiate(osoby, zawody);
            ZapytanieJedenaste(osoby);
            ZapytanieDwunaste(osoby, zawody);
            ZapytanieTrzynaste(osoby, zawody);

            Console.ReadKey();

        }

        private static void ZapytanieTrzynaste(IEnumerable<Osoba> osoby, List<Zawod> zawody)
        {
            Console.WriteLine("\n*********** trzynaste zapytanie za pomoca iteracji ************");

            foreach (var z in zawody)
            {
                decimal suma = 0;
                foreach (var o in osoby)
                {
                    if (z.IdZawodu == o.IdZawodu)
                    {
                        suma += o.Pensja;
                    }
                }
                Console.WriteLine("{0}: {1:C} ", z.Nazwa, suma);//wyniki identyczne jak w zapytaniu dwunastym

            }
        }
        private static void ZapytanieDwunaste(IEnumerable<Osoba> osoby, List<Zawod> zawody)
        {
            Console.WriteLine("\n*********** dwunaste zapytanie ************");
            var os10 = from z in zawody
                       join o in osoby on z.IdZawodu equals o.IdZawodu into grupa
                       from g in grupa.DefaultIfEmpty() //left join
                       group g by z.Nazwa into grupa2
                       select new
                       {
                           nazwaZawodu = grupa2.Key,
                           suma = grupa2.Sum(z => z == null ? 0 : z.Pensja)
                       };
            foreach (var z in os10)
            {
                Console.WriteLine("{0} zarobił {1:C}", z.nazwaZawodu, z.suma);
            }
        }
        private static void ZapytanieJedenaste(IEnumerable<Osoba> osoby)
        {
            Console.WriteLine("\n*********** jedenaste zapytanie ************");
            var os9 = osoby.GroupBy(p => p.IdZawodu);//pokazuje 'idzawodu' oraz sume pensji
            foreach (var z in os9)
            {
                Console.WriteLine("IdZawodu:{0} -- Łącznie zarobili:{1}", z.Key, z.Sum(p => p.Pensja));//jedna odpowiedź jest pusta, vo ma wartość 'null'.
            }
        }
        private static void ZapytanieDzisiate(IEnumerable<Osoba> osoby, List<Zawod> zawody)
        {
            Console.WriteLine("\n*********** dziesiate zapytanie ************");
            var os8 = zawody.GroupJoin(osoby, z => z.IdZawodu, o => o.IdZawodu,
                (z, grupa) => new { z.Nazwa, Ilosc = grupa.Count() });
            foreach (var z in os8)
            {
                Console.WriteLine("{0} {1}", z.Nazwa, z.Ilosc);
            }
        }
        private static void ZapytanieDziewiate(IEnumerable<Osoba> osoby)
        {
            var zawody = Zawod.Utworz();
            Console.WriteLine("\n*********** dziewiąte zapytanie ************");
            var os7 = from z in zawody
                      join o in osoby
                      on z.IdZawodu equals o.IdZawodu into grupa
                      from g in grupa.DefaultIfEmpty()//domyslma wartosc jezeli puste, czylu 'null' w tym wypadku
                      select new
                      {
                          z.Nazwa,
                          Imie = g == null ? "--- brak imienia ---" : g.Imie, //pytanie czy wartość 'null' i jeżeli tak, to '---'
                          Nazwisko = g == null ? "--- brak nazwiska ---" : g.Nazwisko
                      };
            foreach (var z in os7)
            {
                Console.WriteLine("{0} -- {1} {2}", z.Nazwa, z.Imie, z.Nazwisko);
            }
        }
        private static void ZapytanieOsme(IEnumerable<Osoba> osoby)
        {
            Console.WriteLine("\n*********** ósme zapytanie ************");
            var zawody = Zawod.Utworz();
            //metoda dłuższa:            
            //var os6 = from z in zawody
            //          join o in osoby
            //          on z.IdZawodu equals o.IdZawodu into grupa
            //          select new { z.Nazwa, grupa };
            //foreach (var z in os6)
            //{
            //    Console.WriteLine(z.Nazwa);
            //    foreach (var o in z.grupa)
            //    {
            //        Console.WriteLine(o);
            //    }

            //metoda krótsza, ale tożsama z powyższą:
            //tworzymy jedną 'klasę', w której wyszczególniane są wszystkie inne elementy. Tu wg zawodu otrzymujemy członków.
            var os6alt = zawody.GroupJoin(osoby, z => z.IdZawodu, o => o.IdZawodu,
                (z, grupa) => new { z.Nazwa, grupa });

            foreach (var z in os6alt)
            {
                Console.WriteLine(z.Nazwa);
                foreach (var o in z.grupa)
                {
                    Console.WriteLine("\t\t{0}", o);
                }
            }
        }
        private static void ZapytanieSiodme(IEnumerable<Osoba> osoby)
        {
            Console.WriteLine("\n*********** siódme zapytanie (inna składnia niż w szóstym, ta sama odpowiedź) ************"); //to jest alternatywna wersja zapytania szóstego.
            var zawody = Zawod.Utworz();
            var os5 = osoby.Join(zawody, o => o.IdZawodu, z => z.IdZawodu,
                (o, z) => new { o.Imie, o.Nazwisko, z.Nazwa });
            foreach (var o in os5)
            {
                Console.WriteLine("{0} {1} -- zawód: {2}", o.Imie, o.Nazwisko, o.Nazwa);
            }
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
        public int? IdZawodu { get; set; } // '?' umożliwia wartość 'null' dla zmiennej

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
