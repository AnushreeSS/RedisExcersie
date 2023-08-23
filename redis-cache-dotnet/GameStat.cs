public class GameStat
 {
     public string Id { get; set; }
     public string Sport { get; set; }
     public DateTimeOffset DatePlayed { get; set; }
     public string Game { get; set; }
     public IList<string> Teams { get; set; }
     public IList<(string team, int score)> Results { get; set; }
 }