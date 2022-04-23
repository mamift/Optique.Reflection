using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Optique.Reflection
{
    public static class ReflectionUtility
    {
        public const string GlobalNamespace = "::Global Namespace::";

        public static Assembly[] Assemblies { get; } = AppDomain.CurrentDomain.GetAssemblies();
        public static Type[] Types { get; } = Assemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();
        public static Namespace[] Namespaces { get; } = GetNamespaces();

        private static readonly Dictionary<string, Type> TypesCache = new Dictionary<string, Type>();


        public static Type[] GetTypes(params ITypeFilter[] filters)
        {
            if (filters.Length == 0)
            {
                return Types;
            }
            else
            {
                return Types.Where(type => filters.All(filter => filter.IsMatch(type))).ToArray();
            }
        }

        private static Namespace[] GetNamespaces()
        {
            Dictionary<string, List<Type>> namespacesMap = new Dictionary<string, List<Type>>();

            foreach (Type type in Types)
            {
                string name = type.Namespace ?? GlobalNamespace;

                if (namespacesMap.ContainsKey(name) == false)
                {
                    namespacesMap.Add(name, new List<Type>());
                }

                namespacesMap[name].Add(type);
            }

            List<Namespace> namespaces = new List<Namespace>(namespacesMap.Count);

            foreach (KeyValuePair<string, List<Type>> namespacePair in namespacesMap)
            {
                Namespace item = new Namespace(namespacePair.Key)
                {
                        Types = namespacePair.Value.ToArray()
                };

                namespaces.Add(item);
            }

            foreach (Namespace space in namespaces)
            {
                space.Namespaces = namespaces.Where(n => space.IsChildOf(n)).ToArray();

                foreach (Namespace child in space.Namespaces)
                {
                    child.Parent = space;
                }
            }

            List<Namespace> additionalNamespaces = new List<Namespace>();

            foreach (Namespace space in namespaces)
            {
                if (space.Parent != null || space.Name.Contains('.') == false)
                {
                    continue;
                }

                string[] modules = space.Name.Split('.');

                Namespace nearestNamespace = null;
                StringBuilder nameBuilder = new StringBuilder();

                for (int i = 0; i < modules.Length - 1; ++i)
                {
                    string moduleName = modules[i];
                    if (nameBuilder.Length > 0)
                    {
                        nameBuilder.Append('.');
                    }

                    nameBuilder.Append(moduleName);
                    string name = nameBuilder.ToString();

                    Namespace parentNamespace = namespaces.Find(n => n.Name.Equals(name));
                    if (parentNamespace == null)
                    {
                        parentNamespace = additionalNamespaces.Find(n => n.Name.Equals(name));
                    }

                    if (parentNamespace == null)
                    {
                        Namespace extraNamespace = new Namespace(name);

                        if (nearestNamespace != null)
                        {
                            if (nearestNamespace.Namespaces == null)
                            {
                                nearestNamespace.Namespaces = new[] {extraNamespace};
                            }
                            else
                            {
                                nearestNamespace.Namespaces = nearestNamespace.Namespaces.Concat(new[] {extraNamespace})
                                        .ToArray();
                            }

                            extraNamespace.Parent = nearestNamespace;
                        }

                        nearestNamespace = extraNamespace;
                        additionalNamespaces.Add(extraNamespace);
                    }
                    else
                    {
                        nearestNamespace = parentNamespace;
                    }
                }

                if (nearestNamespace != null)
                {
                    if (nearestNamespace.Namespaces == null)
                    {
                        nearestNamespace.Namespaces = new[] {space};
                    }
                    else
                    {
                        nearestNamespace.Namespaces = nearestNamespace.Namespaces.Concat(new[] {space}).ToArray();
                    }

                    space.Parent = nearestNamespace;
                }
            }

            namespaces.AddRange(additionalNamespaces);

            return namespaces.Where(n => n.Parent == null).ToArray();
        }

        public static Assembly FindAssembly(string assemblyName)
        {
            Assembly result = Assemblies.FirstOrDefault(assembly => assembly.GetName().Name.Equals(assemblyName));
            return default == result ? null : result;
        }

        public static Type FindType(string typeFullName)
        {
            if (TypesCache.ContainsKey(typeFullName) == false)
            {
                bool Predicate(Type type) => typeFullName.Equals(type.FullName);

                if (Types.Any(Predicate))
                {
                    TypesCache.Add(typeFullName, Types.First(Predicate));
                }
                else
                {
                    return null;
                }
            }

            return TypesCache[typeFullName];
        }
    }
}