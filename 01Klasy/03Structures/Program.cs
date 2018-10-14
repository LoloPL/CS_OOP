﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Structures {
    class Pierwsza {
        public int x = 0;
    }

    struct First {
        public int x;
    }
    
    class Program {
        static void Main(string[] args) {
            Pierwsza p1 = new Pierwsza();
            p1.x = 5;
            Pierwsza p2 = p1;
            p2.x = 7;
            Console.WriteLine($"p1.x = {p1.x}, p2.x = {p2.x}");

            First f1 = new First();
            f1.x = 5;
            First f2 = f1;
            f2.x = 7;
            Console.WriteLine($"f1.x = {f1.x}, f2.x = {f2.x}");

            int x = 5;
            int y = x;
            y = 7;
            Console.WriteLine($"x = {x}, y = {y}");
        }
    }
}
