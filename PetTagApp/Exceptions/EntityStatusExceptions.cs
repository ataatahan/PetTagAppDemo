using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Exceptions
{
    public class EntityStatusExceptions : Exception
    {
        public class EntityAlreadyActiveException : Exception
        {
            public EntityAlreadyActiveException()
                : base("Entity is already active.") { }
        }

        public class EntityAlreadyPassiveException : Exception
        {
            public EntityAlreadyPassiveException()
                : base("Entity is already passive.") { }
        }

        public class InvalidEntityStatusException : Exception
        {
            public InvalidEntityStatusException(string message)
                : base(message) { }
        }
    }
}
