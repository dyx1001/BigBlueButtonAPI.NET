using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BigBlueButtonAPI.Core;
using BigBlueButtonAPI.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotNetCore22Sample.Controllers
{
    public class SampleController : Controller
    {
        private readonly BigBlueButtonAPIClient client;
        public SampleController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// It ensures the settings of BigBlueButton is ok. 
        /// It just helps you run the demo normally. In product environment, this method is not needed.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> isBigBlueButtonAPISettingsOKAsync()
        {
            try
            {
                var res = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest { meetingID = Guid.NewGuid().ToString() });
                if (res.returncode == Returncode.FAILED) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ActionResult> Index()
        {
            var setupOk = await isBigBlueButtonAPISettingsOKAsync();
            if (setupOk)
            {
                var result = await client.GetMeetingsAsync();
                return View(result);
            }
            return View();
        }
        public async Task<ActionResult> GetMeetingInfo(string meetingID)
        {
            var result = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest { meetingID = meetingID });
            return Json(result);
        }
        public async Task<ActionResult> Recordings()
        {
            var setupOk = await isBigBlueButtonAPISettingsOKAsync();
            if (setupOk)
            {
                var result = await client.GetRecordingsAsync();
                return View(result);
            }
            return View();
        }
        public async Task<ActionResult> PublishRecordings(string recordID, string type)
        {
            var request = new PublishRecordingsRequest
            {
                recordID = recordID,
                publish = type == "1"
            };
            var result = await client.PublishRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }
        public ActionResult Error(IBaseResponse response)
        {
            return View(response);
        }
        public async Task<ActionResult> DeleteRecordings(string recordID)
        {
            var request = new DeleteRecordingsRequest
            {
                recordID = recordID
            };
            var result = await client.DeleteRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }
        public async Task<ActionResult> UpdateRecordings(string recordID)
        {
            var request = new UpdateRecordingsRequest
            {
                recordID = recordID,
                meta = new MetaData { { "customdata", DateTime.Now.Ticks.ToString() } }
            };
            var result = await client.UpdateRecordingsAsync(request);

            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Recordings");
        }

        public async Task<ActionResult> GetDefaultConfigXML()
        {
            var result = await client.GetDefaultConfigXMLAsync();
            return Content(result, "text/xml");
        }


        public async Task<ActionResult> IsMeetingRunning(string meetingID)
        {
            var result = await client.IsMeetingRunningAsync(new IsMeetingRunningRequest { meetingID = meetingID });
            return Json(result);
        }

        public async Task<ActionResult> Tracks(string recordID)
        {
            var result = await client.GetRecordingTextTracksAsync(new GetRecordingTextTracksRequest { recordID = recordID });
            return Json(result);
        }
        public async Task<ActionResult> PutTrack(string recordID)
        {
            var request = new PutRecordingTextTrackRequest
            {
                recordID = recordID,
                kind = "subtitles",// "captions",
                label = "English",
                lang = "en"
            };

            var webVTT = "WEBVTT - Some title\n\n00:00.000 --> 00:04.000\nHello\n\n00:04.000 --> 00:08.000\nWorld\n\n\n";
            var fileData = Encoding.UTF8.GetBytes(webVTT);

            request.file = new FileContentData
            {
                Name = "file",
                FileName = "a.vtt",
                FileData = fileData
            };
            var result = await client.PutRecordingTextTrackAsync(request);
            return Json(result);
        }
        public async Task<ActionResult> Create()
        {
            var result = await client.CreateMeetingAsync(new CreateMeetingRequest
            {
                name = "Test Meeting",
                meetingID = "TestMeeting001",
                record = true
            });
            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> End(string meetingID, string pass)
        {
            var result = await client.EndMeetingAsync(new EndMeetingRequest
            {
                meetingID = meetingID,
                password = pass
            });
            if (result.returncode == Returncode.FAILED) return View("Error", result);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// role=0 or other: Attendee
        /// role=1: Moderator
        /// token=0 or other: No config token
        /// token=1: Has config token
        /// </summary>
        /// <param name="meetingID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ActionResult> Join(string meetingID, string role, string pass, string token)
        {
            var requestJoin = new JoinMeetingRequest { meetingID = meetingID };
            if (role == "1")
            {
                requestJoin.password = pass;
                requestJoin.userID = "10000";
                requestJoin.fullName = "Admin";
            }
            else
            {
                requestJoin.password = pass;
                requestJoin.userID = "20000";
                requestJoin.fullName = "User";
            }
            if (token == "1")
            {
                var setConfigRequest = new SetConfigXMLRequest
                {
                    meetingID = meetingID,
                    configXML = "<config><modules><localeversion supressWarning=\"false\">0.9.0</localeversion></modules></config>"
                };
                var setConfigResult = await client.SetConfigXMLAsync(setConfigRequest);
                if (setConfigResult.returncode == Returncode.FAILED) return View("Error", setConfigResult);
                requestJoin.configToken = setConfigResult.configToken;
            }

            var url = client.GetJoinMeetingUrl(requestJoin);
            return Redirect(url);
        }




    }
}