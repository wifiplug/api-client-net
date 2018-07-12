using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Converters
{
    /// <summary>
    /// Provides functionality to convert between a repetition enum and JSON.
    /// </summary>
    internal class TimerRepetitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(TimerRepetition);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            // load next token
            JToken nextToken = JToken.Load(reader);

            if (nextToken.Type != JTokenType.Object)
                return null;

            // build repetition
            JObject obj = (JObject)nextToken;
            TimerRepetition repetition = TimerRepetition.None;

            if ((bool)obj["monday"])
                repetition |= TimerRepetition.Monday;
            if ((bool)obj["tuesday"])
                repetition |= TimerRepetition.Tuesday;
            if ((bool)obj["wednesday"])
                repetition |= TimerRepetition.Wednesday;
            if ((bool)obj["thursday"])
                repetition |= TimerRepetition.Thursday;
            if ((bool)obj["friday"])
                repetition |= TimerRepetition.Friday;
            if ((bool)obj["saturday"])
                repetition |= TimerRepetition.Saturday;
            if ((bool)obj["sunday"])
                repetition |= TimerRepetition.Sunday;

            return repetition;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            // cast
            TimerRepetition repetition = (TimerRepetition)value;

            // create object
            JObject obj = new JObject();
            obj["monday"] = (repetition & TimerRepetition.Monday) == TimerRepetition.Monday;
            obj["tuesday"] = (repetition & TimerRepetition.Tuesday) == TimerRepetition.Tuesday;
            obj["wednesday"] = (repetition & TimerRepetition.Wednesday) == TimerRepetition.Wednesday;
            obj["thursday"] = (repetition & TimerRepetition.Thursday) == TimerRepetition.Thursday;
            obj["friday"] = (repetition & TimerRepetition.Friday) == TimerRepetition.Friday;
            obj["saturday"] = (repetition & TimerRepetition.Saturday) == TimerRepetition.Saturday;
            obj["sunday"] = (repetition & TimerRepetition.Sunday) == TimerRepetition.Sunday;

            obj.WriteTo(writer);
        }
    }
}
