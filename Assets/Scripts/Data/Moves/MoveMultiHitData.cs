using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MoveMultiHitData : MoveData {

    [Header("Multi-Hit Data")]
    [Tooltip("Whether or not the move hits multiple times.")]
    public bool multiHit = true;
    [Tooltip("Minimum number of hits the move can do")]
    public int minimumHits = 1;
    [Tooltip("Maximum number of hits the move can do")]
    public int maximumHits = 5;

    public int GetHitCount()
    {
        return Random.Range(minimumHits, maximumHits + 1);
    }
}
