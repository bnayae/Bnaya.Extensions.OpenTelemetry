using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;

using Microsoft.VisualStudio.TestPlatform.Utilities;

using Xunit;
using Xunit.Abstractions;

namespace System.Text.Json.Extension.Extensions.Tests;
public partial class MetricsExtensionsTests
{
    private readonly ITestOutputHelper _output;
    private const string NAME = "Test123";
    private Meter _meter = new Meter(NAME);
    private const double IntervalSecs = 10;
    private static readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(60);

    #region Ctor

    public MetricsExtensionsTests(ITestOutputHelper output)
    {
        _output = output;
    }

    #endregion // Ctor

    #region MetricsBuilder_Test

    //[Theory]
    //[InlineData("color", "red")]
    //[InlineData("amount", 1)]
    //[InlineData("succeed", false)]
    //public void MetricsBuilder_Test(string key, object? value)
    [Fact]
    public void MetricsBuilder_Test()
    {
        Counter<int> c = _meter.CreateCounter<int>("counter1");
        UpDownCounter<int> udc = _meter.CreateUpDownCounter<int>("upDownCounter1");
        Histogram<int> h = _meter.CreateHistogram<int>("histogram1");

        c.WithTag("Color", "red")
         .Add(5);
        c.WithTag("Color", "blue")
         .Add(6);
        h.WithTag("Size", 123)
         .Record(19);
        h.WithTag("Size", 124)
         .Record(20);
        udc.WithTag("Color", "red")
            .Add(-33);
        udc.WithTag("Color", "blue")
            .Add(-34);
    }

    #endregion // MetricsBuilder_Test
}


