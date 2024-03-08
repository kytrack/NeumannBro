namespace ConsoleApp8
{
    using System.Text.RegularExpressions;
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> Adatsorok = File.ReadAllLines("uzenetek.txt").ToList();
            //a Egysorba nig
            Console.WriteLine($"A kérdőjelek száma: {Adatsorok.Sum(sor => sor.Count(c => c == '?'))}");
            //V: 7442

            //b


            int sorHossz = Adatsorok.FirstOrDefault()?.Length ?? 0;
            string helyesadat = "";

            string helyesadatmegoldva = "7610922751913275142233435073915524642160008422684973049146293643594970710907471138712178244571230807";
            List<Hibak> lista= new List<Hibak>();
            for (int i = 0; i < 10; i++)
            {
                lista.Add(new Hibak(i , 0, 0));
            }


            


            var karakterGyakorisag = Enumerable.Range(0, sorHossz)
    .Select(pozicio => new
    {
        Pozicio = pozicio,
        Gyakorisag = Adatsorok.Select(sor => sor[pozicio])
                            .GroupBy(karakter => karakter)
                            .ToDictionary(gr => gr.Key, gr => gr.Count())
    })
    .SelectMany(data => data.Gyakorisag.Select(kv => new
    {
        Pozicio = data.Pozicio, 
        Karakter = kv.Key,
        Gyakorisag = kv.Value
    }))
    .GroupBy(data => data.Pozicio)
    .Select(group => group.OrderByDescending(data => data.Gyakorisag).First());

            foreach (var adat in karakterGyakorisag)
            {
                Console.WriteLine($"Pozíció: {adat.Pozicio + 1}, Karakter: {adat.Karakter}, Gyakoriság: {adat.Gyakorisag}");

                // Hozzáadás a helyes adathoz
                
                helyesadat += adat.Karakter;


                double numerikusErtek = char.GetNumericValue(adat.Karakter);
                int szamjegyjo = (int)numerikusErtek;

                lista[szamjegyjo].Gyakorisag++;
                lista[szamjegyjo].Osszhibaszam+=500-adat.Gyakorisag;


                //  lista[Convert.ToInt32(adat.Karakter)].Osszhibaszam += 500 - adat.Gyakorisag;
            }


            Console.WriteLine(helyesadat + " ennyi karakter:" + helyesadat.Length);
            //7610922751913275142233435073915524642160008422684973049146293643594970710907471138712178244571230807










            //c
            //0.75138461538 ez egy százalék baby



            foreach (var item in lista)
            {
                Console.WriteLine("számjegy:"+ item.Szamjegy+" gyakorisag:"+item.Gyakorisag+" osszhibaszam:"+item.Osszhibaszam);
            }



            //d
            int legtobbhiba = 0;
            int rosszkodsora = 0;
            int rosszkodpozicioja = 0;
            for (int i = 0; i < Adatsorok.Count(); i++)
            {
                int hibak = 0;
                for (int l = 0; l < helyesadat.Length; l++)
                {
                    if (Adatsorok[i][l] != helyesadat[l])
                    {
                        hibak++;
                    }
                    else
                    {
                        if (hibak > legtobbhiba)
                        {
                            legtobbhiba = hibak;
                            rosszkodsora = i + 1;
                            rosszkodpozicioja = l + 1;
                        }
                        hibak = 0;
                    }
                }

            }
            Console.WriteLine("Egybefuggohiba:" + legtobbhiba + " Sora:" + rosszkodsora + " Pozicioja:" + rosszkodpozicioja);
            //V: 31 95 91







            //2. feladat
            List<string> SzavakLista = File.ReadAllLines("szavak.txt").ToList();

            //a
            Console.WriteLine($"A szavak száma amelyekben legalább 4 magánhanzó vagy: {SzavakLista.Count(x => MennyiMaganhangzoVanBenne(x) >= 4)}");

            //b
            string command = @".*E.*S.*A.*T.*";
            Regex rg = new Regex(command);
            Console.WriteLine($"A szavak száma amely tartalmazza a megfelelő szöveget (ESAT): {SzavakLista.Count(x => Convert.ToBoolean(rg.IsMatch(x)))}");

            //c

            Console.WriteLine(SzavakLista.Count(a => IsPrime(a.ToList().Sum(x => Convert.ToInt32(x)/*valami*/))));

            //d

            //Console.WriteLine("dibag" + SzavakLista.Select(a => a.ToList().Sum(x => Convert.ToInt32(x))).ToList().GroupBy(item => item).GroupBy(g => g.Count(), g => g.Key).OrderByDescending(counts => counts.Key).First().Key);
            Console.WriteLine(SzavakLista.Select(a => a.ToList().Sum(x => Convert.ToInt32(x))).ToList().GroupBy(item => item).OrderByDescending(x => x.Count()).ToList().First().Key);
            //"a drágámnak"(a mátrix)
            // if(gymási)

            //e
            List<char> SzavakBetui = new List<char>();
            List<char> megoldas = new List<char>();

            SzavakLista.ForEach(x => {
                if (AsciiOsszeg(x) == 607)
                {
                    SzavakBetui.AddRange(SzoBetui(x));
                }
            });

            Console.Write("Megoldas: ");
            SzavakBetui.GroupBy(x => x).ToList().ForEach(x =>
            {
                if (x.Count() > 5)
                {
                    megoldas.Add(x.Key);
                }
            });

            megoldas = megoldas.OrderBy(x => (int)x).ToList();
            Console.WriteLine(String.Join("", megoldas));

        }

        static int AsciiOsszeg(string szo)
        {
            return szo.ToList().Sum(x => Convert.ToInt32(x));
        }

        static List<char> SzoBetui(string szo)
        {
            return szo.Distinct().ToArray().OrderBy(x => (int)x).ToList();
        }

        static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        public static int MennyiMaganhangzoVanBenne(string szo)
        {
            List<char> MaganhangzokLista = new List<char>()
            {
                'A', 'E', 'I', 'O', 'U'
            };

            return szo.ToList().Count(x => MaganhangzokLista.Contains(x));

        }
    }
}