﻿// Copyright (c) KhooverSoft. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Khooversoft.Toolbox
{
    /// <summary>
    /// State manager builder
    /// </summary>
    public class StateManagerBuilder : IEnumerable<IStateItem>
    {
        public StateManagerBuilder()
        {
            StateItems = new List<IStateItem>();
        }

        public StateManagerBuilder(StateManager workPlan)
        {
            Verify.IsNotNull(nameof(workPlan), workPlan);

            StateItems = new List<IStateItem>(workPlan.StateItems);
        }

        public IList<IStateItem> StateItems { get; }

        public Action<StateNotify> Notify { get; private set; }

        public StateManagerBuilder Add(IStateItem item)
        {
            Verify.IsNotNull(nameof(item), item);

            StateItems.Add(item);
            return this;
        }

        public StateManagerBuilder Add(IEnumerable<IStateItem> items)
        {
            Verify.IsNotNull(nameof(items), items);

            items.Run(x => Add(x));
            return this;
        }

        public StateManagerBuilder Set(Action<StateNotify> notify)
        {
            Verify.IsNotNull(nameof(notify), notify);

            Notify = notify;
            return this;
        }

        public IStateManager Build()
        {
            return new StateManager(this);
        }

        public IEnumerator<IStateItem> GetEnumerator()
        {
            return StateItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return StateItems.GetEnumerator();
        }
    }
}
