namespace mamift.Reflequery.Extensions;

internal static class NamespaceExtensions
{
    internal static bool IsChildOf(this Namespace parentNamespace, Namespace childNamespace)
    {
        string parent = parentNamespace.Name;
        string child = childNamespace.Name;

        if (child.Length <= parent.Length) {
            return false;
        }

        string[] parentModules = parent.Split('.');
        string[] childModules = child.Split('.');

        if (childModules.Length - parentModules.Length != 1) {
            return false;
        }

        for (int i = 0; i < parentModules.Length; ++i) {
            if (parentModules[i].Equals(childModules[i]) == false) {
                return false;
            }
        }

        return true;
    }
}