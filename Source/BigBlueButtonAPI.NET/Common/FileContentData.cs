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
    public class FileContentData
    {
        /// <summary>
        /// The name of the HTTP content.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The file name for the HTTP content.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file data.
        /// </summary>
        public byte[] FileData { get; set; }
    }
}