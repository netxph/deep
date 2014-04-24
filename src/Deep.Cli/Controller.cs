using Deep.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deep.Cli
{
    public class Controller
    {
        const int PAD_SIZE = 3;
        int _padding = 0;

        public void Start(string assemblyFile)
        {
            var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);

            var browser = new AssemblyBrowser();
            var record = browser.Analyze(assembly);

            DisplayAssembly(record);
        }

        protected virtual void DisplayAssembly(AssemblyRecord record)
        {
            string line = string.Format("{0}{1}", record.Version.PadRight(15), record.Name);
            Console.WriteLine(line.PadLeft(line.Length + _padding));

            _padding += PAD_SIZE;
            foreach (var reference in record.References)
            {
                DisplayAssembly(reference);
            }
            _padding -= PAD_SIZE;

        }

    }
}
