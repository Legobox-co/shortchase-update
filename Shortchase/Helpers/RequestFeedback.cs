using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortchase.Helpers
{
    public class RequestFeedback
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public bool Success { get; set; }

        public RequestFeedback(string Title = "There was a problem resolving your request", string Text = "", bool Success = false)
        {
            this.Title = Title;
            this.Text = Text;
            this.Success = Success;
        }

        public RequestFeedback()
        {
            this.Title = "There was a problem resolving your request";
            this.Text = "";
            this.Success = false;
        }
    }
}
