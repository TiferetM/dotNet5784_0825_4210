using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    internal class BlInsufficientTime : Exception
    {
        public BlInsufficientTime()
        {
        }

        public BlInsufficientTime(string? message) : base(message)
        {
        }

        public BlInsufficientTime(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BlInsufficientTime(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}