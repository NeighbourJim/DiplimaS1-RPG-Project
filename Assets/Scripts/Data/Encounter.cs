using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Encounter
{
    public MonData monster;
    public int minLevel;
    public int maxLevel;
    public MonsterRarity rarity;
}
