using Deep.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep.Cli
{
    class Program
    {
        const string COPYRIGHT = @"Deep - .NET Assembly Browser 2014 (c) All Rights Reserved";


        static void Main(string[] args)
        {
            Console.WriteLine(COPYRIGHT);
            Console.WriteLine();

            var controller = new Controller();
            controller.Start(args[0]);

            Console.ReadLine();

        }
    }
}
