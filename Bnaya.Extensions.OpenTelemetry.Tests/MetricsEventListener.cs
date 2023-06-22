using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace System.Text.Json.Extension.Extensions.Tests;

internal class MetricsEventListener : EventListener
{
    private EventSource? _source;

    public const EventKeywords MessagesKeyword = (EventKeywords)0x1;
    public const EventKeywords TimeSeriesValues = (EventKeywords)0x2;
    public const EventKeywords InstrumentPublishing = (EventKeywords)0x4;

    public const EventKeywords Test = TimeSeriesValues;
    private readonly ConcurrentQueue<EventData> _events = new ConcurrentQueue<EventData>();
    public EventData[] Events => _events.ToArray();

    public MetricsEventListener(params string[] instruments)
    {
        var args = new Dictionary<string, string?> { ["Metrics"] = string.Join(",", instruments) };
        if (_source != null)
        {
            EnableEvents(_source, EventLevel.Informational, MetricsEventListener.Test, args);
        }
    }

    public EventData this[string key, string value] => Events.Single(m => m.Tags.ContainsKey(key) && m.Tags[key] == value);

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        var payload = eventData.Payload;
        if (payload == null)
            return;
        if (payload.Count < 7)
            return;
        var value = payload[6]?.ToString();
        if (string.IsNullOrEmpty(value) || payload.Count > 7 || value == "0")
            return;

        var tags = payload[5]!.ToString()!.Split(',').ToDictionary(
                                                            m => m.Split('=')[0],
                                                            m => m.Split('=')[1]);
        var data = new EventData
        {
            MeterName = payload[1]!.ToString()!,
            MeterVersion = payload[2]!.ToString()!,
            InstrumentName = payload[3]!.ToString()!,
            Unit = payload[4]!.ToString()!,
            Tags = tags,
            Value = value!,
        };
        _events.Enqueue(data);
    }

    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name == "System.Diagnostics.Metrics")
        {
            _source = eventSource;
        }
    }
}

