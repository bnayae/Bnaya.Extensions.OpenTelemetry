// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Diagnostics.Metrics
{
    /// <summary>
    /// The histogram is a metrics Instrument which can be used to report arbitrary values that are likely to be statistically meaningful.
    /// e.g. the request duration.
    /// Use <see cref="Meter.CreateHistogram(string, string?, string?)" /> method to create the Histogram object.
    /// </summary>
    /// <remarks>
    /// This class supports only the following generic parameter types: <see cref="byte" />, <see cref="short" />, <see cref="int" />, <see cref="long" />, <see cref="float" />, <see cref="double" />, and <see cref="decimal" />
    /// </remarks>
    public static class HistogramExtensions
    {
        /// <summary>
        /// using tags builder before record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TTag">The type of the tag.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IHistogramBuilder<T> WithTag<T, TTag>(this Histogram<T> instance, string key, TTag value)
             where T : struct
        {
            var pair = new KeyValuePair<string, object?>(key, value);
            var list = ImmutableArray.Create(pair);
            return new HistogramBuilder<T>(instance, list);
        }
    }
}
