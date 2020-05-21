/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBlueButtonAPI.Core
{
    /// <summary>
    /// The request data of ending a meeting and kick all participants out of the meeting.
    /// </summary>
    public class EndMeetingRequest : BaseRequest
    {
        /// <summary>
        /// The meeting ID that identifies the meeting you are attempting to end.
        /// </summary>
        public string meetingID { get; set; }

        /// <summary>
        /// The moderator password for this meeting. You can not end a meeting using the attendee password.
        /// </summary>
        public string password { get; set; }
    }
}