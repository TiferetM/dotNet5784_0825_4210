using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    internal class BlNullPropertyException : Exception
    {
        public BlNullPropertyException()
        {
        }

        public BlNullPropertyException(string? message) : base(message)
        {
        }

        public BlNullPropertyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlNullPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}