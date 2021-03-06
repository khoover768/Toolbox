﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Khooversoft.Toolbox
{
    /// <summary>
    /// Identifies the level of an event.
    /// </summary>
    public enum TelemetryLevel
    {
        None = 0,

        /// <summary>
        /// Critical error
        /// </summary>
        Critical = 1,

        /// <summary>
        /// Error, usually recoverable
        /// </summary>
        Error = 2,

        /// <summary>
        /// Warning message
        /// </summary>
        Warning = 3,

        /// <summary>
        /// General purpose information
        /// </summary>
        Informational = 4,

        /// <summary>
        /// Verbose level, this level adds lengthy events or messages. It causes all events to be logged.
        /// </summary>
        Verbose = 5,

        /// <summary>
        /// Metric type
        /// </summary>
        Metric = 10,
    }
}
