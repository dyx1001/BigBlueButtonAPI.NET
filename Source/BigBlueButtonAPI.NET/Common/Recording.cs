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
    public class Recording
    {
        public string recordID { get; set; }
        public string meetingID { get; set; }
        public string internalMeetingID { get; set; }
        public string name { get; set; }
        public bool isBreakout { get; set; }
        public bool published { get; set; }
        public string state { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public int participants { get; set; }
        public int rawSize { get; set; }
        public MetaData metadata { get; set; }
        public int size { get; set; }

        [XmlArray("playback")]
        [XmlArrayItem("format")]
        public List<Playback> playbacks { get; set; }

        public RecordingData data { get; set; }
    }
}