statr
=====

statr (pronounced statter) is a tool for collecting, aggregating, reporting and reacting to
application metrics. It is similar to tools like graphite (with statsd) and munin and uses
many of the same terminology and even, in some cases, the same file and wire formats.

What's an application level event?
----------------------------------





How do I send a metric from my application?
-------------------------------------------

There is currently only one way to send an event to the application and that is via the Statr.Client. It uses
a very simple wire format which is identical to [statsd](https://github.com/etsy/statsd/).  Here's how you would
send a count metric:

    // Create a client pointing to local host
    var client = StatrClient.Build("localhost");

	// Send the count metric
    client.Count("stats.user.signups");
	

Contributing
------------

I'd love if you could contribute. You could:

* Add a metric type
* Add a aggreagation type
* Write examples and documentation!

However, if you're going to contribute please make sure you write tests for any new functionality or bug fixes.
Adding an example is also great!