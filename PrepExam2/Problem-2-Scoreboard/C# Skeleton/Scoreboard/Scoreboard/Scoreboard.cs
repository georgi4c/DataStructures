using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Scoreboard : IScoreboard
{
    public int MaxEntriesToKeep { get; set; }
    private Dictionary<string, OrderedBag<ScoreboardEntry>> scoreboard;
    private Dictionary<string, string> users;
    private Dictionary<string, string> games;
    private Trie<int> gamesNames;
    
    public Scoreboard(int maxEntriesToKeep = 10)
    {
        this.scoreboard = new Dictionary<string, OrderedBag<ScoreboardEntry>>();
        this.users = new Dictionary<string, string>();
        this.games = new Dictionary<string, string>();
        this.MaxEntriesToKeep = maxEntriesToKeep;
        this.gamesNames = new Trie<int>();
    }

    public bool RegisterUser(string username, string password)
    {
        if (users.ContainsKey(username))
        {
            return false;
        }
        users.Add(username, password);
        return true;
    }

    public bool RegisterGame(string game, string password)
    {
        if (games.ContainsKey(game))
        {
            return false;
        }
        games.Add(game, password);
        scoreboard.Add(game, new OrderedBag<ScoreboardEntry>());
        gamesNames.Insert(game, 0);
        return true;
    }

    public bool AddScore(string username, string userPassword, string game, string gamePassword, int score)
    {
        if (!games.ContainsKey(game) || !users.ContainsKey(username) || users[username] != userPassword || games[game] != gamePassword)
        {
            return false;
        }
        scoreboard[game].Add(new ScoreboardEntry(){});
        return true;
    }

    public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
    {
        return scoreboard[game].Take(MaxEntriesToKeep);
    }

    public bool DeleteGame(string game, string gamePassword)
    {
        if (!games.ContainsKey(game) || games[game] != gamePassword)
        {
            return false;
        }
        games.Remove(game);
        scoreboard.Remove(game);
        return true;
    }

    public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
    {
        
        var result = gamesNames.GetByPrefix(gameNamePrefix).Take(MaxEntriesToKeep);
        return result;
    }
}