/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBlueButtonAPI.Core
{
    /// <summary>
    /// Indicates whether the intended function was successful or not.
    /// </summary>
    public enum Returncode
    {
        /// <summary>
        /// The call succeeded – the other parameters that are normally associated with this call will be returned.
        /// </summary>
        SUCCESS=0,

        /// <summary>
        /// There was an error of some sort – look for the message and messageKey for more information. 
        /// </summary>
        FAILED=1
    }
}