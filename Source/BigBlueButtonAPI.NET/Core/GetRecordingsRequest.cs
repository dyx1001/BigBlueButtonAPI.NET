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
    public class GetRecordingsRequest:BaseRequest
    {
        /// <summary>
        /// Optional.
        /// A meeting ID for get the recordings. It can be a set of meetingIDs separate by commas. If the meeting ID is not specified, it will get ALL the recordings. If a recordID is specified, the meetingID is ignored.
        /// </summary>
        public string meetingID { get; set; }

        /// <summary>
        /// Optional.
        /// A record ID for get the recordings. It can be a set of recordIDs separate by commas. If the record ID is not specified, it will use meeting ID as the main criteria. If neither the meeting ID is specified, it will get ALL the recordings. The recordID can also be used as a wildcard by including only the first characters in the string.
        /// </summary>
        public string recordID { get; set; }

        /// <summary>
        /// Optional.
        /// Since version 1.0 the recording has an attribute that shows a state that Indicates if the recording is [processing|processed|published|unpublished|deleted]. The parameter state can be used to filter results. It can be a set of states separate by commas. If it is not specified only the states [published|unpublished] are considered (same as in previous versions). If it is specified as “any”, recordings in all states are included.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// You can pass one or more metadata values to filter the recordings returned. The format of these parameters is the same as the metadata passed to the create call. For more information see the docs for the create call.
        /// </summary>
        public MetaData meta { get; set; }
    }
}