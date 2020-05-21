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
    /// <summary>
    /// The interface of the base response.
    /// </summary>
    public interface IBaseResponse
    {
        /// <summary>
        /// Indicates whether the intended function was successful or not.
        /// </summary>
        Returncode returncode { get; set; }

        /// <summary>
        /// Provides similar functionality to the message and follows the same rules. 
        /// However, a message key will be much shorter and will generally remain the same for the life of the API whereas a message may change over time. 
        /// If your third party application would like to internationalize or otherwise change the standard messages returned, you can look up your own custom messages based on this messageKey.
        /// </summary>
        string messageKey { get; set; }

        /// <summary>
        /// A message that gives additional information about the status of the call. 
        /// A message parameter will always be returned if the returncode was FAILED. 
        /// A message may also be returned in some cases where returncode was SUCCESS if additional information would be helpful.
        /// </summary>
        string message { get; set; }
    }
}