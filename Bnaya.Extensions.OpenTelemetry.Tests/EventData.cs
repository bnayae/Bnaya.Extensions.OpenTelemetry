using System.Collections.Generic;

namespace System.Text.Json.Extension.Extensions.Tests;

record EventData
{
    public required string MeterName { get; init; }
    public required string MeterVersion { get; init; }
    public required string InstrumentName { get; init; }
    public required string Unit { get; init; }
    public required IDictionary<string, string> Tags { get; init; }
    public required string Value { get; init; }
}

