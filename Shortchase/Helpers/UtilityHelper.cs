using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Shortchase.Entities;

namespace Shortchase.Helpers
{
    public static class Utility
    {
        public static int PageSize(int Count, int PageSize)
        {
            var number = Count % PageSize != 0 ? Count / PageSize + 1 : Count / PageSize;
            return number;
        }
        public static string GetAPICallUrl(ListingCategory category, string data)
        {
            string url = category.APIURL + data + category.APIMethod + "?api_key=" + category.APIKey;
            return url;
        }
        public static string GetLiveScoreAPICallUrl(ListingCategory category, string dateFrom, string dateTo)
        {
            string url = category.APIURL + "?key=" + category.APIKey + "&from="+dateFrom +"&to="+dateTo;
            return url;
        }

        public static bool IsLinux()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return true;
            }else{
                return false;
            }
        }
    }
}
