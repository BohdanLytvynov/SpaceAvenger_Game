using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class ReflexionUtility
    {
        public static IEnumerable<TypeInfo> GetObjectsTypeInfo(Assembly assembly,
            Func<TypeInfo, bool> predicate)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            var types = assembly.DefinedTypes;

            if (types.Count() == 0)
                throw new Exception("Assembly is empty!");

            var objects = types.Where(predicate);

            if (objects.Count() == 0)
                throw new Exception("Fail to find some TypeInfo objects!");

            return objects;
        }
    }
}
