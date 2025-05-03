﻿namespace Emovere.SharedKernel.Events
{
    public abstract record Message
    {
        protected Message() => MessageType = GetType().Name;

        public string MessageType { get; }
        public Guid AggregateId { get; protected set; }
    }
}