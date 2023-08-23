// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
 using System.Text.Json;

Console.WriteLine("Hello, World!");
var connectionString = "msdocs-redis-cache-anu.redis.cache.windows.net:6380,password=Ap9vvHb4gdo19lhyqn9EcePu1Nr1nxnCgAzCaPxT0FE=,ssl=True,abortConnect=False";
var redisConnection = ConnectionMultiplexer.Connect(connectionString);

 IDatabase db = redisConnection.GetDatabase();
 bool wasSet = db.StringSet("favourite:flavor", "red-velvet");

 string value = db.StringGet("favourite:flavor");
 Console.WriteLine(value);

 var executeResult = db.Execute("ping");
 Console.WriteLine(executeResult.ToString()); 

 var stat = new GameStat
 {
     Id = "1950-world-cup",
     Sport = "Soccer",
     DatePlayed = new DateTime(1950, 7, 16),
     Game = "FIFA World Cup",
     Teams = new[] { "Uruguay", "Brazil" },
     Results = new[] { ("Uruguay", 2), ("Brazil", 1) }
 };

 string jsonString = JsonSerializer.Serialize(stat);
 bool added = db.StringSet("event:1950-world-cup", jsonString);

 // Use the System.Text.Json to turn the string back into an instance of the object:
 var result = db.StringGet("event:1950-world-cup");
 var statResult = JsonSerializer.Deserialize<GameStat>(result.ToString());
 Console.WriteLine(statResult.Sport);

redisConnection.Dispose();
redisConnection = null;