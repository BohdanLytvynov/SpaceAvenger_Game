using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFGameEngine.Interfaces.Loader;
using WPFGameEngine.Utilities.Directories;
using WPFGameEngine.Utilities.Images;

namespace WPFGameEngine.Realizations.Loader
{
    public class RecursiveBitmapImageLoader : IResourceLoader<List<BitmapImage>>
    {
        private string m_path;

        public RecursiveBitmapImageLoader(string path)
        {
            m_path = path;
        }

        public List<BitmapImage> Load(params object[] args)
        {
            if(args == null || args.Length == 0)
                throw new ArgumentNullException("args was null or not set!");

            string resourceName = args[0].ToString();

            if(string.IsNullOrEmpty(resourceName))
                throw new Exception("Can not get resourceName from args!");

            if (!DirectoryUtilities.DirectoryExists(m_path))
                throw new DirectoryNotFoundException(nameof(resourceName));

            var info = new DirectoryInfo(m_path);           

            List<BitmapImage> bitmaps = new List<BitmapImage>();

            bool ret = false;
            
            LoadRec(info, resourceName, ref ret, bitmaps);
            
            return bitmaps;
        }

        private void LoadRec(DirectoryInfo info, string resName, ref bool stop,
            List<BitmapImage> bitmaps)
        {
            if (stop)
                return;

            var files = info.GetFiles();

            if (files.Length > 0 && info.Name.Equals(resName))
            {
                foreach (var file in files)
                {
                    bitmaps.Add(ImageUtility.LoadBitmapsUsingUri(file.FullName, UriKind.RelativeOrAbsolute));
                }

                stop = true;

                return;
            }                

            var dir = info.GetDirectories();

            foreach (var d in dir)
            { 
                if(!stop)
                    LoadRec(d, resName, ref stop, bitmaps);
            }
        }

        
    }
}
