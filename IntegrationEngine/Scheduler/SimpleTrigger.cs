﻿using IntegrationEngine.Model;
using Nest;
using System;
using QuartzTriggerState = Quartz.TriggerState;

namespace IntegrationEngine.Scheduler
{
    public class SimpleTrigger : ISimpleTrigger, IHasStateDescription
    {
        public string Id { get; set; }
        public string JobType { get; set; }
        public int RepeatCount { get; set; }
        public TimeSpan RepeatInterval { get; set; }
        public DateTimeOffset StartTimeUtc { get; set; }
        public int StateId { get; set; }
        [ElasticProperty(OptOut = true)]
        public string StateDescription { get { return StateId.GetStateDescription(); } }
    }
}
