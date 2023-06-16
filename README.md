# Bnaya Open Telemetry Extensions  

[![Build & Deploy NuGet](https://github.com/bnayae/Bnaya.Extensions.OpenTelemetry/actions/workflows/Deploy.yml/badge.svg)](https://github.com/bnayae/Bnaya.Extensions.OpenTelemetry/actions/workflows/Deploy.yml)

[![NuGet](https://img.shields.io/nuget/v/Bnaya.Extensions.OpenTelemetry.svg)](https://www.nuget.org/packages/Bnaya.Extensions.OpenTelemetry/) 

[![codecov](https://codecov.io/gh/bnayae/Bnaya.Extensions.OpenTelemetry/branch/main/graph/badge.svg?token=TPKF0JUWNT)](https://codecov.io/gh/bnayae/Bnaya.Extensions.OpenTelemetry)

Extends OpenTelemetry API:

Usability improvement when building metrics tags.

Having the following API:

``` cs
Counter<int> c = meter.CreateCounter<int>("counter1");
c.WithTag("Color", "red")
 .WithTag("Price", "Low")
    .Add(5);
h.WithTag("Size", 123)
    .Record(19);
udc.WithTag("Color", "red")
    .Add(-33);
```

instead of 

``` cs
Counter<int> c = meter.CreateCounter<int>("counter1");
c.Add(5, 
	new KeyValuePair<string,object?>("Color", "red"),
	new KeyValuePair<string, object?>("Price", "Low"));
h.Record(19, 
	new KeyValuePair<string, object?>("Size", 123));
udc.Add(-33, 
	new KeyValuePair<string, object?>("Color", "red"));
```

The changes introduce an immutable Builder pattern therefore it can benefit from setting the tags once for multiple usage i.e.

```cs
Counter<int> c = meter.CreateCounter<int>("counter1");
c.WithTag("Color", "red")
 .WithTag("Price", "Low")
    .Add(5)
    .Add(6)
    .Add(7);
``` 

Because it's immutable, the following scenario will work just fine:

```cs
Counter<int> c = meter.CreateCounter<int>("counter1");

var counter1 = c.WithTag("Color", "red");
var counter2 =  counter1.WithTag("Price", "Low")
    .Add(5)
    .Add(6)
    .Add(7);

counter1.Add(-1); // won't have the Price tag
```
