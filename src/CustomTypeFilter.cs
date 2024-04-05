using System;

namespace Optique.Reflection;

public readonly struct CustomTypeFilter : ITypeFilter
{
    private readonly Func<Type, bool> _filter;


    public CustomTypeFilter(Func<Type, bool> filter)
    {
        _filter = filter;
    }

    public bool IsMatch(Type targetObject)
    {
        return _filter.Invoke(targetObject);
    }
}