using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonBattleData : MonData
{
    public MonBattleData(MonData bm)
    {
        monsterPrefab = bm.monsterPrefab;
        cry = bm.cry;

        monID = bm.monID;
        monName = bm.monName;
        nickname = bm.nickname;
        ownership = bm.ownership;

        primaryType = bm.primaryType;
        if(bm.secondaryType != null)
            secondaryType = bm.secondaryType;

        level = bm.level;
        curXP = bm.curXP;
        xpToNextLevel = bm.xpToNextLevel;
        xpYield = bm.xpYield;

        evolves = bm.evolves;
        levelToEvolve = bm.levelToEvolve;
        evolvesInto = bm.evolvesInto;

        baseHP = bm.baseHP;
        baseAtk = bm.baseAtk;
        baseDef = bm.baseDef;
        baseSpAtk = bm.baseSpAtk;
        baseSpDef = bm.baseSpDef;
        baseSpeed = bm.baseSpeed;

        ivHP = bm.ivHP;
        ivAtk = bm.ivAtk;
        ivDef = bm.ivDef;
        ivSpAtk = bm.ivSpAtk;
        ivSpDef = bm.ivSpDef;
        ivSpeed = bm.ivSpeed;

        curAtk = bm.curAtk;
        curDef = bm.curDef;
        curSpAtk = bm.curSpAtk;
        curSpDef = bm.curSpDef;
        curSpeed = bm.curSpeed;

        maxHP = bm.maxHP;
        curHP = bm.curHP;
        hasStatus = bm.hasStatus;
        remainingSleepTurns = bm.remainingSleepTurns;

        learnedMoves = bm.learnedMoves;

        learnset = bm.learnset;
    }

    public void SetData(MonData bm)
    {
        monsterPrefab = bm.monsterPrefab;
        cry = bm.cry;

        monID = bm.monID;
        monName = bm.monName;
        nickname = bm.nickname;
        ownership = bm.ownership;

        primaryType = bm.primaryType;
        if (bm.secondaryType != null)
            secondaryType = bm.secondaryType;

        level = bm.level;
        curXP = bm.curXP;
        xpToNextLevel = bm.xpToNextLevel;
        xpYield = bm.xpYield;

        evolves = bm.evolves;
        levelToEvolve = bm.levelToEvolve;
        evolvesInto = bm.evolvesInto;

        baseHP = bm.baseHP;
        baseAtk = bm.baseAtk;
        baseDef = bm.baseDef;
        baseSpAtk = bm.baseSpAtk;
        baseSpDef = bm.baseSpDef;
        baseSpeed = bm.baseSpeed;

        ivHP = bm.ivHP;
        ivAtk = bm.ivAtk;
        ivDef = bm.ivDef;
        ivSpAtk = bm.ivSpAtk;
        ivSpDef = bm.ivSpDef;
        ivSpeed = bm.ivSpeed;

        curAtk = bm.curAtk;
        curDef = bm.curDef;
        curSpAtk = bm.curSpAtk;
        curSpDef = bm.curSpDef;
        curSpeed = bm.curSpeed;

        maxHP = bm.maxHP;
        curHP = bm.curHP;
        hasStatus = bm.hasStatus;

        learnedMoves = bm.learnedMoves;

        learnset = bm.learnset;
    }


    [Header("Stat Boosts")]
    public int buffStageAtk = 0;
    public int buffStageDef = 0;
    public int buffStageSpAtk = 0;
    public int buffStageSpDef = 0;
    public int buffStageSpeed = 0;
    public int buffStageAcc = 0;
    public int buffStageEva = 0;

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            curHP = 0;
            Faint();
        }
    }

    public void HealDamage(int heal)
    {
        curHP += heal;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
    }

    public bool GainStatus(StatusEffect status)
    {
        if(hasStatus == StatusEffect.none)
        {
            hasStatus = status;
            if(status == StatusEffect.sleep)
            {
                remainingSleepTurns = (Random.Range(2, 6));
            }
            return true;
        }

        return false;
    }

    public bool HealStatus()
    {
        if(hasStatus != StatusEffect.none)
        {
            hasStatus = StatusEffect.none;
            return true;
        }
        return false;
    }

    public bool ModStat(MonStat stat, int stages)
    {
        switch (stat)
        {
            case MonStat.atk:
                return ModStat(ref buffStageAtk, stages);
            case MonStat.def:
                return ModStat(ref buffStageDef, stages);
            case MonStat.spatk:
                return ModStat(ref buffStageSpAtk, stages);
            case MonStat.spdef:
                return ModStat(ref buffStageSpDef, stages);
            case MonStat.speed:
                return ModStat(ref buffStageSpeed, stages);
            case MonStat.acc:
                return ModStat(ref buffStageAcc, stages);
            case MonStat.eva:
                return ModStat(ref buffStageEva, stages);
            default:
                return false;
        }
    }

    bool ModStat(ref int stat, int stages)
    {
        if (stages > 0)
        {
            if (stat < 6)
            {
                stat += stages;
                if(stat >= 6)
                {
                    stat = 6;
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            if(stat > -6)
            {
                stat += stages;
                if(stat <= -6)
                {
                    stat = -6;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void Faint()
    {
        // TODO: ACTUALLY CREATE THIS METHOD
        curHP = 0;
        hasStatus = StatusEffect.fainted;
    }

    public Effectiveness GetEffectiveness(TypeData attackingType)
    {
        int damageModifier = 0;

        // If Primary type is weak to attack type, add 2 to damage multiplier
        // Else if Primary type is resistant to attack type, subtract 2 from damage multiplier
        if (primaryType.FindInWeaknesses(attackingType))
        {
            damageModifier += 2;
        }
        else if (primaryType.FindInResistances(attackingType))
        {
            damageModifier -= 2;
        }
        if (secondaryType != null)
        {
            // Same for secondary type
            if (secondaryType.FindInWeaknesses(attackingType))
            {
                damageModifier += 2;
            }
            else if (secondaryType.FindInResistances(attackingType))
            {
                damageModifier -= 2;
            }
        }

        // If either primary or secondary type is immune to attacking type, the damage multiplier is always -10 (immune)
        if (primaryType.FindInImmunities(attackingType))
        {
            damageModifier = -10;
        }
        if (secondaryType != null)
        {
            if (secondaryType.FindInImmunities(attackingType))
            {
                damageModifier = -10;
            }
        }

        return (Effectiveness)damageModifier;
    }
}
