using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Utility.Methods
{
    /// <summary>
    /// Serves to remove boilerplate code, by simply letting the user call a method directly on the object you're using
    /// </summary>
    public static class DataConverter
    {
        /// <summary>
        /// Takes any JSON Object in string form, and deserializes it to the specified class
        /// </summary>
        /// <typeparam name="T">Any Class with fields that match the JSON object</typeparam>
        /// <param name="input">Any JSON Object string</param>
        /// <returns>An Object that contains the data</returns>
        public static T DeserializeJson<T>(this string input)
        {
            return new JavaScriptSerializer().Deserialize<T>(input);
        }

        /// <summary>
        /// Takes any object and turns it into a string containing a JSON Object
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="input">Any Object</param>
        /// <returns>String containing a JSON Object</returns>
        public static string SerializeToJsonObject<T>(this T input)
        {
            return new JavaScriptSerializer().Serialize(input);
        }

        /// <summary>
        /// Takes any object and turns it into an XML string
        /// </summary>
        /// <typeparam name="T">Any object type</typeparam>
        /// <param name="input">An object of any kind</param>
        /// <returns>An XML string based on the object.</returns>
        public static string SerializeToXML<T>(this T input)
        {
            var submit = new XmlSerializer(typeof(T));
            var subReq = input;
            var sww = new StringWriter();
            var writer = XmlWriter.Create(sww);
            submit.Serialize(writer, subReq);

            return sww.ToString();
        }

        /// <summary>
        /// Takes an xml string and turns it into an object of the given class. 
        /// Note that the class needs an XmlRoot and XmlElements specified
        /// </summary>
        /// <typeparam name="T">Any type of class</typeparam>
        /// <param name="input">An XML string</param>
        /// <returns>An object of the specified class.</returns>
        public static T DeserializeFromXML<T>(this string input)
        {
            var serializer = new XmlSerializer(typeof(T));

            T result = default(T);

            using (var reader = new StringReader(input))
            {
                result = (T)serializer.Deserialize(reader);
            }
            
            return result;
        }
    }
}
