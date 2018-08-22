using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Copymon : MonoBehaviour
{
    public int monID;
    public string monName;
    public string nickname;

    public Ownership ownership = Ownership.wild;
    
    public TypeNum pType = TypeNum.normal;
    public TypeNum sType = TypeNum.none;

    private MonType primary;
    private MonType secondary;

    public int level = 1;
    public int baseHP = 30;
    public int baseAtk = 30;
    public int baseDef = 30;
    public int baseSpAtk = 30;
    public int baseSpDef = 30;
    public int baseSpeed = 30;

    // IVs (Individual Values) cause variation in Copymon stats between multiple of the same species
    // Each stat has an IV ranging from 0 to 31
    public int ivHP = 0;
    public int ivAtk = 0;
    public int ivDef = 0;
    public int ivSpAtk = 0;
    public int ivSpDef = 0;
    public int ivSpeed = 0;
    
    public int maxHP;    
    public int curHP;

    // Current values for stats, calculated using IVs and Level
    public int curAtk;
    public int curDef;
    public int curSpAtk;
    public int curSpDef;
    public int curSpeed;

    // Accuracy and evasion stats, which work differently
    // when compared to normal base stats
    public int curAcc = 100;
    public int curEva = 100;

    // Buff stages for each stat, effectively act as a stat multiplier
    // during damage calculations
    public int buffStageAtk = 0;
    public int buffStageDef = 0;
    public int buffStageSpAtk = 0;
    public int buffStageSpDef = 0;
    public int buffStageSpeed = 0;
    public int buffStageAcc = 0;
    public int buffStageEva = 0;

    public int curXP;
    public int xpToNextLevel;

    public StatusEffect hasStatus = StatusEffect.none;

    public int xpYield = 64;
    
    public int moveId1;
    public int moveId2;
    public int moveId3;
    public int moveId4;

    public int selectedMove = 0;

    private TypeList typeList = new TypeList();

    private void Awake()
    {
        // set the primary and secondary types to instances of the type class
        // using enum drop down set in the unity editor
        primary = typeList.typeList[(int)pType];
        secondary = typeList.typeList[(int)sType];
    }

    void CalcXPToNext()
    {
        xpToNextLevel = (int)Mathf.Pow(level, 3);
    }

    public int CalcStat(int stat, int indiv, int level, bool isHP)
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
            return (int)Mathf.Floor(Mathf.Floor(2 * stat + indiv + 0) * level / 100 + 5);
        }
        
        else
        {
            return (int)Mathf.Floor(Mathf.Floor(2 * stat + indiv + 0) * level / 100 + level + 10);
        }
    }

    void GenerateIVs()
    {
        // Set each IV to a random value between 0 and 31
        ivHP = Random.Range(0, 32);
        ivAtk = Random.Range(0, 32);
        ivDef = Random.Range(0, 32);
        ivSpAtk = Random.Range(0, 32);
        ivSpDef = Random.Range(0, 32);
        ivSpeed = Random.Range(0, 32);
    }

    public void CalculateStats(int lvl)
    {        
        level = lvl;
        maxHP = CalcStat(baseHP, ivHP, level, true);
        curHP = maxHP;
        curAtk = CalcStat(baseAtk, ivAtk, level, false);
        curDef = CalcStat(baseDef, ivDef, level, false);
        curSpAtk = CalcStat(baseSpAtk, ivSpAtk, level, false);
        curSpDef = CalcStat(baseSpDef, ivSpDef, level, false);
        curSpeed = CalcStat(baseSpeed, ivSpeed, level, false);
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
        CalculateStats(level);
        CalcXPToNext();
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

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if(curHP <= 0)
        {
            curHP = 0;
            Faint();
        }
    }

    public void HealDamage(int heal)
    {
        curHP += heal;
        if(curHP > maxHP)
        {
            curHP = maxHP;
        }
    }

    public void Faint()
    {

    }

    public Effectiveness GetEffectiveness(TypeNum attackType)
    {
        // Primary Type
        TypeNum[] pWeak = typeList.typeList[primary.typeID].weaknesses;
        TypeNum[] pRes = typeList.typeList[primary.typeID].resistances;
        TypeNum[] pImm = typeList.typeList[primary.typeID].immunities;
        // Secondary Type
        TypeNum[] sWeak = typeList.typeList[secondary.typeID].weaknesses;
        TypeNum[] sRes = typeList.typeList[secondary.typeID].resistances;
        TypeNum[] sImm = typeList.typeList[secondary.typeID].immunities;

        int damageModifier = 0;

        // If Primary type is weak to attack type, add 2 to damage multiplier
        // Else if Primary type is resistant to attack type, subtract 2 from damage multiplier
        if (ArrayUtility.IndexOf(pWeak, attackType) != -1)
        {
            damageModifier += 2;
        }
        else if(ArrayUtility.IndexOf(pRes, attackType) != -1)
        {
            damageModifier -= 2;
        }
        // Same for secondary type
        if (ArrayUtility.IndexOf(sWeak, attackType) != -1)
        {
            damageModifier += 2;
        }
        else if (ArrayUtility.IndexOf(sRes, attackType) != -1)
        {
            damageModifier -= 2;
        }

        // If either primary or secondary type is immune to attacking type, the damage multiplier is always -10 (immune)
        if(ArrayUtility.IndexOf(pImm, attackType) != -1 || ArrayUtility.IndexOf(sImm, attackType) != -1)
        {
            damageModifier = -10;
        }

        return (Effectiveness)damageModifier;
    }
}
