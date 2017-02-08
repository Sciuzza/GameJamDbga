public static class Sync
{
    public static int finalH = UnityEngine.Random.Range(0, 24);
    public const int MODH = 24;
    public const int NHE = 4;
    public static bool isReady;
    public static int actualHour;
    public static int getHour()
    {
        return actualHour % MODH;
    }
}

public static class Score
{
    public static int nTurns;
    public static int nBattles;
}