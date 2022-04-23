using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Optique.Reflection
{
    public class AssemblyFilter : ITypeFilter
    {
        private readonly Assembly[] _assemblies;
        
        public AssemblyFilter(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public AssemblyFilter(params string[] assemblies)
        {
            List<Assembly> result = new List<Assembly>(assemblies.Length);
            foreach (string assemblyName in assemblies)
            {
                Assembly assembly = ReflectionUtility.FindAssembly(assemblyName);
                if (assembly != null)
                {
                    result.Add(assembly);
                }
            }

            _assemblies = result.ToArray();
        }
        
        public bool IsMatch(Type targetObject)
        {
            return _assemblies.Any(assembly => assembly.Equals(targetObject.Assembly));
        }
    }
}