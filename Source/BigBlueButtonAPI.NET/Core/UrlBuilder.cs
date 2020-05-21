using BigBlueButtonAPI.Core;
using BigBlueButtonAPI.Common;
using System.Collections.Generic;
/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System.Net.Http;
using System.Reflection;
namespace BigBlueButtonAPI.Core
{

    /// <summary>
    /// It is used to build the url to call the BigBlueButton API.
    /// Please visit the following site for details:
    ///   http://docs.bigbluebutton.org/dev/api.html
    /// </summary>
    public class UrlBuilder
    {
        private readonly BigBlueButtonAPISettings settings;

        public UrlBuilder(BigBlueButtonAPISettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// It builds the url.
        /// </summary>
        /// <param name="method">The BigBlueButton API name.</param>
        /// <param name="parameters">
        /// The request data in query string format. 
        /// For example: a=1&b=2
        /// It doesn't contain the checksum. 
        /// </param>
        /// <param name="onlyQueryString">
        /// If it is true, the method returns only the paramters append checksum data.
        /// Otherwise it returns the full url.
        /// </param>
        /// <returns></returns>
        private string Build(string method, string parameters, bool onlyQueryString = false)
        {
            if (parameters == null) parameters = string.Empty;
            var checksum = GetChecksum(method,parameters);

            if (onlyQueryString)
            {
                if (string.IsNullOrEmpty(parameters))
                {
                    return string.Format("checksum={0}", checksum);
                }
                else
                {
                    return string.Format("{0}&checksum={1}", parameters, checksum);
                }
            } else
            {
                if (string.IsNullOrEmpty(parameters))
                {
                    return string.Format("{0}{1}?checksum={2}", settings.ServerAPIUrl, method, checksum);
                }
                else
                {
                    return string.Format("{0}{1}?{2}&checksum={3}", settings.ServerAPIUrl, method, parameters, checksum);
                }
            }

        }

        /// <summary>
        /// It builds the url.
        /// </summary>
        /// <param name="method">The BigBlueButton API name.</param>
        /// <param name="request">
        /// The request data.
        /// It will be built into the query string format.
        /// </param>
        /// <param name="onlyQueryString">
        /// If it is true, the method returns only the query string append checksum data.
        /// Otherwise it returns the full url.
        /// </param>
        /// <returns></returns>
        public string Build(string method, BaseRequest request, bool onlyQueryString = false)
        {
            string parameters = BuildParameters(method, request);
            return Build(method, parameters, onlyQueryString);
        }

        /// <summary>
        /// It returns the base url for a method.
        /// For example: http://yourserver.com/bigbluebutton/api/create
        /// </summary>
        /// <param name="method">The BigBlueButton API name.</param>
        /// <returns></returns>
        public string BuildMethodUrl(string method)
        {
            return settings.ServerAPIUrl + method;
        }
        /// <summary>
        /// It builds the parameters.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildParameters(string method, BaseRequest request)
        {
            string parameters = string.Empty;
            if (request != null)
            {
                var items = new List<KeyValuePair<string, string>>();

                string sValue;
                foreach (System.Reflection.PropertyInfo p in request.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    var value = p.GetValue(request, null);
                    if (value != null)
                    {
                        if (value is FileContentData) continue;
                        if (value is MetaData)
                        {
                            var metaData = (MetaData)value;
                            if (metaData.Count > 0)
                            {
                                foreach (var key in metaData.Keys)
                                {
                                    items.Add(new KeyValuePair<string, string>("meta_" + key, metaData[key]));
                                }

                            }
                        }
                        else
                        {
                            if (value.Equals(true)) sValue = "true";
                            else if (value.Equals(false)) sValue = "false";
                            else sValue = value.ToString();

                            items.Add(new KeyValuePair<string, string>(p.Name, sValue));
                        }

                    }
                }
                if (items.Count > 0)
                {
                    items.Sort((x, y) => x.Key.CompareTo(y.Key));
                    var c = new FormUrlEncodedContent(items);
                    parameters = c.ReadAsStringAsync().Result;
                }
            }
            return parameters;
        }

        /// <summary>
        /// It returns the checksum.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string GetChecksum(string method, string parameters)
        {
            if (parameters == null) parameters = string.Empty;
            return HashHelper.Sha1Hash(method + parameters, settings.SharedSecret);
        }
    }
}