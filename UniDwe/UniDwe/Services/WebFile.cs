using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;
using System.Security.Cryptography;

namespace UniDwe.Services
{
    public class WebFile
    {
        public string GetWebFileName(string filename)
        { 
            string dir = GetWebFileFolder(filename);
            CreateDirectory(dir);
            return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpeg";
        }
        public string GetWebFileFolder(string filename)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputbytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputbytes);

            string hash = Convert.ToHexString(hashBytes);

            return "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
        }

        public void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth, int newHeight)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidh = newWidth;
                int aspectHeight = newHeight;
                if (image.Width / (image.Height / newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newHeight));
                else
                    aspectWidh = (int)(image.Width / (image.Height / (float)newHeight));



               int width = image.Width / 2;
               int height = image.Height / 2;
               image.Mutate(x => x.Resize(aspectWidh, aspectHeight, KnownResamplers.Lanczos3));

               await image.SaveAsJpegAsync(filename, new JpegEncoder() { Quality = 100 });
            }
        }
    }
}
