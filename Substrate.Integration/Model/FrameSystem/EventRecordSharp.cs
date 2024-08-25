using Substrate.Bajun.NET.NetApiExt.Generated.Model.frame_system;
using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Integration.Model.FrameSystem
{
    /// <summary>
    /// Event Record C# Wrapper
    /// </summary>
    public class EventRecordSharp
    {
        /// <summary>
        /// Event Record Constructor
        /// </summary>
        /// <param name="eventRecord"></param>
        public EventRecordSharp(EventRecord eventRecord)
        {
            Phase = new EnumPhaseSharp(eventRecord.Phase);
            Event = new EnumRuntimeEventSharp(eventRecord.Event);
            Topics = new List<string>();
            foreach (var topic in eventRecord.Topics.Value)
            {
                Topics.Add(Encoding.UTF8.GetString(topic.Bytes));
            }
        }

        /// <summary>
        /// Phase
        /// </summary>
        public EnumPhaseSharp Phase { get; }

        /// <summary>
        /// Event
        /// </summary>
        public EnumRuntimeEventSharp Event { get; }

        /// <summary>
        /// Topics
        /// </summary>
        public List<string> Topics { get; }
    }
}
