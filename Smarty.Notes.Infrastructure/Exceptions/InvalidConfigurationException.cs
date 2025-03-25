using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarty.Notes.Infrastructure.Exceptions
{
    public class InvalidConfigurationException : InfrastructureException
    {
        public InvalidConfigurationException()
        {
        }

        public InvalidConfigurationException(string? message) : base(message)
        {
        }

        public InvalidConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}