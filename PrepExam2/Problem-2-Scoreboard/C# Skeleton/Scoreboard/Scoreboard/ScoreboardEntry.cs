using System;

public class ScoreboardEntry : IComparable<ScoreboardEntry>
{
    public int CompareTo(ScoreboardEntry other)
    {
        var cmp = this.Score.CompareTo(other.Score);
        if (cmp > 0)
        {
            return -1;
        }
        if (cmp < 0)
        {
            return 1;
        }
        var secCmp = this.Username.CompareTo(other.Username);
        return secCmp;

    }

    public int Score { get; set; }

    public string Username { get; set; }
}