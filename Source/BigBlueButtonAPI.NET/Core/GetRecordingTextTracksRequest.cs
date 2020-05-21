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
    public class GetRecordingTextTracksRequest:BaseRequest
    {
        /// <summary>
        /// A single recording ID to retrieve the available captions for. (Unlike other recording APIs, you cannot provide a comma-separated list of recordings.)
        /// </summary>
        public string recordID { get; set; }
    }
}