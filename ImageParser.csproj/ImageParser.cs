using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ImageParser
{
    internal class JsonImage
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Format { get; set; }
        public long Size { get; set; }
 
    }
    public class ImageParser : IImageParser
    {
        public string GetImageInfo(Stream stream)
        {
            const int lengthArrayBytes = 80;
            byte[] bytes = new byte[lengthArrayBytes];
            int count = stream.Read(bytes, 0, bytes.Length);
            JsonImage myJson = new JsonImage();

            var formatImage =  "";
            var sizeImage =  (long)0;
            var widthImage =  0;
            var heightImage =  0;
            //bmp
            if ((bytes[0] == 66) && (bytes[1] == 77))
            {
                formatImage = "bmp";
                sizeImage = BitConverter.ToInt32(bytes, 2);
                widthImage = BitConverter.ToInt32(bytes, 18);
                heightImage = BitConverter.ToInt32(bytes, 22);
            }
            //png
            if ((bytes[1] == 80) && (bytes[2] == 78)&& (bytes[3] == 71))
            {
                formatImage = "png";
                sizeImage = stream.Length;
                bytes = bytes.Reverse().ToArray();
                widthImage = BitConverter.ToInt32(bytes, 60);
                heightImage = BitConverter.ToInt32(bytes, 56);
            }
            //gif
            if ((bytes[0] == 71) && (bytes[1] == 73)&& (bytes[2] == 70))
            {
                formatImage = "gif";
                sizeImage = stream.Length;

                widthImage = BitConverter.ToInt16(bytes, 6);
                heightImage = BitConverter.ToInt16(bytes, 8);
            }

            myJson.Size = sizeImage;
            myJson.Format = formatImage;
            myJson.Height = heightImage;
            myJson.Width = widthImage; 
            
            var serialized = JsonConvert.SerializeObject(myJson);
            return serialized;
        }
    }
}