using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class hra
    {
        public int PocetHracu;
        public int[] Body = new int[5];
        public int BodyCelkem;
        public int Cislo;
        public bool Pokracovat;
        public Kostka dice = new Kostka();
        public int[] BodyHracu = new int[2000];

        public hra(int PocetHracu)
        {
            this.PocetHracu = PocetHracu;
        }

        /// <summary>
        /// hodí kostkou a zapíše hodnotu
        /// </summary>
        public void Hrej()
        {
            Cislo = dice.Hod();
        }

        /// <summary>
        /// sečte body jednotlivce
        /// </summary>
        public void SectiBody()
        {
            BodyCelkem = ((Body[0] + Body[1] - Body[2]) * Body[3]) / Body[4];
        }

        /// <summary>
        /// vymaže body hráčů z předchozí hry
        /// </summary>
        public void VycistiBody()
        {
            for(int i = 0; i < PocetHracu; i++)
            {
                BodyHracu[i] = 0;
            }
        }

        /// <summary>
        /// zjistí zda po třetím hodu hráč nevypadl
        /// </summary>
        public void Check()
        {
                if(Body[0] + Body[1] - Body[2] <= 0)
                Pokracovat = false;
                else Pokracovat = true;
        }
    }

    public class Kostka
    {
        private static Random rn;
        public Kostka()
        {
            rn = new Random();
        }
        public int Hod()
        {
            return rn.Next(1, 6);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("                                 MATEMATIKA");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine(" Každý hráč má sérii pěti hodů. Hodnoty prvních dvou hodů sečte a od tohoto");
            Console.WriteLine(" součtu odečte hodnotu třetího hodu. Jestliže v tuto chvíli dosáhl záporného");
            Console.WriteLine(" výsledku nebo nuly, vypadává ze hry. V opačném případě hází počtvrté a tímto");
            Console.WriteLine(" číslem násobí dosavadní výsledek. Posledním hodem pak tento součin vydělí.");
            Console.WriteLine(" Komu vyšlo po závěrečném dělení nejvyšší číslo, zvítězil.");
            Console.WriteLine("");
            Console.WriteLine("(Stiskněte libovolnou klávesu pro začátek hry)");
            Console.ReadKey();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" Zadejte počet hráčů (2+)");
                string input = Console.ReadLine();
                int PctHrcu;
                if (int.TryParse(input, out PctHrcu) && PctHrcu >= 2 && PctHrcu <= 2000)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(" {0}", PctHrcu);
                    hra hra1 = new hra(PctHrcu);
                    for (int i = 0; i < PctHrcu; i++)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(" hraje hráč č.{0}", i + 1);
                        for (int l = 0; l < 3; l++)
                        {
                            Console.WriteLine(" Napište h pro hod");
                            input = Console.ReadLine();
                            if (input == "h")
                            {
                                hra1.Hrej();
                                hra1.Body[l] = hra1.Cislo;
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.WriteLine(" hodil jste {0}", hra1.Cislo);
                                Console.WriteLine("");
                            }
                            else
                            {
                                l = l - 1;
                                Console.SetCursorPosition(0, Console.CursorTop - 2);
                            }
                        }
                        hra1.Check();
                        if (hra1.Pokracovat == false)
                        {
                            Console.WriteLine("");
                            Console.WriteLine(" {0} + {1} - {2} JE MENŠÍ NEBO ROVNO NULE, HRÁČ č.{3} VYPADÁVÁ", hra1.Body[0], hra1.Body[1], hra1.Body[2], i + 1);
                            hra1.Pokracovat = true;
                        }
                        else
                        {
                            for (int t = 0; t < 2; t++)
                            {
                                Console.WriteLine(" Napište h pro hod");
                                input = Console.ReadLine();
                                if (input == "h")
                                {
                                    hra1.Hrej();
                                    hra1.Body[t + 3] = hra1.Cislo;
                                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                                    Console.WriteLine(" hodil jste {0}", hra1.Cislo);
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    t = t - 1;
                                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                                }
                            }
                            hra1.SectiBody();
                            Console.WriteLine(" CELKOVÝ POČET BODŮ HRÁČE č.{0} JE {1}", i + 1, hra1.BodyCelkem);
                            Console.WriteLine("");
                            hra1.BodyHracu[i] = hra1.BodyCelkem;
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("  Vyhrává hráč č.{0} s {1} body", Array.IndexOf(hra1.BodyHracu, hra1.BodyHracu.Max()) + 1, hra1.BodyHracu.Max());
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("Stiskněte libovolnou klávesu pro novou hru");
                    hra1.VycistiBody();
                }
                else Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.ReadKey();
            }
        }
    }
}
