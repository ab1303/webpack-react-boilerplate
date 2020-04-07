using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BatchPayments.Utility.ExtensionHelpers
{
    public static class TypeExtensions
    {
        public static T DeepCopy<T>(T other)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter
                {
                    Context = new StreamingContext(StreamingContextStates.Clone)
                };
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
