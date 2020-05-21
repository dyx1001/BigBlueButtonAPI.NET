/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
namespace BigBlueButtonAPI.Core
{
    /// <summary>
    /// The request data of joining a user to the meeting specified in the meetingID parameter.
    /// </summary>
    public class JoinMeetingRequest:BaseRequest
    {
        /// <summary>
        /// Required.
        /// The full name that is to be used to identify this user to other conference attendees.
        /// </summary>
        public string fullName { get; set; }

        /// <summary>
        /// Required.
        /// The meeting ID that identifies the meeting you are attempting to join.
        /// </summary>
        public string meetingID { get; set; }

        /// <summary>
        /// Required.
        /// The password that this attendee is using. 
        /// If the moderator password is supplied, he will be given moderator status (and the same for attendee password, etc).
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Optional.
        /// Third-party apps using the API can now pass createTime parameter (which was created in the create call), BigBlueButton will ensure it matches the ‘createTime’ for the session. If they differ, BigBlueButton will not proceed with the join request. This prevents a user from reusing their join URL for a subsequent session with the same meetingID.
        /// </summary>
        public long? createTime { get; set; }

        /// <summary>
        /// Optional.
        /// An identifier for this user that will help your application to identify which person this is. 
        /// This user ID will be returned for this user in the getMeetingInfo API call so that you can check.
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// Optional.
        /// If you want to pass in a custom voice-extension when a user joins the voice conference using voip. This is useful if you want to collect more info in you Call Detail Records about the user joining the conference. You need to modify your /etc/asterisk/bbb-extensions.conf to handle this new extensions.
        /// </summary>
        public string webVoiceConf { get; set; }

        /// <summary>
        /// Optional.
        /// The token returned by a setConfigXML API call. This causes the BigBlueButton client to load the config.xml associated with the token (not the default config.xml).
        /// </summary>
        public string configToken { get; set; }

        /// <summary>
        /// Optional.
        /// The layout name to be loaded first when the application is loaded.
        /// </summary>
        public string defaultLayout { get; set; }

        /// <summary>
        /// Optional.
        /// The link for the user’s avatar to be displayed when displayAvatar in config.xml is set to true (not yet implemented in the HTML5 client, see #8566).
        /// </summary>
        public string avatarURL { get; set; }


        /// <summary>
        /// Optional (Experimental).
        /// The default behaviour of the JOIN API is to redirect the browser to the Flash client when the JOIN call succeeds. 
        /// There have been requests if it’s possible to embed the Flash client in a “container” page and that the client starts as a hidden DIV tag which becomes visible on the successful JOIN. 
        /// Setting this variable to FALSE will not redirect the browser but returns an XML instead whether the JOIN call has succeeded or not. The third party app is responsible for displaying the client to the user.
        /// </summary>
        public bool? redirect { get; set; }

        /// <summary>
        /// Optional (Experimental).
        /// Some third party apps what to display their own custom client. These apps can pass the URL containing the custom client and when redirect is not set to false, the browser will get redirected to the value of clientURL.
        /// </summary>
        public string clientURL { get; set; }

        /// <summary>
        /// Optional.
        /// Set to “true” to force the HTML5 client to load for the user.
        /// </summary>
        public bool? joinViaHtml5 { get; set; }

        /// <summary>
        /// Optional.
        /// Set to “true” to indicate that the user is a guest.
        /// </summary>
        public bool? guest { get; set; }
    }
}