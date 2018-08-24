using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{

    public int moveID = 000;
    public string moveName = "?MOVENAME?";

    public TypeNum type = TypeNum.none;
    public PhysSpec physSpec = PhysSpec.status;

    public int basePower = 0;
    public int accuracy = 0;

    public StatusEffect causesStatus = StatusEffect.none;
    public int statusChance = 0;

    public MonStat statToChange = MonStat.none;
    public int buffAmount = 0;
}
