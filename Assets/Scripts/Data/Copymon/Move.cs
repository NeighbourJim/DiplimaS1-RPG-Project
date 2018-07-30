using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public int moveID = 000;
    public string moveName = "NONAME";
    public Type type = Type.normal;
    public PhysSpec physSpec = PhysSpec.status;
    public int basePower = 0;
    public int accuracy = 0;
    public StatusEffect causesStatus = StatusEffect.none;
    public int statusChance = 0;

}

public enum PhysSpec
{
    physical,
    special,
    status
}

public enum StatusEffect
{
    none,
    burned,
    poisoned,
    paralyzed,
    sleep,
    frozen
}