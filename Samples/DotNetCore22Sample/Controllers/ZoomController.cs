using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigBlueButtonAPI.Core;
using DotNetCore22Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace DotNetCore22Sample.Controllers
{
    public class ZoomController : Controller
    {
        private readonly BigBlueButtonAPIClient client;
        public ZoomController(BigBlueButtonAPIClient client)
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

        [HttpGet]
        public async Task<ActionResult> Start()
        {
            var setupOk = await isBigBlueButtonAPISettingsOKAsync();
            if (setupOk)
            {
                var Id = Guid.NewGuid().ToString();
                var model = new StartModel
                {
                    Id = Id,
                    Url = Url.Action("Join", "Zoom", new { Id = Id }, Request.Scheme),
                    Name = ""
                };
                return View(model);
            }
            return View();
        }

        /// <summary>
        /// Creates an meeting and join it.
        /// </summary>
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


        [HttpGet]
        public ActionResult Join(string Id)
        {
            var model = new JoinModel
            {
                Id = Id,
                Name = ""
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Join(JoinModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var response = await client.GetMeetingInfoAsync(new GetMeetingInfoRequest
            {
                meetingID = model.Id
            });
            if (response.returncode == Returncode.FAILED)
            {
                ModelState.AddModelError("", "The meeting is not running.");
                return View(model);
            }
            if (response.running == false)
            {
                if (response.hasUserJoined == true)
                {
                    ModelState.AddModelError("", "The meeting is ended.");
                }
                else
                {
                    ModelState.AddModelError("", "The meeting is not started.");
                }

                return View(model);
            }
            var url = client.GetJoinMeetingUrl(new JoinMeetingRequest
            {
                meetingID = model.Id,
                fullName = model.Name,
                password = response.attendeePW
            });
            return Redirect(url);
        }

    }
}