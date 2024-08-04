using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGameEngine.Utilities.Directories;

namespace WPFGameEngine.Utilities.Images
{
    public static class ImageUtility
    {
        public static BitmapImage LoadBitmapsUsingUri(string uri, UriKind kind)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri, kind);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();            
            return bitmap;
        }

        public async static Task<BitmapImage> LoadBitmapsUsingUriAsync(string uri, UriKind kind)
        {
            BitmapImage bitmap = new BitmapImage();
            await Task.Run(() =>
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(uri, kind);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            });
            return bitmap;
        }

        public static List<BitmapImage> LoadBitMapsFromFolder(string path)
        {
            List<BitmapImage> images = new List<BitmapImage>();

            var imgs = DirectoryUtilities.GetFilesNames(path);

            foreach (var imgsFile in imgs)
            {
                images.Add(LoadBitmapsUsingUri(
                    imgsFile,
                    UriKind.RelativeOrAbsolute));
            }

            return images;
        }

        public async static Task<List<BitmapImage>> LoadBitMapsFromFolderAsync(string path)
        {
            List<BitmapImage> images = null;
            await Task.Run(() =>
            {
                images = LoadBitMapsFromFolder(path);
            });
            return images;
        }
    }
}
