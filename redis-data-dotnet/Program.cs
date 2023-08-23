namespace redis_data_dotnet;
 using ServiceStack.Redis;
class Program
{
    static string reddisConnString = "Ap9vvHb4gdo19lhyqn9EcePu1Nr1nxnCgAzCaPxT0FE=@msdocs-redis-cache-anu.redis.cache.windows.net:6380?ssl=true";
    static void Main(string[] args)
    {
        bool transactionResult = false;
        using RedisClient redisClient = new RedisClient(reddisConnString);
        using var transaction = redisClient.CreateTransaction();
        transaction.QueueCommand(c => c.Set("name", "anu"));
        transaction.QueueCommand(c => c.Set("type", "human"));
        transaction.QueueCommand(c => ((RedisNativeClient)c).Expire("name", 15));
        transaction.QueueCommand(c => ((RedisNativeClient)c).Expire("type", 15));
        transactionResult = transaction.Commit();
        if (transactionResult){
            Console.WriteLine("Transaction Committed");
        } else {
            Console.WriteLine("Transaction failed");
        }
    }
}
