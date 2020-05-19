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
  
  - The constructor method:
  ```csharp
  public BigBlueButtonAPIClient(BigBlueButtonAPISettings settings, HttpClient httpClient)
  ```
  > The **BigBlueButtonAPI.Core.BigBlueButtonAPISettings** class contains the config data for the BigBlueButton API:
  >> The **ServerAPIUrl** property: The BigBlueButton server API endpoint (usually the server’s hostname followed by **/bigbluebutton/api/**, for example: http://yourserver.com/bigbluebutton/api/ ).
  >>
  >> The **SharedSecret** property: The shared secret code that is needed for the BigBlueButton server API. You can retrieve it using the command in your BigBlueButton server:  
  `$ bbb-conf --secret`
- **It makes some enhancement: meta, recording, etc.**
## Quickstart
- **BigBlueButtonAPI.NET** is built on .NET Standard 1.3. It depends these packages:
  - NETStandard.Library 1.6.1
  - Newtonsoft.Json 10.0.1
  - System.Reflection.TypeExtensions 4.3.0
  - System.Xml.XmlSerializer 4.3.0
- The source project and samples projects are built by VS 2019.
- How to use it in your ASP.NET Core project?
  - Add the reference **BigBlueButtonAPI.NET.dll** to your project.
  - If your project doesn't reference the NuGet Package **Newtonsoft.Json 10.0.1** or higher, please add reference.
- How to use it in your ASP.NET project?
  - You project should target .NET Framework 4.6 or higher.
  - Add the reference **BigBlueButtonAPI.NET.dll** to your project.
  - If your project doesn't reference the NuGet Package **Newtonsoft.Json 10.0.1** or higher, please add reference.
  - If your project doesn't reference the NuGet Package **System.Reflection.TypeExtensions 4.3.0** or higher, please add reference.
- Code Sample  
Let's start a meeting (create a meeting and join it), **client** is the instance of **BigBlueButtonAPIClient**.  
  ```csharp
  [HttpPost]
  public async Task<ActionResult> Start(StartModel model)
  {
      if (!ModelState.IsValid) return View(model);

      //1. Create a meeting
      var responseCreateMeeting = await client.CreateMeetingAsync(new CreateMeetingRequest
      {
          name = "Test Meeting",
          meetingID = model.Id
      });
      //Check the response from the BigBlueButton server and return error if has error.
      if (responseCreateMeeting.returncode == Returncode.FAILED)
      {
          ModelState.AddModelError("", responseCreateMeeting.message);
          return View(model);
      }

      //2. Join the meeting as moderator
      var url = client.GetJoinMeetingUrl(new JoinMeetingRequest
      {
          meetingID = model.Id,
          fullName = model.Name,
          password = responseCreateMeeting.moderatorPW
      });
      return Redirect(url);
  }
  ```
