using System;

namespace Optique.Reflection
{
    public class Namespace
    {
        public Namespace Parent { get; internal set; }
        public string Name { get; internal set; }
        public Type[] Types { get; internal set; } = Array.Empty<Type>();
        public Namespace[] Namespaces { get; internal set; } = Array.Empty<Namespace>();

        
        internal Namespace(string name)
        {
            Name = name;
        }
        
        public override string ToString()
        {
            return $"{Name}: namespaces {Namespaces?.Length ?? 0}, types {Types?.Length ?? 0}";
        }
    }
}