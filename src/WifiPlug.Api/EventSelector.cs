// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents an event subscription selector.
    /// </summary>
    public class EventSelector
    {
        /// <summary>
        /// Gets the resource ID.
        /// </summary>
        public string Resource { get; private set; }

        /// <summary>
        /// Gets resource type.
        /// </summary>
        public string ResourceType { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        private bool TryParseInternal(string input) {
            // check minimum valid length
            if (input.Length < 5)
                return false;

            // create selector
            EventSelector tempSelector = new EventSelector();

            // parse out the components
            ParseState parseState = ParseState.ResourceType;
            int parseIndex = 0;

            for (int i = 0; i < input.Length; i++) {
                if (input[i] == ':') {
                    // set resource type
                    ResourceType = input.Substring(0, i).Trim();

                    // setup state to parse the resource ID.
                    parseState = ParseState.Resource;
                    parseIndex = i + 1;
                } else if (input[i] == '.' && parseState == ParseState.Resource) {
                    // set resource
                    Resource = input.Substring(parseIndex, i - parseIndex).Trim();

                    // setup state to parse the name
                    parseState = ParseState.Name;
                    parseIndex = i + 1;
                }
            }

            Name = input.Substring(parseIndex).Trim();

            // check if the selector has a name
            if (Name.Length == 0 || ResourceType.Length == 0 || Resource.Length == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Trys the parse the provided input string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>If the selector was parsed.</returns>
        public static bool TryParse(string input, out EventSelector selector) {
            EventSelector subscriptionSelector = new EventSelector();

            if (subscriptionSelector.TryParseInternal(input)) {
                selector = subscriptionSelector;
                return true;
            } else {
                selector = null;
                return false;
            }
        }

        /// <summary>
        /// Parses the provided input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static EventSelector Parse(string input) {
            if (!TryParse(input, out EventSelector selector))
                throw new FormatException("The subscription selector format is invalid");

            return selector;
        }

        /// <summary>
        /// Used internally by <see cref="TryParse(string, out EventSelector)"/>.
        /// </summary>
        enum ParseState
        {
            ResourceType,
            Resource,
            Name
        }

        /// <summary>
        /// Gets the string representation of this selector.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{ResourceType}:{Resource}.{Name}";
        }

        private EventSelector() { }

        /// <summary>
        /// Creates a selector.
        /// </summary>
        /// <param name="input">The input.</param>
        public EventSelector(string input) {
            if (!TryParseInternal(input))
                throw new FormatException("The subscription selector format is invalid");
        }

        /// <summary>
        /// Creates a selector.
        /// </summary>
        /// <param name="resourceType">The resource type.</param>
        /// <param name="resourceId">The resource.</param>
        /// <param name="name">The name.</param>
        public EventSelector(string resourceType, string resourceId, string name) {
            // validate arguments
            if (resourceType == null)
                throw new ArgumentNullException(nameof(resourceType), "The resource type cannot be null");
            else if (resourceType.Length == 0)
                throw new ArgumentException(nameof(resourceType), "The resource type cannot be empty");
            else if (resourceId == null)
                throw new ArgumentNullException(nameof(resourceId), "The resource ID cannot be null");
            else if (resourceId.Length == 0)
                throw new ArgumentException(nameof(resourceId), "The resource ID cannot be empty");
            else if (name == null)
                throw new ArgumentNullException(nameof(resourceType), "The name cannot be null");
            else if (name.Length == 0)
                throw new ArgumentException(nameof(name), "The name cannot be empty");

            ResourceType = resourceType;
            Resource = resourceId;
            Name = name;
        }
    }
}
