using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.VisualStudio.TestPlatform.Utilities;

using Xunit;
using Xunit.Abstractions;

namespace System.Text.Json.Extension.Extensions.Tests;
public class MetricsExtensionsTests
{
    private readonly ITestOutputHelper _output;
    private const string NAME = "Test123";
    private readonly Meter _meter = new Meter(NAME, "1.0.0");
    private static TimeSpan TIMEOUT = Debugger.IsAttached ? TimeSpan.FromMinutes(3) : TimeSpan.FromSeconds(5);

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
    public async Task MetricsBuilder_Test()
    {
        var cts = new CancellationTokenSource(TIMEOUT);
        Counter<int> c = _meter.CreateCounter<int>("counter1", "unit");
        //UpDownCounter<int> udc = _meter.CreateUpDownCounter<int>("upcounter1", "kg");
        Histogram<int> h = _meter.CreateHistogram<int>("histogram1", "meter");

        EventData[] events = Array.Empty<EventData>();
        //using (MetricsEventListener listener = new(@$"{NAME}\counter1;{NAME}\upcounter1;{NAME}\histogram1"))
        using (MetricsEventListener listener = new(NAME))
        {
            c.WithTag("Color", "red")
             .WithTag("Importance", "high")
             .Add(2)
             .Add(-1)
             .Add(7);
            c.WithTag("Color", "blue")
             .Add(6);
            h.WithTag("Size", 123)
             .WithTag("test", false)
             .Record(3)
             .Record(4)
             .Record(955)
             .Record(10)
             .Record(11)
             .Record(780)
             .Record(15)
             .Record(800)
             .Record(5)
             .Record(1)
             .Record(850)
             .Record(750)
             .Record(750)
             .Record(750)
             .Record(750)
             .Record(750)
             .Record(750)
             .Record(650)
             .Record(650)
             .Record(650)
             .Record(650)
             .Record(850)
             .Record(2)
             .Record(2);
            h.WithTag("Size", 124)
             .Record(200)
             .Record(10)
             .Record(20);
            //udc.WithTag("Weight", "85")
            //    .Add(20);
            //udc.WithTag("Weight", "72")
            //    .Add(5);

            while (!cts.Token.IsCancellationRequested && listener.Events.Length < 4)
            {
                _output.WriteLine("wait");
                await Task.Delay(250);           
            }
            events = listener.Events;

            Assert.Equal(4, events.Length);
            Assert.Equal("8", listener["Color", "red"].Value);
            Assert.Equal("8", listener["Importance", "high"].Value);
            Assert.Equal("6", listener["Color", "blue"].Value);
            Assert.Equal("0.5=650;0.95=850;0.99=955", listener["Size", "123"].Value);
            Assert.Equal("0.5=20;0.95=200;0.99=200", listener["Size", "124"].Value);
        }
    }

    #endregion // MetricsBuilder_Test
}

