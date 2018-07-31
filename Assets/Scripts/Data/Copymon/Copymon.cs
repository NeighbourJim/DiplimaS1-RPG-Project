using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copymon : MonoBehaviour
{
    public int monID;
    public string monName;
    public string nickname;

    public Ownership ownership = Ownership.wild;
    
    public Type type = Type.normal;
    
    public int level = 1;
    public int baseHP;
    public int baseAtk;
    public int baseDef;
    public int baseSpAtk;
    public int baseSpDef;
    public int baseSpeed;

    public int ivHP;
    public int ivAtk;
    public int ivDef;
    public int ivSpAtk;
    public int ivSpDef;
    public int ivSpeed;
    
    public int maxHP;    
    public int curHP;

    public int curAtk;
    public int curDef;
    public int curSpAtk;
    public int curSpDef;
    public int curSpeed;

    public int curXP;
    public int xpToNextLevel;

    public StatusEffect hasStatus = StatusEffect.none;

    public int xpYield = 64;

    public Type[] weaknesses;

    public int moveId1;
    public int moveId2;
    public int moveId3;
    public int moveId4;

    private void Awake()
    {
        
    }

    void CalcXPToNext()
    {
        xpToNextLevel = (int)Mathf.Pow(level, 3);
    }

    public int CalcStat(int stat, int level, bool isHP)
    {
        // Calculate the current value of a stat given the base stat and level
        // HP is calculated slightly differently
        // HP = floor((2 * B + I + E) * L / 100 + L + 10)
        // Other Stats = floor( ((2 * B + I + E) * L / 100 + 5) * N )
        // B = Base Stat
        // I = Individual Value (to give mons of same species stat variance)
        // E = Effort Value/4 
        // L = Mon Level
        // N = Nature multiplier (I'm not bothering with natures for this prototype so it is excluded from the calculations)
        if (!isHP)
        {
            return (int)Mathf.Floor(Mathf.Floor(2 * stat + 0 + 0) * level / 100 + 5);
        }
        
        else
        {
            return (int)Mathf.Floor(Mathf.Floor(2 * stat + 0 + 0) * level / 100 + level + 10);
        }
    }

    public void GenerateStats(int lvl)
    {        
        level = lvl;
        maxHP = CalcStat(baseHP, level, true);
        curHP = maxHP;
        curAtk = CalcStat(baseAtk, level, false);
        curDef = CalcStat(baseDef, level, false);
        curSpAtk = CalcStat(baseSpAtk, level, false);
        curSpDef = CalcStat(baseSpDef, level, false);
        curSpeed = CalcStat(baseSpeed, level, false);
        CalcXPToNext();
    }

    public void IncrementExp(int expToAdd)
    {
        curXP += expToAdd;
        if(curXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        GenerateStats(level);
    }

    public int GetXPValue()
    {
        // Calculates how much experience to award if mon is fainted
        // a = Trainer Multiplier; 1.0 if wild, 1.5 if trainer.
        // b = base experience yield
        // L = level of copymon
        // s = number of player copymon that participated (at this stage, will always be 1)
        float exp;
        float a;
        int b;
        float L;
        float s;

        if (ownership == Ownership.trainer)
        {
            a = 1.5f;
        }
        else
        {
            a = 1.0f;
        }
        b = xpYield;
        L = this.level;
        s = 1.0f;

        exp = (a * b * L) / 7 * s;

        return Mathf.FloorToInt(exp);
    }

}

public enum Type
    {
        normal,
        grass,
        water,
        fire
    }

public enum Ownership
{
    wild,
    player,
    trainer
}
