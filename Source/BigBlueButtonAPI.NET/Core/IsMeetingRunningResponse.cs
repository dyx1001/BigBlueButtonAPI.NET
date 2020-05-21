/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System.Xml.Serialization;

namespace BigBlueButtonAPI.Core
{
    [XmlRoot("response")]
    public class IsMeetingRunningResponse : BaseResponse
    {
        /// <summary>
        /// Whether or not a meeting is running
        /// </summary>
        public bool? running { get; set; }
    }
}