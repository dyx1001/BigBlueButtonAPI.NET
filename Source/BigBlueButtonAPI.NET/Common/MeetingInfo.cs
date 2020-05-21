/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BigBlueButtonAPI.Common
{
    public class MeetingInfo
    {
        /// <summary>
        /// The name for the meeting.
        /// </summary>
        public string meetingName { get; set; }

        /// <summary>
        /// The meeting ID.
        /// </summary>
        public string meetingID { get; set; }

        /// <summary>
        /// The internal meeting ID that the system uses.
        /// </summary>
        public string internalMeetingID { get; set; }
        public long? createTime { get; set; }
        public string createDate { get; set; }

        /// <summary>
        /// Voice conference number for the FreeSWITCH voice conference associated with this meeting. This must be a 5-digit number in the range 10000 to 99999. If you add a phone number to your BigBlueButton server, This parameter sets the personal identification number (PIN) that FreeSWITCH will prompt for a phone-only user to enter. If you want to change this range, edit FreeSWITCH dialplan and defaultNumDigitsForTelVoice of bigbluebutton.properties.
        /// The voiceBridge number must be different for every meeting.
        /// </summary>
        public int? voiceBridge { get; set; }

        /// <summary>
        /// The dial access number that participants can call in using regular phone. You can set a default dial number via defaultDialAccessNumber in bigbluebutton.properties
        /// </summary>
        public string dialNumber { get; set; }

        /// <summary>
        /// The password that the join URL can later provide as its password parameter to indicate the user will join as a viewer. 
        /// If no attendeePW is provided, the create call will return a randomly generated attendeePW password for the meeting.
        /// </summary>
        public string attendeePW { get; set; }


        public string moderatorPW { get; set; }
        public bool? running { get; set; }
        public int? duration { get; set; }
        public bool? hasUserJoined { get; set; }
        public bool? recording { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public long? startTime { get; set; }
        public long? endTime { get; set; }
        public int? participantCount { get; set; }
        public int? listenerCount { get; set; }
        public int? voiceParticipantCount { get; set; }
        public int? videoCount { get; set; }
        public int? maxUsers { get; set; }
        public int? moderatorCount { get; set; }

        [XmlArrayItem("attendee")]
        public List<Attendee> attendees { get; set; }

        public MetaData metadata { get; set; }

        public bool? isBreakout { get; set; }

        /// <summary>
        /// If a meeting has spawned breakout rooms, then getMeetingInfo will also a list of meetingIDs for the breakout rooms.
        /// </summary>
        [XmlArrayItem("breakout")]
        public List<string> breakoutRooms { get; set; }

        /// <summary>
        /// If a meeting is a breakout room itself, then getMeetingInfo will also return a link to the parent internal meetingID.
        /// </summary>
        public string parentMeetingID { get; set; }
        /// <summary>
        /// If a meeting is a breakout room itself, then getMeetingInfo will also return a link to the parent internal meetingID.
        /// </summary>
        public int? sequence { get; set; }

        /// <summary>
        /// If a meeting is a breakout room itself, then getMeetingInfo will also return a link to the parent internal meetingID.
        /// </summary>
        public bool? freeJoin { get; set; }
    }
}