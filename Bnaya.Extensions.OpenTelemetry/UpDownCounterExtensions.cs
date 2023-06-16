#pragma warning disable S125 // Sections of code should not be commented out

//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.

//using System.Collections.Generic;
//using System.Collections.Immutable;

//namespace System.Diagnostics.Metrics
//{
//    /// <summary>
//    /// The UpDownCounter is an instrument that supports reporting positive or negative metric values.
//    /// UpDownCounter may be used in scenarios like reporting the change in active requests or queue size.
//    /// </summary>
//    /// <remarks>
//    /// This class supports only the following generic parameter types: <see cref="byte" />, <see cref="short" />, <see cref="int" />, <see cref="long" />, <see cref="float" />, <see cref="double" />, and <see cref="decimal" />
//    /// </remarks>
//    public static class UpDownCounterExtensions
//    {
//        /// <summary>
//        /// using tags builder before adding a delta.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <typeparam name="TTag">The type of the tag.</typeparam>
//        /// <param name="instance">The instance.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        public static IUpDownCounterBuilder<T> WithTag<T, TTag>(this UpDownCounter<T> instance, string key, TTag value)
//             where T : struct 
//        {
//            var pair = new KeyValuePair<string, object?>(key, value);
//            var list = ImmutableArray.Create(pair);
//            return new UpDownCounterBuilder<T>(instance, list);
//        }
//    }
//}
