using UnityEngine;

public class ExplorerStats
{
    // Body Stats
    public float health { get; set; }
    public float attack { get; set; }
    public float stamina { get; set; }
    public float mana { get; set; }

    // Items Stats
    public float itemDamage { get; set; }
    public float itemDefense { get; set; }

    private static ExplorerStats explorerStats;

    public static ExplorerStats getInstance()
    {
        if (explorerStats == null)
        {
            explorerStats = new ExplorerStats();
        }
        return explorerStats;
    }

    private ExplorerStats()
    { }
}
