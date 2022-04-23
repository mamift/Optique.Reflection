using System;
using System.Reflection;

namespace Optique.Reflection
{
    public class CustomMemberFilter : IMemberFilter
    {
        private readonly Func<MemberInfo, bool> _filter;
        
        
        public CustomMemberFilter(Func<MemberInfo, bool> filter)
        {
            _filter = filter;
        }
        
        public bool IsMatch(MemberInfo targetObject)
        {
            return _filter.Invoke(targetObject);
        }
    }
}