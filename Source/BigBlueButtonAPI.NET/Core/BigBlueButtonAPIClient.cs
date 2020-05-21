/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using BigBlueButtonAPI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigBlueButtonAPI.Core
{
    /// <summary>
    /// The client class to make an API call to your BigBlueButton server.
    /// Please reference the following document:
    /// http://docs.bigbluebutton.org/dev/api.html
    /// </summary>
    public class BigBlueButtonAPIClient
    {
        #region Common
        private readonly HttpClient httpClient;
        private readonly UrlBuilder urlBuilder;

        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="settings">The BigBlueButtonAPI.Core.BigBlueButtonAPISettings class contains the config data for the BigBlueButton API</param>
        /// <param name="httpClient">The HttpClient instance</param>
        public BigBlueButtonAPIClient(BigBlueButtonAPISettings settings, HttpClient httpClient)
        {
            this.urlBuilder = new UrlBuilder(settings);
            this.httpClient = httpClient;
        }

        private async Task<T> HttpGetAsync<T>(string method, BaseRequest request)
        {
            var url = urlBuilder.Build(method, request);
            var result = await HttpGetAsync<T>(url);
            return result;
        }
        private async Task<T> HttpGetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            var xmlOrJson = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

            // Most APIs return XML string.
            // The getRecordingTextTracks API may return JSON string.
            if (xmlOrJson.StartsWith("<"))
            {
                return XmlHelper.FromXml<T>(xmlOrJson);
            }
            else
            {
                var wrapper= JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                return wrapper.response;
            }
        }

        private async Task<T> HttpPostAsync<T>(string method, BaseRequest request)
        {
            var formData = urlBuilder.Build(method, request, true);
            var formDataBytes= System.Text.Encoding.UTF8.GetBytes(formData);
            
            var cts = new CancellationTokenSource();
            using (var content = new ByteArrayContent(formDataBytes))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(urlBuilder.BuildMethodUrl(method), content, cts.Token);
                var xmlOrJson = await response.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

                // Most APIs return XML string.
                // The getRecordingTextTracks API may return JSON string.
                if (xmlOrJson.StartsWith("<"))
                {
                    return XmlHelper.FromXml<T>(xmlOrJson);
                }
                else
                {
                    var wrapper = JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                    return wrapper.response;
                }
                
            }

        }
        private async Task<T> HttpPostFileAsync<T>(string method, BasePostFileRequest request)
        {
            var url= urlBuilder.Build(method, request);
            var cts = new CancellationTokenSource();
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new ByteArrayContent(request.file.FileData), request.file.Name, request.file.FileName);

                var response = await httpClient.PostAsync(url, content, cts.Token);
                var xmlOrJson = await response.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string)) return (T)(object)xmlOrJson;

                // Most APIs return XML string.
                // The getRecordingTextTracks API may return JSON string.
                if (xmlOrJson.StartsWith("<"))
                {
                    return XmlHelper.FromXml<T>(xmlOrJson);
                }
                else
                {
                    var wrapper = JsonConvert.DeserializeObject<ResponseJsonWrapper<T>>(xmlOrJson);
                    return wrapper.response;
                }

            }

        }
        #endregion

        #region create
        /// <summary>
        /// Creates a new meeting.
        /// The create call is idempotent: you can call it multiple times with the same parameters without side effects. This simplifies the logic for joining a user into a session as your application can always call create before returning the join URL to the user. This way, regardless of the order in which users join, the meeting will always exist when the user tries to join (the first create call actually creates the meeting; subsequent calls to create simply return SUCCESS).
        /// The BigBlueButton server will automatically remove empty meetings that were created but have never had any users after a number of minutes specified by meetingExpireIfNoUserJoinedInMinutes defined in bigbluebutton.properties.
        /// </summary>
        /// <param name="request">The request data</param>
        /// <returns></returns>
        public async Task<CreateMeetingResponse> CreateMeetingAsync(CreateMeetingRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            //if (string.IsNullOrEmpty(request.attendeePW)) request.attendeePW = HashHelper.Sha1Hash(request.meetingID + "attendeePW");
            //if (string.IsNullOrEmpty(request.moderatorPW)) request.moderatorPW = HashHelper.Sha1Hash(request.meetingID + "moderatorPW");

            return await HttpGetAsync<CreateMeetingResponse>("create", request);
        }
        #endregion

        #region getDefaultConfigXML
        /// <summary>
        /// Gets the default config.xml (these settings configure the BigBlueButton client for each user).
        /// Retrieve the default config.xml. This call enables a 3rd party application to get the current config.xml, modify it’s parameters, and use setConfigXML to store it on the BigBlueButton server (getting a reference token to the new config.xml), then using the token in as a parameter in the join URL to override the default config.xml.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetDefaultConfigXMLAsync(GetDefaultConfigXMLRequest request = null)
        {
            return await HttpGetAsync<string>("getDefaultConfigXML", request);
        }
        #endregion

        #region setConfigXML
        /// <summary>
        /// Add a custom config.xml to an existing meeting.
        /// Associate a custom config.xml file with the current session. This call returns a token that can later be passed as a parameter to a join URL. When passed as a parameter, the BigBlueButton client will use the associated config.xml for the user instead of using the default config.xml. This enables 3rd party applications to provide user-specific config.xml files.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SetConfigXMLResponse> SetConfigXMLAsync(SetConfigXMLRequest request)
        {
            return await HttpPostAsync<SetConfigXMLResponse>("setConfigXML", request);
        }
        #endregion

        #region join
        /// <summary>
        /// Join a new user to an existing meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetJoinMeetingUrl(JoinMeetingRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            // If the redirect is set as false, the reponse will be a XML, but there is 401 error when use the URL in the XML.
            // Maybe BigBlueButton API set some cookies at the same time. 
            // So I set redirect property is false.
            if (request.redirect == false) request.redirect = true;

            return urlBuilder.Build("join", request);
        }
        #endregion

        #region end
        /// <summary>
        /// Ends meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EndMeetingResponse> EndMeetingAsync(EndMeetingRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<EndMeetingResponse>("end", request);
        }
        #endregion

        #region isMeetingRunning
        /// <summary>
        /// Checks whether if a specified meeting is running.
        /// </summary>
        /// <param name="request">The request data</param>
        /// <returns></returns>
        public async Task<IsMeetingRunningResponse> IsMeetingRunningAsync(IsMeetingRunningRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<IsMeetingRunningResponse>("isMeetingRunning", request);
        }
        #endregion

        #region getMeetings
        /// <summary>
        /// Get the list of Meetings.
        /// </summary>
        /// <returns></returns>
        public async Task<GetMeetingsResponse> GetMeetingsAsync(GetMeetingsRequest request = null)
        {
            return await HttpGetAsync<GetMeetingsResponse>("getMeetings", request);
        }
        #endregion

        #region getMeetingInfo
        /// <summary>
        /// Get the details of a Meeting.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetMeetingInfoResponse> GetMeetingInfoAsync(GetMeetingInfoRequest request)
        {
            return await HttpGetAsync<GetMeetingInfoResponse>("getMeetingInfo", request);
        }
        #endregion


        #region getRecordings
        /// <summary>
        /// Get a list of recordings.
        /// Retrieves the recordings that are available for playback for a given meetingID (or set of meeting IDs).
        /// </summary>
        /// <returns></returns>
        public async Task<GetRecordingsResponse> GetRecordingsAsync(GetRecordingsRequest request=null)
        {
            var result= await HttpGetAsync<GetRecordingsResponse>("getRecordings", request);

            //The url may contain leading and trailing white-space characters.
            if (result.recordings !=null && result.recordings.Count>0)
            {
                foreach(var recording in result.recordings)
                {
                    if (recording.playbacks != null && recording.playbacks.Count > 0)
                    {
                        foreach(var f in recording.playbacks)
                        {
                            if (f.url != null) f.url = f.url.Trim();
                            if (f.previewImages !=null && f.previewImages.Count>0)
                            {
                                foreach(var image in f.previewImages)
                                {
                                    if (image.url != null) image.url = image.url.Trim();
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region publishRecordings
        /// <summary>
        /// Enables publishing or unpublishing of a recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PublishRecordingsResponse> PublishRecordingsAsync(PublishRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<PublishRecordingsResponse>("publishRecordings", request);
        }
        #endregion

        #region deleteRecordings
        /// <summary>
        /// Deletes an existing recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteRecordingsResponse> DeleteRecordingsAsync(DeleteRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<DeleteRecordingsResponse>("deleteRecordings", request);
        }
        #endregion

        #region updateRecordings
        /// <summary>
        /// Updates metadata in a recording.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UpdateRecordingsResponse> UpdateRecordingsAsync(UpdateRecordingsRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<UpdateRecordingsResponse>("updateRecordings", request);
        }
        #endregion

        #region getRecordingTextTracks
        /// <summary>
        /// Get a list of the caption/subtitle.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetRecordingTextTracksResponse> GetRecordingTextTracksAsync(GetRecordingTextTracksRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpGetAsync<GetRecordingTextTracksResponse>("getRecordingTextTracks", request);
        }
        #endregion

        #region putRecordingTextTrack
        /// <summary>
        /// Upload a caption or subtitle file to add it to the recording.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileContentData"></param>
        /// <returns></returns>
        public async Task<PutRecordingTextTrackResponse> PutRecordingTextTrackAsync(PutRecordingTextTrackRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return await HttpPostFileAsync<PutRecordingTextTrackResponse>("putRecordingTextTrack", request);
        }
        #endregion

    }
}