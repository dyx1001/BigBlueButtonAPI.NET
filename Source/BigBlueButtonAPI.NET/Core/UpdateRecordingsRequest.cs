/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using BigBlueButtonAPI.Common;

namespace BigBlueButtonAPI.Core
{
    public class UpdateRecordingsRequest:BaseRequest
    {
        /// <summary>
        /// Required.
        /// A record ID for specify the recordings to apply the publish action. It can be a set of record IDs separated by commas.
        /// </summary>
        public string recordID { get; set; }

        /// <summary>
        /// Required.
        /// You can pass one or more metadata values to be updated.
        /// </summary>
        public MetaData meta { get; set; }
    }
}