using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TungstenRssReader.DataReaders
{
    public class DataReaderException : Exception
    {
        public DataReaderException()
        {
        }

        public DataReaderException(string message) : base(message)
        {
        }

        public DataReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
