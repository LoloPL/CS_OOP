﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Reflection;

using PluginInterfejs;

namespace ProgramGlowny
{
    class Program
    {
        private static List<IMojPlugin> zaladowaneWtyczki = new List<IMojPlugin>();

        private static char Menu()
        {
            Console.Clear();
            //double qqq = 5;
            //double a = 1 / (qqq - 5);
            //Console.WriteLine($"a = {a + 1} {Double.IsNaN(a)} {Double.IsInfinity(a)}");

            Console.WriteLine("\t\t\tA - Załaduj wtyczki");
            char c = 'B';
            foreach(IMojPlugin wtyczka in zaladowaneWtyczki)
            {
                Console.WriteLine("\t\t\t{0} - {1}", c, wtyczka.Menu);
                c++;
            }
            Console.WriteLine("\t\t\t{0} - Wyjście z programu", c);
            if (c == 'B')
                Console.WriteLine("\n\t\t\tBrak załadowanych wtyczek, załaduj najpierw wtyczki");
            return Console.ReadKey(true).KeyChar;
        }
        private static bool CzyJuzZaladowany(Type t)
        {
            foreach (IMojPlugin wtyczka in zaladowaneWtyczki)
            {
                Type t2 = wtyczka.GetType();
                if (t == t2)
                    return true;
            }
            return false;
        }

        private static bool CzyZaladowac(Type t)
        {  
            object[] atrybuty = t.GetCustomAttributes(typeof(MojAtrybutAttribute), false);
            if (atrybuty != null)
            {
                MojAtrybutAttribute moj = (MojAtrybutAttribute)atrybuty[0];
                Console.WriteLine($"Czy załadować wtyczkę napisaną przez {moj.Autor} i której celem jest:\n{moj.Opis}");
            }
            else
            {
                Console.WriteLine("Czy załadować wtyczkę nieznanego autora i niewiadomego przeznaczenia?");
            }
            char c = Console.ReadKey().KeyChar;
            return c == 't' || c == 'T' ;
        }

        private static void ZaladujWtyczki()
        {
         
          if(!Directory.Exists("Wtyczki"))
            {
                Console.WriteLine("Brak katalogu Wtyczki, który służy do przechowywania rozszerzeń tego programu!!!");
                Console.ReadKey(true);
                return;
            }
            string[] pliki = Directory.GetFiles(@"Wtyczki\", "*.dll");
            for (int i = 0; i < pliki.Length; i++)
            {
                Assembly zestaw = Assembly.LoadFrom(pliki[i]);
                foreach (Type typ in zestaw.GetTypes())
                {
                    if (typ.IsPublic && !typ.IsAbstract)
                    {

                        Type interfejsy = typ.GetInterface("PluginInterfejs.IMojPlugin", true);
                        if (interfejsy != null)
                        {
                            if (!CzyJuzZaladowany(typ))
                            {
                                if (CzyZaladowac(typ))
                                {

                                    IMojPlugin tmp = (IMojPlugin)Activator.CreateInstance(zestaw.GetType(typ.ToString()));
                                    zaladowaneWtyczki.Add(tmp);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Typ {0} załadowany", typ);
                                Console.ReadKey();
                            }

                        }
                    }
                }
            }            
        }

        private static bool UruchomPolecenie(char c)
        {
            if (c == 'A')
                ZaladujWtyczki();
            int numer = c - 'B';
            if (numer == zaladowaneWtyczki.Count)
                return false;       //oznacza że wybrano koniec programu

            if (numer >= 0 && numer < zaladowaneWtyczki.Count)
            {
                double x = 0, y = 0;
                do
                {
                    Console.Write("Podaj pierwszą liczbę: ");
                }
                while (!double.TryParse(Console.ReadLine(), out x));
                do
                {
                    Console.Write("Podaj drugą liczbę: ");
                }
                while (!double.TryParse(Console.ReadLine(), out y));
                Console.WriteLine("Wynikiem jest: {0}", zaladowaneWtyczki[numer].RobCos(x,y));
                Console.ReadKey(true);

            }
            return true;
        }

        static void Main(string[] args)
        {
            char c;
            do
            {
                c = Menu();
            }
            while(UruchomPolecenie(c));
        }
    }
}
