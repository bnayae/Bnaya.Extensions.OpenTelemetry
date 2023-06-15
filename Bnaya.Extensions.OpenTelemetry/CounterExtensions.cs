// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Diagnostics.Metrics
{
    /// <summary>
    /// The counter is an instrument that supports adding non-negative values. For example you might call
    /// counter.Add(1) each time a request is processed to track the total number of requests. Most metric viewers
    /// will display counters using a rate by default (requests/sec) but can also display a cumulative total.
    /// </summary>
    /// <remarks>
    /// This class supports only the following generic parameter types: <see cref="byte" />, <see cref="short" />, <see cref="int" />, <see cref="long" />, <see cref="float" />, <see cref="double" />, and <see cref="decimal" />
    /// </remarks>
    public static class CounterExtensions
    {
        /// <summary>
        /// using tags builder before adding a delta.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TTag">The type of the tag.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ICounterBuilder<T> WithTag<T, TTag>(this Counter<T> instance, string key, TTag value)
                         where T : struct
        {
            var pair = new KeyValuePair<string, object?>(key, value);
            var list = ImmutableArray.Create(pair);
            return new CounterBuilder<T>(instance, list);
        }
    }
}
