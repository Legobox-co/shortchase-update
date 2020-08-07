using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using FixerIoCore;

namespace Shortchase.Helpers
{
    public static class CurrencyHelper
    {
        public static Symbol? GetSymbolFromAcronym(string Acronym)
        {
            
            switch (Acronym.ToUpper()) {
                case "AUD": 
                    return Symbol.AUD;
                case "BGN": 
                    return Symbol.BGN;
                case "BRL": 
                    return Symbol.BRL;
                case "CAD": 
                    return Symbol.CAD;
                case "CHF": 
                    return Symbol.CHF;
                case "CNY": 
                    return Symbol.CNY;
                case "CZK": 
                    return Symbol.CZK;
                case "DKK": 
                    return Symbol.DKK;
                case "EUR": 
                    return Symbol.EUR;
                case "GBP": 
                    return Symbol.GBP;
                case "HKD": 
                    return Symbol.HKD;
                case "HRK": 
                    return Symbol.HRK;
                case "HUF": 
                    return Symbol.HUF;
                case "IDR":
                    return Symbol.IDR;
                case "ILS":
                    return Symbol.ILS;
                case "INR":
                    return Symbol.INR;
                case "JPY":
                    return Symbol.JPY;
                case "KRW":
                    return Symbol.KRW;
                case "MXN":
                    return Symbol.MXN;
                case "MYR":
                    return Symbol.MYR;
                case "NOK":
                    return Symbol.NOK;
                case "NZD":
                    return Symbol.NZD;
                case "PHP":
                    return Symbol.PHP;
                case "PLN":
                    return Symbol.PLN;
                case "RON":
                    return Symbol.RON;
                case "RUB":
                    return Symbol.RUB;
                case "SEK":
                    return Symbol.SEK;
                case "SGD":
                    return Symbol.SGD;
                case "THB":
                    return Symbol.THB;
                case "TRY":
                    return Symbol.TRY;
                case "USD":
                    return Symbol.USD;
                case "ZAR":
                    return Symbol.ZAR;

                default:
                    return null;
            }
           
        }

    }
}
