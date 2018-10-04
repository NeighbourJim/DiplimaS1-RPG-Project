using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    BattleStarting,
    EnemyIntro,
    PlayerIntro,
    SelectingAction,
    EnemySelectAction,
    SelectingMove,
    DetermineOrder,
    FirstAction,
    SecondAction,
    FaintCheck,
    FleeAttempt,
    PlayerFaints,
    EnemyFaints,
    BothFaint,
    TurnEnding,
    PlayerLose,
    PlayerWin,
    BattleEnding
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

public enum BattleType
{
    WildFleeable,
    WildUnfleeable,
    Trainer
}

public enum PhysSpec
{
    Physical,
    Special,
    Status
}

public enum StatusEffect
{
    none,
    Burn,
    Poison,
    Paralysis,
    Sleep,
    Frozen,
    Confusion,
    fainted = 50
}

public enum MonStat
{
    none,
    Attack,
    Defense,
    SpecialAttack,
    SpecialDefense,
    Speed,
    Accuracy,
    Evasion
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

public enum BattleEvent
{
    wildMonEncounter,
    wildMonFaint,
    wildMonCaptured,

    trainerMonSentOut,
    trainerMonWithdrawn,
    trainerMonFainted,

    playerMonSentOut,
    playerMonWithdrawn,
    playerMonFainted,

    playerMonUseMove,
    enemyMonUseMove,

    moveCritical,
    moveMiss,
    moveImmune,
    moveEffective,
    moveResisted,
    moveBuffStat,
    moveDebuffStat,
    moveCauseStatus,

    receiveExp,
    levelUp,

    completedPlayerWin,
    completedPlayerLose,
}

public enum MonsterRarity
{
    Common,
    Uncommon,
    Rare,
    VeryRare
}