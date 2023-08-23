// See https://aka.ms/new-console-template for more information
 using StackExchange.Redis;
    
 var connectionString = "msdocs-redis-cache-anu.redis.cache.windows.net:6380,password=Ap9vvHb4gdo19lhyqn9EcePu1Nr1nxnCgAzCaPxT0FE=,ssl=True,abortConnect=False";
 var redis = ConnectionMultiplexer.Connect(connectionString);
  ISubscriber sub = redis.GetSubscriber();

 sub.Subscribe("org.shipping.alerts").OnMessage(channelMessage => {
     Console.WriteLine((string) channelMessage.Message);
 });

sub.Subscribe("org.shipping.alerts").OnMessage(async channelMessage => {
     await Task.Delay(1000);
     Console.WriteLine((string) channelMessage.Message);
 });

  Console.WriteLine("Listening for messages. Press any key to exit.");
 Console.ReadKey();



