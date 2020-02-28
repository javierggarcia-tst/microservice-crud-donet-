using System;

namespace CRUDBasico.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class AtributosDomainException : Exception
    {
        public AtributosDomainException()
        { }

        public AtributosDomainException(string message)
            : base(message)
        { }

        public AtributosDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
