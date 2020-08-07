using System;

namespace Shortchase.Entities
{
    public class ErrorLog : IntBase
    {
        public string Log { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public Guid? User { get; set; }
    }
}