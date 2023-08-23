// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
var connectionString = "msdocs-redis-cache-anu.redis.cache.windows.net:6380,password=Ap9vvHb4gdo19lhyqn9EcePu1Nr1nxnCgAzCaPxT0FE=,ssl=True,abortConnect=False";
var redis = ConnectionMultiplexer.Connect(connectionString);

IDatabase db = redis.GetDatabase();

var messageId = db.StreamAdd("events_stream", "foo_name", "bar_value");
Console.WriteLine($"messageId = {messageId}");

var values = new NameValueEntry[]
 {
     new NameValueEntry("sensor_id", "1234"),
     new NameValueEntry("temp", "19.8")
 };

 var sensorMessageId = db.StreamAdd("sensor_stream", values);
 Console.WriteLine($"sensorMessageId = {sensorMessageId}");

  var messages = db.StreamRead("events_stream", "0-0");
 var writeMessage = (string stream, StreamEntry message) => {
     Console.WriteLine($"stream = {stream}");
     Console.WriteLine($"messageId = {message.Id}");
     foreach (var entry in message.Values)
     {
         Console.WriteLine($"entry = {entry.Name}:{entry.Value}");
     }
 };

 foreach (var message in messages)
 {
     writeMessage("events_stream", message);
 }

var streams = db.StreamRead(new StreamPosition[]
 {
     new StreamPosition("events_stream", "0-0"),
     new StreamPosition("sensor_stream", "0-0")
 });

 Console.WriteLine($"Stream = {streams.First().Key}");
 Console.WriteLine($"Length = {streams.First().Entries.Length}");
 foreach (var stream in streams)
 {
     foreach (var message in stream.Entries)
     {
         writeMessage(stream.Key.ToString(), message);
     }
 }

Console.WriteLine("\n\n");
 var streamEntries = db.StreamRange("events_stream", minId: "-", maxId: "+");
 foreach (var entry in streamEntries) {
    writeMessage("events_stream", entry);
 }