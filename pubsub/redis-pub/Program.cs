 using StackExchange.Redis;
    
 var connectionString = "msdocs-redis-cache-anu.redis.cache.windows.net:6380,password=Ap9vvHb4gdo19lhyqn9EcePu1Nr1nxnCgAzCaPxT0FE=,ssl=True,abortConnect=False";
 var redis = ConnectionMultiplexer.Connect(connectionString);

 ISubscriber sub = redis.GetSubscriber();
 
  for (int i = 0; i < 10; i++)
 {
     sub.Publish("org.shipping.alerts", $"labelprint-sdf9878-{i}");
     Thread.Sleep(1000);
 }