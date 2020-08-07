using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Shortchase.Helpers
{
    public static class ImageHelper
    {
        ///
        /// Utility method used to validate the contents of uploaded files
        ///
        public static string ConvertImageToBase64(string filepath)
        {
            return "data:image/" + System.IO.Path.GetExtension(filepath).Replace(".", "") + ";base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(filepath));
        }

    }
}
