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
    public class JoinMeetingResponse : BaseResponse
    {
        /// <summary>
        /// The internal meeting ID that the system uses.
        /// </summary>
        [XmlElement("meeting_id")]
        public string internalMeetingID { get; set; }

        /// <summary>
        /// An identifier for this user that will help your application to identify which person this is. 
        /// </summary>
        [XmlElement("user_id")]
        public string userID { get; set; }



        /// <summary>
        /// You should simply redirect the user to the call URL, and they will be entered into the meeting.
        /// </summary>
        public string url { get; set; }
    }
}