using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class NotFoundSliderException : Exception
    {
        public NotFoundSliderException(string? message) : base(message)
        {
        }
    }
}
