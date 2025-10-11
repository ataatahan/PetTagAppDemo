using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Exceptions
{
    public class EmptyHealthRecordDescriptionException : Exception
    {
        public EmptyHealthRecordDescriptionException()
            : base("Health record açıklaması boş geçilemez.") { }

        public EmptyHealthRecordDescriptionException(string message)
            : base(message) { }

    }
}
