using System;
using System.Collections.Generic;

namespace TheatreUZ
{
    public class CommandResponse
    {
        public Guid ID { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<object> ResponseObjects { get; set; }
    }
}