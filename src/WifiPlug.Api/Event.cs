// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents a received event.
    /// </summary>
    public class Event
    {
        #region Properties
        public string Name { get; private set; }
        public string ResourceType { get; private set; }
        public string Resource { get; private set; }
        public JObject Payload { get; private set; }
        #endregion

        #region Constructors

        #endregion
    }
}
