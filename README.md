# BigBlueButtonAPI.NET - BigBlueButton API .NET Standard SDK
It helps the .NET Framework application or the .NET Core application integrate with BigBlueButton API, **quickly and easily**.

What is BigBlueButton?

[BigBlueButton](http://bigbluebutton.org) is an open source web conferencing system for online learning.
## BigBlueButtonAPI.NET Features:
- It is built on **.NET Standard 1.3**. Any .NET platform that implements .NET Standard 1.3 can use it (.NET Framework 4.6 and higher, .NET Core 1.0 and higher, etc.).
- It supports **all of the latest BigBlueButton API 2.2**:
  - **Administration**
  
  |API|Description|
  |--|--|
  |create|Creates a new meeting.|
  |getDefaultConfigXML|Gets the default config.xml (these settings configure the BigBlueButton client for each user).|
  |setConfigXML|Adds a custom config.xml to an existing meeting.|
  |join|Joins a new user to an existing meeting.|
  |end|Ends meeting.|

  - **Monitoring**
  
  |API|Description|
  |--|--|
  |isMeetingRunning|Checks whether if a specified meeting is running.|
  |getMeetings|Gets the list of Meetings.|
  |getMeetingInfo|Gets the details of a Meeting.|
  
  - **Recording**
  
  |API|Description|
  |--|--|
  |getRecordings|Gets a list of recordings.|
  |publishRecordings|Enables publishing or unpublishing of a recording.|
  |deleteRecordings|Deletes an existing recording.|
  |updateRecordings|Updates metadata in a recording.|
  |getRecordingTextTracks|Gets a list of the caption/subtitle.|
  |putRecordingTextTrack|Uploads a caption or subtitle file to add it to the recording.|
  
  - Please reference the following document: [http://docs.bigbluebutton.org/dev/api.html](http://docs.bigbluebutton.org/dev/api.html) 
  
- The **BigBlueButtonAPI.Core.BigBlueButtonAPIClient** class provides functions to call the BigBlueButton APIs.
  - The public methods:
  
  |API|Method|
  |--|--|
  |create|`public async Task<CreateMeetingResponse> CreateMeetingAsync(CreateMeetingRequest request)`|
  |getDefaultConfigXML|`public async Task<string> GetDefaultConfigXMLAsync(GetDefaultConfigXMLRequest request = null)`|
  |setConfigXML|`public async Task<SetConfigXMLResponse> SetConfigXMLAsync(SetConfigXMLRequest request)`|
  |join|`public string GetJoinMeetingUrl(JoinMeetingRequest request)`|
  |end|`public async Task<EndMeetingResponse> EndMeetingAsync(EndMeetingRequest request)`|
  |isMeetingRunning|`public async Task<IsMeetingRunningResponse> IsMeetingRunningAsync(IsMeetingRunningRequest request)`|
  |getMeetings|`public async Task<GetMeetingsResponse> GetMeetingsAsync(GetMeetingsRequest request = null)`|
  |getMeetingInfo|`public async Task<GetMeetingInfoResponse> GetMeetingInfoAsync(GetMeetingInfoRequest request)`|
  |getRecordings|`public async Task<GetRecordingsResponse> GetRecordingsAsync(GetRecordingsRequest request=null)`|
  |publishRecordings|`public async Task<PublishRecordingsResponse> PublishRecordingsAsync(PublishRecordingsRequest request)`|
  |deleteRecordings|`public async Task<DeleteRecordingsResponse> DeleteRecordingsAsync(DeleteRecordingsRequest request)`|
  |updateRecordings|`public async Task<UpdateRecordingsResponse> UpdateRecordingsAsync(UpdateRecordingsRequest request)`|
  |getRecordingTextTracks|`public async Task<GetRecordingTextTracksResponse> GetRecordingTextTracksAsync(GetRecordingTextTracksRequest request)`|
  |putRecordingTextTrack|`public async Task<PutRecordingTextTrackResponse> PutRecordingTextTrackAsync(PutRecordingTextTrackRequest request)`|
  
  > Each method has a **XXXRequest** input parameter.  
  >  
  > Most of methods return Task &lt;XXXResponse&gt;, it contains the result data or error data: If the BigBlueButton API meets errors, the **returncode** of the response equals to **Returncode.FAILED**; the **messageKey** of the response is the error code; the **message** of the response is the error message.
  >
  > Only the **GetDefaultConfigXMLAsync** method and the **GetJoinMeetingUrl** method return string.
  
  
  
```csharp
public async Task<CreateMeetingResponse> CreateMeetingAsync(CreateMeetingRequest request)
```
