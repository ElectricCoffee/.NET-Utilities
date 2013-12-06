using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Utility.Methods
{
    public static class ServerCommunicator
    {
        private const string 
            APPLICATION_JSON = "application/json",
            REQUEST_POST     = "POST",
            REQUEST_DELETE   = "DELETE";

        /// <summary>
        /// A Helper method to remove boilerplate code, it does all the actual conversion.
        /// </summary>
        /// <param name="inputUri">A string representing the uri of whatever you want to get data from</param>
        /// <param name="clientfunction">An higher-order function/delegate that allows us to specify the behavior of the method</param>
        private static void ResponseHelper(string inputUri, Action<WebClient, Uri> clientfunction)
        {
            var client = new WebClient();
            client.Headers[HttpResponseHeader.ContentType] = APPLICATION_JSON;
            var uri = new Uri(inputUri);
            clientfunction(client, uri);
        }

        /// <summary>
        /// Gets data from the source specified by the URI string
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <returns></returns>
        public static string Get(this string inputUri)
        {
            string result = "";
            ResponseHelper(inputUri, (client, uri) => result = client.DownloadString(uri));
            return result;
        }

        /// <summary>
        /// Gets JSON object from the source specified by the URI string, and converts it directly to an object of the given class
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <returns></returns>
        public static T Get<T>(this string inputUri)
        {
            string result = "";
            ResponseHelper(inputUri, (client, uri) => result = client.DownloadString(uri));
            return new JavaScriptSerializer().Deserialize<T>(result);
        }

        /// <summary>
        /// Gets data from the source specified by the URI string asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <param name="downloadStringCompleted">The event hander the downloaded string will be sent to</param>
        public static void GetAsync(this string inputUri, DownloadStringCompletedEventHandler downloadStringCompleted)
        {
            ResponseHelper(inputUri, (client, uri) =>
            {
                client.DownloadStringCompleted += downloadStringCompleted;
                client.DownloadStringAsync(uri);
            });
        }

        /// <summary>
        /// A helper method for the Post operation to avoid boilerplate code
        /// </summary>
        /// <param name="inputUri">The URI of the source you want to post to</param>
        /// <param name="clientfunction">A higher-order function/delegate that specifies the behavior of the helper method</param>
        private static void RequestHelper(string inputUri, Action<WebClient, Uri> clientfunction)
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = APPLICATION_JSON;
            var uri = new Uri(inputUri);
            clientfunction(client, uri);
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void Post(this string inputUri, string jsonInput)
        {
            RequestHelper(inputUri, (client, uri) => client.UploadString(uri, REQUEST_POST, jsonInput));
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void PostAsync(this string inputUri, string jsonInput)
        {
            RequestHelper(inputUri, (client, uri) => client.UploadStringAsync(uri, REQUEST_POST, jsonInput));
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI, the data is automatically turned into a Json object
        /// </summary>
        /// <typeparam name="T">Any object type</typeparam>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="obj">The object to be sent to the receiver as json</param>
        public static void Post<T>(this string inputUri, T obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            inputUri.Post(json);
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI asynchronously, the data is automatically turned into a Json object
        /// </summary>
        /// <typeparam name="T">Any object type</typeparam>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="obj">The object to be sent to the receiver as json</param>
        public static void PostAsync<T>(this string inputUri, T obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            inputUri.PostAsync(json);
        }

        /// <summary>
        /// Deletes data to a receiver specified by the input URI
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void Delete(this string inputUri)
        {
            RequestHelper(inputUri, (client, uri) => client.UploadString(uri, REQUEST_DELETE, ""));
        }

        /// <summary>
        /// Deletes data to a receiver specified by the input URI asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void DeleteAsync(this string inputUri)
        {
            RequestHelper(inputUri, (client, uri) => client.UploadStringAsync(uri, REQUEST_DELETE, ""));
        }

        /// <summary>
        /// A Helper method to remove boilerplate code, it does all the actual conversion.
        /// </summary>
        /// <param name="inputUri">A string representing the uri of whatever you want to get data from</param>
        /// <param name="clientfunction">An higher-order function/delegate that allows us to specify the behavior of the method</param>
        private static void ResponseHelper(Action<WebClient> clientfunction)
        {
            var client = new WebClient();
            client.Headers[HttpResponseHeader.ContentType] = APPLICATION_JSON;
            clientfunction(client);
        }

        /// <summary>
        /// Gets data from the source specified by the URI string
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <returns></returns>
        public static string Get(this Uri inputUri)
        {
            string result = "";
            ResponseHelper(client => result = client.DownloadString(inputUri));
            return result;
        }

        /// <summary>
        /// Gets JSON object from the source specified by the URI string, and converts it directly to an object of the given class
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <returns></returns>
        public static T Get<T>(this Uri inputUri)
        {
            string result = "";
            ResponseHelper(client => result = client.DownloadString(inputUri));
            return new JavaScriptSerializer().Deserialize<T>(result);
        }

        /// <summary>
        /// Gets data from the source specified by the URI string asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the source</param>
        /// <param name="downloadStringCompleted">The event hander the downloaded string will be sent to</param>
        public static void GetAsync(this Uri inputUri, DownloadStringCompletedEventHandler downloadStringCompleted)
        {
            ResponseHelper(client =>
            {
                client.DownloadStringCompleted += downloadStringCompleted;
                client.DownloadStringAsync(inputUri);
            });
        }

        /// <summary>
        /// A helper method for the Post operation to avoid boilerplate code
        /// </summary>
        /// <param name="inputUri">The URI of the source you want to post to</param>
        /// <param name="clientfunction">A higher-order function/delegate that specifies the behavior of the helper method</param>
        private static void RequestHelper(Action<WebClient> clientfunction)
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = APPLICATION_JSON;
            clientfunction(client);
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void Post(this Uri inputUri, string jsonInput)
        {
            RequestHelper(client => client.UploadString(inputUri, "POST", jsonInput));
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void PostAsync(this Uri inputUri, string jsonInput)
        {
            RequestHelper(client => client.UploadStringAsync(inputUri, "POST", jsonInput));
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI, the data is automatically turned into a Json object
        /// </summary>
        /// <typeparam name="T">Any object type</typeparam>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="obj">The object to be sent to the receiver as json</param>
        public static void Post<T>(this Uri inputUri, T obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            inputUri.Post(json);
        }

        /// <summary>
        /// Posts data to a receiver specified by the input URI asynchronously, the data is automatically turned into a Json object
        /// </summary>
        /// <typeparam name="T">Any object type</typeparam>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="obj">The object to be sent to the receiver as json</param>
        public static void Post<T>(this Uri inputUri, T obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            inputUri.PostAsync(json);
        }

        /// <summary>
        /// Deletes data to a receiver specified by the input URI
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void Delete(this Uri inputUri, string jsonInput)
        {
            RequestHelper(client => client.UploadString(inputUri, "DELETE", ""));
        }

        /// <summary>
        /// Deletes data to a receiver specified by the input URI asynchronously
        /// </summary>
        /// <param name="inputUri">The URI of the receiver</param>
        /// <param name="jsonInput">The JSON object string to be sent to the receiver</param>
        public static void DeleteAsync(this Uri inputUri)
        {
            RequestHelper(client => client.UploadStringAsync(inputUri, "DELETE", ""));
        }
    }
}