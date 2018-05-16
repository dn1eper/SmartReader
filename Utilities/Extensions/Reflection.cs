using System.Collections.Generic;
using System.Linq;

namespace System.Reflection
{
    internal static class ReflectionExcensions
    {
        public delegate bool Criteria(Type type);
        public static Type[] GetTypes(this AppDomain domain, Criteria criteria)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            IEnumerable<Type> types = assembly.LoadDependencies().GetTypes(criteria);
            return Enumerable.ToArray(types);
        }

        private static IEnumerable<Type> GetTypes(this IEnumerable<Assembly> assemblies, Criteria criteria)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes(criteria))
                {
                    yield return type;
                }
            }
        }

        private static IEnumerable<Assembly> LoadDependencies(this Assembly a)
        {
            foreach (AssemblyName name in a.GetReferencedAssemblies())
            {
                yield return Assembly.Load(name);
            }
            yield return a;
        }

        private static IEnumerable<Type> GetTypes(this Assembly assembly, Criteria criteria)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (criteria(type))
                {
                    yield return type;
                }
            }
        }
    }
}
