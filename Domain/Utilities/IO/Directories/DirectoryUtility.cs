using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities.IO.Directories
{
    public static class DirectoryUtility
    {
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static void Create(string path)
        {
            if (!DirectoryExists(path))
                Directory.CreateDirectory(path);
        }

        public static int GetFileCount(string path)
        {
            int count = 0;

            if (DirectoryExists(path))
            {
                count = Directory.GetFiles(path).Length;
            }
            else
                throw new DirectoryNotFoundException(nameof(path));

            return count;
        }

        public static IEnumerable<string> GetFilesNames(string path)
        {
            if (DirectoryExists(path))
                return Directory.GetFiles(path);
            throw new DirectoryNotFoundException(nameof(path));
        }

        public static IEnumerable<FileInfo> GetFilesInfo(string path)
        {
            if (DirectoryExists(path))
            {
                DirectoryInfo d = new DirectoryInfo(path);

                return d.GetFiles();
            }

            throw new DirectoryNotFoundException(nameof(path));
        }
    }
}
