using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deep.Core
{
    public class AssemblyRecord
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public List<AssemblyRecord> References { get; set; }


        public System.Reflection.AssemblyName AssemblyName { get; set; }
    }
}
