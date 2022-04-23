using System.Reflection;

namespace Optique.Reflection
{
    public class AccessModifierFilter : IMemberFilter
    {
        private readonly AccessModifiers _filterCriteria;

        
        public AccessModifierFilter(AccessModifiers accessModifier)
        {
            _filterCriteria = accessModifier;
        }

        public bool IsMatch(MemberInfo memberInfo)
        {
            switch (memberInfo.GetAccessModifier())
            {
                case AccessModifiers.Private: return _filterCriteria.HasFlag(AccessModifiers.Private);
                case AccessModifiers.PrivateProtected: return _filterCriteria.HasFlag(AccessModifiers.PrivateProtected);
                case AccessModifiers.Protected: return _filterCriteria.HasFlag(AccessModifiers.Protected);
                case AccessModifiers.ProtectedInternal: return _filterCriteria.HasFlag(AccessModifiers.ProtectedInternal);
                case AccessModifiers.Internal: return _filterCriteria.HasFlag(AccessModifiers.Internal);
                case AccessModifiers.Public: return _filterCriteria.HasFlag(AccessModifiers.Public);
            }

            return false;
        }
    }
}