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

        HashSet<AssemblyRecord> _records = new HashSet<AssemblyRecord>();

        public void Start(string assemblyFile)
        {
            var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);

            var browser = new AssemblyBrowser();
            var record = browser.Analyze(assembly);

            DisplayAssembly(record);
        }

        protected virtual void DisplayAssembly(AssemblyRecord record)
        {
            if (_records.Add(record))
            {
                Console.WriteLine(string.Format("Assembly: {0}", record.Name));
                Console.WriteLine("References:");

                foreach (var reference in record.References)
                {
                    string line = string.Format("{0}{1}", reference.Version.PadRight(15), reference.Name);
                    Console.WriteLine(line.PadLeft(line.Length + PAD_SIZE));
                }

                foreach (var reference in record.References)
                {
                    DisplayAssembly(reference);
                }

            }

        }

    }
}
