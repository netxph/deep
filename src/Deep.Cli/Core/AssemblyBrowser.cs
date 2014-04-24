using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Deep.Core
{
    public class AssemblyBrowser
    {
        Dictionary<string, AssemblyRecord> _localCache = new Dictionary<string, AssemblyRecord>();

        public AssemblyRecord Analyze(Assembly assembly)
        {
            var record = new AssemblyRecord();
            record.AssemblyName = assembly.GetName();
            record.Name = record.AssemblyName.Name;

            var version = record.AssemblyName.Version;
            record.Version = string.Format("{0}.{1}.{2}.{3}", version.Major.ToString().PadLeft(2), version.Minor.ToString().PadLeft(2), version.MajorRevision.ToString().PadLeft(2), version.MinorRevision.ToString().PadLeft(2));

            record.References = new List<AssemblyRecord>();

            foreach (var reference in assembly.GetReferencedAssemblies())
            {
                try
                {
                    var referenceAssembly = Assembly.ReflectionOnlyLoad(reference.FullName);
                    if (!referenceAssembly.GlobalAssemblyCache)
                    {
                        if (!_localCache.ContainsKey(referenceAssembly.FullName))
                        {
                            var referenceRecord = Analyze(referenceAssembly);
                            _localCache.Add(referenceAssembly.FullName, referenceRecord);
                        }

                        record.References.Add(_localCache[referenceAssembly.FullName]);
                    }
                }
                catch { }
            }

            record.References.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));

            return record;
        }

    }
}
