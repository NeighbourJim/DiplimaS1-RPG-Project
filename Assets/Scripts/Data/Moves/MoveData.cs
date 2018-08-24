using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class MoveData : ScriptableObject {

    [Header("Identity")]
    [Tooltip("Move ID should be a unique number. Increment by 1 whenever a new move is created.")]
    public int moveID = 000;
    public string moveName = "?MOVENAME?";
    [TextArea(2,2)]
    public string moveDescription = "?DESC?";

    [Header("Move Type & Physical/Special")]
    public TypeNum type = TypeNum.normal;
    [Tooltip("This determines which attacking and defending stats are used in damage calculations, or if the move is a non-damaging move.")]
    public PhysSpec physSpec = PhysSpec.status;

    [Header("Move Number Values")]
    [Tooltip("Base Power used in damage calculation.")]
    public int basePower = 0;
    [Tooltip("Chance to hit % ( 100 = 100% )")]
    public int accuracy = 100;

    [Header("Status Effects")]
    [Tooltip("Which status effect this move causes, if any.")]
    public StatusEffect causesStatus = StatusEffect.none;
    [Tooltip("% Chance to cause status effect")]
    public int statusChance = 0;

    [Header("Stat Change")]
    [Tooltip("Which stat this move changes, if any.")]
    public Stat statToChange = Stat.none;
    [Tooltip("Whether or not the stat change is applied to the user or the opponent.")]
    public bool affectSelf = false;
    [Tooltip("How many stages to buff by (cap is at -6 / 6)")]
    public int buffAmount = 0;

    [Header("Priority")]
    [Tooltip("The priority for the move. Moves with higher priority go before those with lower, regardless of the monster's speed.")]
    public int priority = 0;

    [Header("Misc.")]
    [Tooltip("Whether or not the move has increased chance to crit.")]
    public bool increasedCritRatio = false;
    [Tooltip("Whether or not a move can cause a flinch.")]
    public bool causesFlinch = false;
}
