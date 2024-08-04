using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WPFGameEngine.Interfaces.Loader;


namespace WPFGameEngine.Extensions
{
    public static class ApplicationExtentions
    {
        public static TResource? TryGetResourceOrLoad<TResource>(this Application app, string folderName, 
            IResourceLoader<TResource> loader = null, string resNameMod = "")
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(folderName);
            if(!string.IsNullOrEmpty(resNameMod))
                stringBuilder.Append("-");
            stringBuilder.Append(resNameMod);

            var str = stringBuilder.ToString();

            TResource? resource = default;

            resource = (TResource)app.TryFindResource(str);

            if (resource == null)
            {
                if (loader == null)
                    throw new ArgumentNullException(nameof(loader));

                resource = loader.Load(folderName);

                app.Resources.Add(str, resource);
            }

            return resource;
        }
    }


}
