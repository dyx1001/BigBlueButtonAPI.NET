/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBlueButtonAPI.Common
{
    public class RecordingTrack
    {
        /// <summary>
        /// A link to download this text track file. The format will always be WebVTT (text/vtt mime type), which is similar to the SRT format.
        /// The timing of the track will match the current recording playback video and audio files.Note that if the recording is edited(adjusting in/out markers), tracks from live or automatic sources will be re-created with the new timing.Uploaded tracks will be edited, but this may result in data loss if sections of the recording are removed during edits.
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// Indicates the intended use of the text track. The value will be one of the following strings: subtitles captions
        /// The meaning of these values is defined by the HTML5 video element, see the MDN docs for details.Note that the HTML5 specification defines additional values which are not currently used here, but may be added at a later date.
        /// </summary>
        public string kind { get; set; }

        /// <summary>
        /// A human-readable label for the text track. This is the string displayed in the subtitle selection list during recording playback.
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// The language of the text track, as a language tag. See RFC 5646 for details on the format, and the Language subtag lookup for assistance using them. It will usually consist of a 2 or 3 letter language code in lowercase, optionally followed by a dash and a 2-3 letter geographic region code (country code) in uppercase.
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// Indicates where the track came from. The value will be one of the following strings:
        ///   live - A caption track derived from live captioning performed in a BigBlueButton.
        ///   automatic - A caption track generated automatically via computer voice recognition.
        ///   upload - A caption track uploaded by a 3rd party.
        /// </summary>
        public string source { get; set; }
    }
}