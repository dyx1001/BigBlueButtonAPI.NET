/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBlueButtonAPI.Common
{
    public class Attendee
    {
        /// <summary>
        /// An identifier for this user that will help your application to identify which person this is. 
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// The full name that is to be used to identify this user to other conference attendees.
        /// </summary>
        public string fullName { get; set; }

        /// <summary>
        /// The role of the user: VIEWER or MODERATOR.
        /// </summary>
        public string role { get; set; }

        public bool isPresenter { get; set; }
        public bool isListeningOnly { get; set; }
        public bool hasJoinedVoice { get; set; }
        public bool hasVideo { get; set; }
        public string clientType { get; set; }
    }
}