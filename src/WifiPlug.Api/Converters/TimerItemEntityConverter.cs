﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Converters
{
    /// <summary>
    /// Provides functionality to convert between a repetition enum and JSON.
    /// </summary>
    internal class TimerItemEntityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(TimerItemEntity) || objectType == typeof(TimerItemEntity[]);
        }

        private TimerItemEntity FromObject(JObject obj) {
            string type = (string)obj["type"];

            if (type.Equals("device", StringComparison.CurrentCultureIgnoreCase)) {
                return new TimerItemEntity() {
                    Type = type,
                    Entity = ((JObject)obj["entity"]).ToObject<DeviceEntity>()
                };
            } else if (type.Equals("group", StringComparison.CurrentCultureIgnoreCase)) {
                return new TimerItemEntity() {
                    Type = type,
                    Entity = ((JObject)obj["entity"]).ToObject<GroupEntity>()
                };
            } else {
                return null;
            }
        }

        private JObject ToObject(TimerItemEntity entity) {
            JObject obj = new JObject();
            obj["type"] = entity.Type;
            obj["entity"] = JObject.FromObject(entity.Entity);
            return obj;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            // load next token
            JToken nextToken = JToken.Load(reader);

            if (nextToken.Type == JTokenType.Array) {
                JArray arr = (JArray)nextToken;
                TimerItemEntity[] entities = new TimerItemEntity[arr.Count];

                for (int i = 0; i < arr.Count; i++) {
                    entities[i] = FromObject((JObject)arr[i]);
                }

                return entities;
            }

            // build
            return FromObject((JObject)nextToken);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value == typeof(TimerItemEntity)) {
                // cast
                TimerItemEntity entity = (TimerItemEntity)value;

                // build
                ToObject(entity).WriteTo(writer);
            } else {
                // cast
                TimerItemEntity[] entities = (TimerItemEntity[])value;

                JObject[] objs = new JObject[entities.Length];

                for (int i = 0; i < objs.Length; i++) {
                    objs[i] = ToObject(entities[i]);
                }

                new JArray(objs).WriteTo(writer);
            }
        }
    }
}