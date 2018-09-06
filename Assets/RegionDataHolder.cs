using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionDataHolder : MonoBehaviour {

    Encounter[][] regions = new Encounter[10][];

    public Encounter[] region0;
    public Encounter[] region1;

    public int ActiveRegion = 0;

    int rand = 0;

    private void Start()
    {
        regions[0] = region0;
        regions[1] = region1;
    }

    public void SetRegion(int r)
    {
        ActiveRegion = r;
    }

    public int GetEncounterMon()
    {
        rand = Random.Range(0, regions[ActiveRegion].Length);
        return regions[ActiveRegion][rand].monster.monID;
    }

    public int GetEncounterLevel()
    {
        return Random.Range(regions[ActiveRegion][rand].minLevel, regions[ActiveRegion][rand].maxLevel + 1);
    }
}
