using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Optique.Reflection
{
    public class ConjunctiveMemberFilter : IMemberFilter
    {
        private readonly IEnumerable<IMemberFilter> _filters;
        
        public ConjunctiveMemberFilter(IEnumerable<IMemberFilter> filters)
        {
            _filters = filters;
        }
        
        public bool IsMatch(MemberInfo targetObject)
        {
            return _filters.All(filter => filter.IsMatch(targetObject));
        }
    }
}