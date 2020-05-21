/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
namespace BigBlueButtonAPI.Core
{
    public class PutRecordingTextTrackRequest : BasePostFileRequest
    {
        /// <summary>
        /// A single recording ID to retrieve the available captions for. (Unlike other recording APIs, you cannot provide a comma-separated list of recordings.)
        /// </summary>
        public string recordID {get;set;}

        /// <summary>
        /// Indicates the intended use of the text track. See the getRecordingTextTracks description for details. Using a value other than one listed in this document will cause an error to be returned.
        /// </summary>
        public string kind { get; set; }

        /// <summary>
        /// Indicates the intended use of the text track. See the getRecordingTextTracks description for details. Using a value other than one listed in this document will cause an error to be returned.
        /// </summary>
        public string lang { get; set; }


        /// <summary>
        /// A human-readable label for the text track. If not specified, the system will automatically generate a label containing the name of the language identified by the lang parameter.
        /// </summary>
        public string label { get; set; }
    }
}