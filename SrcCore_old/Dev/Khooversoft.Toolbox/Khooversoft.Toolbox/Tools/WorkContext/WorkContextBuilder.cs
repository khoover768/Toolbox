﻿// Copyright (c) KhooverSoft. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Khooversoft.Toolbox
{
    /// <summary>
    /// Work context builder, used this class to create new instances of immutable work context
    /// </summary>
    public class WorkContextBuilder
    {
        /// <summary>
        /// Default construct
        /// </summary>
        public WorkContextBuilder()
        {
            Properties = new Dictionary<string, object>();
            Cv = new CorrelationVector();
            Tag = Tag.Empty;
            EventLog = new EventLogNull();
            Dimensions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Construct from work context
        /// </summary>
        /// <param name="context"></param>
        public WorkContextBuilder(IWorkContext context)
        {
            Verify.IsNotNull(nameof(context), context);

            Properties = context.Properties.ToDictionary(x => x.Key, x => x.Value);
            Cv = context.Cv;
            Tag = context.Tag;
            WorkContainer = context.Container;
            CancellationToken = context.CancellationToken;
            EventLog = context.EventLog;
            Dimensions = context.Dimensions.ToDictionary(x => x.Key, x => x.Value);
        }

        public CorrelationVector Cv { get; set; }

        public Tag Tag { get; set; }

        public ILifetimeScope WorkContainer { get; set; }

        public IDictionary<string, object> Properties { get; }

        public CancellationToken? CancellationToken { get; set; }

        public IEventLog EventLog { get; set; }

        public IDictionary<string, string> Dimensions { get; set; }

        /// <summary>
        /// Add new property
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns>this</returns>
        public WorkContextBuilder Add(string key, object value)
        {
            Properties.Add(key, value);
            return this;
        }

        /// <summary>
        /// Set new value in property, uses type's name as key
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="value">value</param>
        /// <returns>this</returns>
        public WorkContextBuilder Set<T>(T value) where T : class
        {
            Properties.Set<T>(value);
            return this;
        }

        /// <summary>
        /// Set code tag
        /// </summary>
        /// <param name="tag">code tag</param>
        /// <returns>this</returns>
        public WorkContextBuilder Set(Tag tag)
        {
            Verify.IsNotNull(nameof(tag), tag);

            Tag = Tag.With(tag);
            return this;
        }

        /// <summary>
        /// Set correlation vector
        /// </summary>
        /// <param name="cv">cv</param>
        /// <returns>this</returns>
        public WorkContextBuilder Set(CorrelationVector cv)
        {
            Verify.IsNotNull(nameof(cv), cv);

            Cv = cv;
            return this;
        }

        /// <summary>
        /// Set event log
        /// </summary>
        /// <param name="eventLog">event log</param>
        /// <returns>this</returns>
        public WorkContextBuilder Set(IEventLog eventLog)
        {
            Verify.IsNotNull(nameof(eventLog), eventLog);

            EventLog = eventLog;
            return this;
        }

        /// <summary>
        /// Set event dimensions
        /// </summary>
        /// <param name="values">dimensions values</param>
        /// <returns>this</returns>
        public WorkContextBuilder Set(IEnumerable<KeyValuePair<string, string>> values)
        {
            Verify.IsNotNull(nameof(values), values);

            Dimensions = values
                .ToDictionary(x => x.Key, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

            return this;
        }

        /// <summary>
        /// Set container (AutoFac)
        /// </summary>
        /// <param name="workContainer">container</param>
        /// <returns>this</returns>
        public WorkContextBuilder SetContainer(ILifetimeScope workContainer)
        {
            Verify.IsNotNull(nameof(workContainer), workContainer);

            WorkContainer = workContainer;
            return this;
        }

        /// <summary>
        /// Set cancellation token
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public WorkContextBuilder SetCancellationToken(CancellationToken? token)
        {
            CancellationToken = token;
            return this;
        }

        /// <summary>
        /// Remove property
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>this</returns>
        public WorkContextBuilder Remove(string key)
        {
            Properties.Remove(key);
            return this;
        }

        /// <summary>
        /// Remove key using type's name as key
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>this</returns>
        public WorkContextBuilder Remove<T>() where T : class
        {
            Properties.Remove<T>();
            return this;
        }

        /// <summary>
        /// Build immutable work context from details
        /// </summary>
        /// <returns>new instance of work context</returns>
        public IWorkContext Build()
        {
            return new WorkContext(
                cv: Cv,
                tag: Tag,
                workContainer: WorkContainer,
                properties: Properties.ToProperties(),
                CancellationToken,
                eventLog: EventLog,
                dimensions: Dimensions.ToEventDimensions()
                );
        }
    }
}
