using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Converters
{
    /// <summary>
    /// Downgrades any ISO 8061 to not format milliseconds.
    /// </summary>
    internal sealed class InaccurateIsoDateTimeConverter : IsoDateTimeConverter
    {
        public InaccurateIsoDateTimeConverter() {
            DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
        }
    }
}
