using Newtonsoft.Json;
using System.IO;

namespace MpesaLib.Helpers.Serialization
{
    /// <summary>
    /// JSON Stream Serialization and Deserialization Helper Class
    /// </summary>
    public class JSONStreamHelper
    {
        /// <summary>
        /// Deserializes data from stream into a json object
        /// </summary>
        /// <param name="stream">stream of bytes</param>
        /// <returns></returns>
        public static object DeserializeFromStream(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var strm = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(strm))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }
    }
}
