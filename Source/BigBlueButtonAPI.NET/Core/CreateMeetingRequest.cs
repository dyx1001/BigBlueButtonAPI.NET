/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using BigBlueButtonAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBlueButtonAPI.Core
{
    /// <summary>
    /// The request data of creating a new meeting.
    /// </summary>
    public class CreateMeetingRequest:BaseRequest
    {
        /// <summary>
        /// Optional.
        /// A name for the meeting.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Required.
        /// A meeting ID that can be used to identify this meeting by the 3rd-party application. 
        /// This must be unique to the server that you are calling: different active meetings can not have the same meeting ID.
        /// If you supply a non-unique meeting ID (a meeting is already in progress with the same meeting ID), then if the other parameters in the create call are identical, the create call will succeed (but will receive a warning message in the response). The create call is idempotent: calling multiple times does not have any side effect. This enables a 3rd-party applications to avoid checking if the meeting is running and always call create before joining each user.
        /// Meeting IDs should only contain upper/lower ASCII letters, numbers, dashes, or underscores. A good choice for the meeting ID is to generate a GUID value as this all but guarantees that different meetings will not have the same meetingID.
        /// </summary>
        public string meetingID { get; set; }

        /// <summary>
        /// Optional (recommended).
        /// The password that the join URL can later provide as its password parameter to indicate the user will join as a viewer. 
        /// If no attendeePW is provided, the create call will return a randomly generated attendeePW password for the meeting.
        /// </summary>
        public string attendeePW { get; set; }

        /// <summary>
        /// Optional (recommended).
        /// The password that will join URL can later provide as its password parameter to indicate the user will as a moderator. if no moderatorPW is provided, create will return a randomly generated moderatorPW password for the meeting.
        /// </summary>
        public string moderatorPW { get; set; }

        /// <summary>
        /// Optional.
        /// A welcome message that gets displayed on the chat window when the participant joins. You can include keywords (%%CONFNAME%%, %%DIALNUM%%, %%CONFNUM%%) which will be substituted automatically.
        /// This parameter overrides the default defaultWelcomeMessage in bigbluebutton.properties.
        /// The welcome message has limited support for HTML formatting. Be careful about copy/pasted HTML from e.g. MS Word, as it can easily exceed the maximum supported URL length when used on a GET request.
        /// </summary>
        public string welcome { get; set; }

        /// <summary>
        /// Optional.
        /// The dial access number that participants can call in using regular phone. You can set a default dial number via defaultDialAccessNumber in bigbluebutton.properties
        /// </summary>
        public string dialNumber { get; set; }

        /// <summary>
        /// Optional.
        /// Voice conference number for the FreeSWITCH voice conference associated with this meeting. This must be a 5-digit number in the range 10000 to 99999. If you add a phone number to your BigBlueButton server, This parameter sets the personal identification number (PIN) that FreeSWITCH will prompt for a phone-only user to enter. If you want to change this range, edit FreeSWITCH dialplan and defaultNumDigitsForTelVoice of bigbluebutton.properties.
        /// The voiceBridge number must be different for every meeting.
        /// This parameter is optional. If you do not specify a voiceBridge number, then BigBlueButton will assign a random unused number for the meeting.
        /// If do you pass a voiceBridge number, then you must ensure that each meeting has a unique voiceBridge number; otherwise, reusing same voiceBridge number for two different meetings will cause users from one meeting to appear as phone users in the other, which will be very confusing to users in both meetings.
        /// </summary>
        public int? voiceBridge { get; set; }

        /// <summary>
        /// Optional.
        /// Set the maximum number of users allowed to joined the conference at the same time.
        /// </summary>
        public int? maxParticipants { get; set; }

        /// <summary>
        /// Optional.
        /// The URL that the BigBlueButton client will go to after users click the OK button on the ‘You have been logged out message’. This overrides the value for bigbluebutton.web.logoutURL in bigbluebutton.properties.
        /// </summary>
        public string logoutURL { get; set; }

        /// <summary>
        /// Optional.
        /// Setting ‘record=true’ instructs the BigBlueButton server to record the media and events in the session for later playback. The default is false.
        /// In order for a playback file to be generated, a moderator must click the Start/Stop Recording button at least once during the sesssion; otherwise, in the absence of any recording marks, the record and playback scripts will not generate a playback file.See also the autoStartRecording and allowStartStopRecording parameters in bigbluebutton.properties.
        /// </summary>
        public bool? record { get; set; }

        /// <summary>
        /// Optional.
        /// The maximum length (in minutes) for the meeting.
        /// Normally, the BigBlueButton server will end the meeting when either (a) the last person leaves (it takes a minute or two for the server to clear the meeting from memory) or when the server receives an end API request with the associated meetingID (everyone is kicked and the meeting is immediately cleared from memory).
        /// BigBlueButton begins tracking the length of a meeting when it is created. If duration contains a non-zero value, then when the length of the meeting exceeds the duration value the server will immediately end the meeting (equivalent to receiving an end API request at that moment).
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Required(Breakout Room).
        /// Must be set to true to create a breakout room.
        /// </summary>
        public bool? isBreakout { get; set; }

        /// <summary>
        /// Required(Breakout Room).
        /// Must be provided when creating a breakout room, the parent room must be running.
        /// </summary>
        public string parentMeetingID { get; set; }

        /// <summary>
        /// Required(Breakout Room).
        /// The sequence number of the breakout room. Start from 1.
        /// </summary>
        public int? sequence { get; set; }

        /// <summary>
        /// Optional.
        /// This is a special parameter type (there is no parameter named just meta).
        /// You can pass one or more metadata values when creating a meeting. These will be stored by BigBlueButton can be retrieved later via the getMeetingInfo and getRecordings calls.
        /// Examples of the use of the meta parameters are meta_Presenter=Jane%20Doe, meta_category=FINANCE, and meta_TERM=Fall2016.
        /// This is a dictionary object, each item in it will be passed to the server as a parameter:
        ///     For example: if the key of a item is "category" and the value of it is "software", the parameter is "meta_category=software".
        /// </summary>
        public MetaData meta { get; set; }


        /// <summary>
        /// Optional(Breakout Room).
        /// If set to true, the client will give the user the choice to choose the breakout rooms he wants to join.
        /// </summary>
        public bool? freeJoin { get; set; }

        /// <summary>
        /// Whether to automatically start recording when first user joins (default false).
        /// When this parameter is true, the recording UI in BigBlueButton will be initially active.Moderators in the session can still pause and restart recording using the UI control.
        /// NOTE: Don’t pass autoStartRecording= false and allowStartStopRecording = false - the moderator won’t be able to start recording!
        /// </summary>
        public bool? autoStartRecording { get; set; }

        /// <summary>
        /// Allow the user to start/stop recording. (default true)
        /// If you set both allowStartStopRecording=false and autoStartRecording = true, then the entire length of the session will be recorded, and the moderators in the session will not be able to pause/resume the recording.
        /// </summary>
        public bool? allowStartStopRecording { get; set; }

        /// <summary>
        /// Setting webcamsOnlyForModerator=true will cause all webcams shared by viewers during this meeting to only appear for moderators.
        /// </summary>
        public bool? webcamsOnlyForModerator { get; set; }

        /// <summary>
        /// Optional.
        /// Setting logo=http://www.example.com/my-custom-logo.png will replace the default logo in the Flash client. (added 2.0)
        /// </summary>
        public string logo { get; set; }

        /// <summary>
        /// Optional.
        /// Will set the banner text in the client. (added 2.0)
        /// </summary>
        public string bannerText { get; set; }

        /// <summary>
        /// Optional.
        /// Will set the banner background color in the client. The required format is color hex #FFFFFF. (added 2.0)
        /// </summary>
        public string bannerColor { get; set; }

        /// <summary>
        /// Optional.
        /// Setting copyright=My custom copyright will replace the default copyright on the footer of the Flash client. (added 2.0)
        /// </summary>
        public string copyright { get; set; }

        /// <summary>
        /// Optional.
        /// Setting muteOnStart=true will mute all users when the meeting starts. 
        /// </summary>
        public bool? muteOnStart { get; set; }

        /// <summary>
        /// Optional.
        /// Default allowModsToUnmuteUsers=false. Setting to allowModsToUnmuteUsers=true will allow moderators to unmute other users in the meeting. (added 2.2)
        /// </summary>
        public bool? allowModsToUnmuteUsers { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsDisableCam=false. Setting lockSettingsDisableCam=true will prevent users from sharing their camera in the meeting. 
        /// </summary>
        public bool? lockSettingsDisableCam { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsDisableMic=false. Setting to lockSettingsDisableMic=true will only allow user to join listen only.
        /// </summary>
        public bool? lockSettingsDisableMic { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsDisablePrivateChat=false. Setting to lockSettingsDisablePrivateChat=true will disable private chats in the meeting. 
        /// </summary>
        public bool? lockSettingsDisablePrivateChat { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsDisablePublicChat=false. Setting to lockSettingsDisablePublicChat=true will disable public chat in the meeting. 
        /// </summary>
        public bool? lockSettingsDisablePublicChat { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsDisableNote=false. Setting to lockSettingsDisableNote=true will disable notes in the meeting. (added 2.2)
        /// </summary>
        public bool? lockSettingsDisableNote { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsLockedLayout=false. Setting to lockSettingsLockedLayout=true will lock the layout in the meeting. (added 2.2)
        /// </summary>
        public bool? lockSettingsLockedLayout { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsLockOnJoin=true. Setting to lockSettingsLockOnJoin=false will not apply lock setting to users when they join. (added 2.2)
        /// </summary>
        public bool? lockSettingsLockOnJoin { get; set; }

        /// <summary>
        /// Optional.
        /// Default lockSettingsLockOnJoinConfigurable=false. Setting to lockSettingsLockOnJoinConfigurable=true will allow applying of lockSettingsLockOnJoin param. (added 2.2)
        /// </summary>
        public bool? lockSettingsLockOnJoinConfigurable { get; set; }

        /// <summary>
        /// Optional.
        /// Default guestPolicy=ALWAYS_ACCEPT. Will set the guest policy for the meeting. The guest policy determines whether or not users who send a join request with guest=true will be allowed to join the meeting. Possible values are ALWAYS_ACCEPT, ALWAYS_DENY, and ASK_MODERATOR.
        /// </summary>
        public string guestPolicy { get; set; }
    }
}