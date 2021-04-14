using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.Exceptions
{
    class DuplicateRequestException : Exception
    {
        public DuplicateRequestException()
        {
        }

        public DuplicateRequestException(string message)
            : base(message)
        {
        }

        public DuplicateRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
