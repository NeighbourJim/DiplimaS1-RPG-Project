using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    Intro,
    SelectingAction,
    EnemySelectAction,
    SelectingMove,
    DetermineOrder,
    FirstAction,
    SecondAction,
    PlayerFaints,
    EnemyFaints,
    BothFaint,
    PlayerLose,
    PlayerWin
}

public enum TypeNum
{
    none,
    normal,
    grass,
    water,
    fire,
    electric,
    psychic,
    ice,
    dragon,
    fighting,
    flying,
    poison,
    ground,
    rock,
    bug,
    ghost
}

public enum Ownership
{
    wild,
    player,
    trainer
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
    frozen,
    confused,
    fainted = 50
}

public enum MonStat
{
    none,
    atk,
    def,
    spatk,
    spdef,
    speed,
    acc,
    eva
}

public enum Effectiveness
{
    immune = -10,
    resist2x = -2,
    resist4x = -4,    
    normal1x = 0,
    weak2x = 2,
    weak4x = 4
}