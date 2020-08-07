using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Shortchase.Helpers
{
    public static class FileHelper
    {
        ///
        /// Utility method used to validate the contents of uploaded files
        ///
        public static bool FileIsFormSafe(string fileType)
        {
            try
            {
                List<string> acceptedFileTypes = new List<string>
                {
                    ".png",
                    ".jpg",
                };

                if (acceptedFileTypes.Contains(fileType.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        ///
        /// Utility method used to validate the contents of uploaded files using filesize
        /// <param name="size">The maximum size of the file in bytes</param>
        ///
        public static bool FileIsFormSafe(string fileType, long fileSize)
        {
            return fileSize <= 10240000 && FileIsFormSafe(fileType);
        }


        public static string PathCombine(string path1, string path2)
        {
            if (Path.IsPathRooted(path2))
            {
                path2 = path2.TrimStart(Path.DirectorySeparatorChar);
                path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);
            }

            return Path.Combine(path1, path2);
        }

        public static string PathCombine(params string[] paths)
        {
            var finalpath = "";
            if (paths.Count() > 0) finalpath = paths[0];
            for (int i = 1; i < paths.Count(); i++)
            {
                finalpath = PathCombine(finalpath, paths[i]);
            }
            return finalpath;
        }


        ///
        /// Utility method used to validate the contents of uploaded files
        ///
        public static bool FileIsMassUploaderSafe(string fileType)
        {
            try
            {
                List<string> acceptedFileTypes = new List<string>
                {
                    ".csv"
                };

                if (acceptedFileTypes.Contains(fileType.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        ///
        /// Utility method used to validate the contents of uploaded files using filesize
        /// <param name="size">The maximum size of the file in bytes</param>
        ///
        public static bool FileIsMassUploaderSafe(string fileType, long fileSize)
        {
            return fileSize <= 10240000 && FileIsMassUploaderSafe(fileType);
        }

    }
}
