using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionDataHolder : MonoBehaviour {

    Encounter[][] regions = new Encounter[10][];

    public Encounter[] region0;
    public Encounter[] region1;

    public int ActiveRegion = 0;

    int rand = 0;

    int monsterID = 1;
    Encounter encounter;

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
        List<Encounter> c = new List<Encounter>();
        List<Encounter> uc = new List<Encounter>();
        List<Encounter> r = new List<Encounter>();
        List<Encounter> vr = new List<Encounter>();

        MonsterRarity rarity;
        bool encounterFound = false;

        foreach(Encounter e in regions[ActiveRegion])
        {
            switch (e.rarity)
            {
                case MonsterRarity.Common:
                    c.Add(e);
                    break;
                case MonsterRarity.Uncommon:
                    uc.Add(e);
                    break;
                case MonsterRarity.Rare:
                    r.Add(e);
                    break;
                case MonsterRarity.VeryRare:
                    vr.Add(e);
                    break;
                default:
                    c.Add(e);
                    break;
            }
        }

        rarity = GetEncounterRarity();
        while (!encounterFound)
        {
            switch (rarity)
            {
                case MonsterRarity.Common:
                    if (c.Count > 0)
                    {
                        encounterFound = true;
                        encounter = c[Random.Range(0, c.Count)];
                    }
                    else
                    {
                        Debug.Log("SOMETHING WENT VERY WRONG. ENSURE THE ENCOUNTER TABLE HAS AT LEAST ONE MONSTER");
                    }
                    break;
                case MonsterRarity.Uncommon:
                    if (uc.Count > 0)
                    {
                        encounterFound = true;
                        encounter = uc[Random.Range(0, uc.Count)];
                    }
                    else
                    {
                        rarity = MonsterRarity.Common;
                        Debug.Log("No UC found");
                    }
                    break;
                case MonsterRarity.Rare:
                    if (r.Count > 0)
                    {
                        encounterFound = true;
                        encounter = r[Random.Range(0, r.Count)];
                    }
                    else
                    {
                        rarity = MonsterRarity.Uncommon;
                        Debug.Log("No R found");
                    }
                    break;
                case MonsterRarity.VeryRare:
                    if (vr.Count > 0)
                    {
                        encounterFound = true;
                        encounter = vr[Random.Range(0, vr.Count)];
                    }
                    else
                    {
                        rarity = MonsterRarity.Rare;
                        Debug.Log("No VR found");
                    }
                    break;
            }
        }

        return encounter.monster.monID;
    }

    public MonsterRarity GetEncounterRarity()
    {
        rand = Random.Range(0, 101);
        
        if(rand <= 50)
        {
            return MonsterRarity.Common;
        }
        if(rand <= 75)
        {
            return MonsterRarity.Uncommon;
        }
        if(rand <= 90)
        {
            return MonsterRarity.Rare;
        }
        else
        {
            return MonsterRarity.VeryRare;
        }
    }

    public int GetEncounterLevel()
    {
        return Random.Range(encounter.minLevel, encounter.maxLevel + 1);
    }
}
