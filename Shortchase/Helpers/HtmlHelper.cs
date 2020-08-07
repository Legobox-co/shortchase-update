using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Shortchase.Helpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Will return a specific class if one of the controllers and one of the actions match the current route. Returns an empty string if it doesn't.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controllers">All controllers to be checked against. Comma separated</param>
        /// <param name="actions">All the actions to be checked against. Comma separated</param>
        /// <param name="cssClass">The classes that should be added in case of success. Default is "active"</param>
        /// <returns></returns>
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active text-primary")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            var RouteValues = htmlHelper.ViewContext.ViewData.ModelState.Values.ToList();

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        /// <summary>
        /// Will return a specific class if one of the controllers and one of the actions match the current route with the specified parameter name and value. Returns an empty string if it doesn't.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controllers">All controllers to be checked against. Comma separated</param>
        /// <param name="actions">All the actions to be checked against. Comma separated</param>
        /// <param name="cssClass">The classes that should be added in case of success. Default is "active"</param>
        /// <param name="keys">The name of the parameter in the route to check for</param>
        /// <param name="values">The value of the parameter in the route to check for as a string</param>
        /// <returns></returns>
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string keys, string values, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
            var Route = htmlHelper.ViewContext.ViewData.ModelState;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');
            IEnumerable<string> acceptedKeys = (keys ?? "").Split(',');
            IEnumerable<string> acceptedValues = (values ?? "").Split(',');

            bool isRoute = acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController);
            bool isValue = Route.Any(r => acceptedKeys.Contains(r.Key) && acceptedValues.Contains(r.Value.AttemptedValue));

            return isRoute && isValue ? cssClass : String.Empty;
        }


        public static string TruncateHtml(this string input, int length = 300,
                                   string ommission = "...")
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}" + ommission, input.Substring(0, (iNextSpace > 0) ?
                                                                  iNextSpace : length).Trim());
        }

        public static string StripTags(this string markup)
        {
            try
            {
                StringReader sr = new StringReader(markup);
                XPathDocument doc;
                using (XmlReader xr = XmlReader.Create(sr,
                                   new XmlReaderSettings()
                                   {
                                       ConformanceLevel = ConformanceLevel.Fragment
                               // for multiple roots
                           }))
                {
                    doc = new XPathDocument(xr);
                }

                return doc.CreateNavigator().Value; // .Value is similar to .InnerText of  
                                                    //  XmlDocument or JavaScript's innerText
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
