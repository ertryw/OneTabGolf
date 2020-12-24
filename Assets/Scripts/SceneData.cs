using UnityEngine;

/// <summary>
/// Scene current states and data
/// </summary>
public class SceneData : Singleton<SceneData>
{
    protected SceneData() { }
    /// <summary> GameOver State </summary>
    public bool gameover;
    /// <summary> Hole state </summary>
    public bool holed;
    /// <summary> Wait for ball get in hole state </summary>
    public bool waitState;
    /// <summary> Ball throw bool </summary>
    public bool taped;
    /// <summary> Current scores </summary>
    public int scores;
    /// <summary> Best Scores </summary>
    public int best_scores;
    /// <summary> Current velocity of trajectory increment </summary>
    public float velocity_Increment;
    /// <summary> Velocity step for new level </summary>
    public float velocity_IncrementPerLvl;

}
