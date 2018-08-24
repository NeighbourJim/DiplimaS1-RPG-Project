using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class MonData : ScriptableObject
{
    #region: Field Declarations

    [Header("Model Prefab")]
    public GameObject monsterPrefab;

    [Header("Audio")]
    public AudioClip cry;

    [Header("Identity")]
    public int monID;
    public string monName;
    [TextArea(2,3)]
    public string monpediaEntry = "?ENTRY?";
    public string nickname;
    public Ownership ownership = Ownership.wild;

    [Tooltip("All monsters must have at least a primary type.")]
    [Header("Type")]
    public TypeNum primaryType = TypeNum.normal;
    public TypeNum secondaryType = TypeNum.none;
    public MonType pType;
    public MonType sType;

    [Header("Level / XP")]
    public int level = 1;
    public int curXP;
    public int xpToNextLevel;
    public int xpYield = 64; 

    [Header("Evolution")]
    [Tooltip("Whether or not the monster evolves.")]
    public bool evolves = false;
    [Tooltip("What level the monster evolves at")]
    public int levelToEvolve = -1;
    [Tooltip("Which monster it evolves into")]
    public MonData evolvesInto;

    [Tooltip("The base stats for the monster, used in calculating its current stats for each level.")]
    [Header("Base Stats")]
    public int baseHP = 30;
    public int baseAtk = 30;
    public int baseDef = 30;
    public int baseSpAtk = 30;
    public int baseSpDef = 30;
    public int baseSpeed = 30;

    [Tooltip("These values allow for variance in stats between monsters of the same species, and are calculated when the monster is initially created.")]
    [Header("Individual Values")]
    public int ivHP = 0;
    public int ivAtk = 0;
    public int ivDef = 0;
    public int ivSpAtk = 0;
    public int ivSpDef = 0;
    public int ivSpeed = 0;

    [Tooltip("Current values for stats, calculated at runtime using IVs and Level.")]
    [Header("Current Stats")]
    public int curAtk;
    public int curDef;
    public int curSpAtk;
    public int curSpDef;
    public int curSpeed;

    [Tooltip("These stats work differently to regular base stats in that they are multipliers applied to a move's accuracy.")]
    [Header("Accuracy & Evasion")]
    public int curAcc = 100;
    public int curEva = 100;

    [Header("HP and Status")]
    public int maxHP;
    public int curHP;
    public StatusEffect hasStatus = StatusEffect.none;

    [Header("Stat Boosts")]    
    public int buffStageAtk = 0;
    public int buffStageDef = 0;
    public int buffStageSpAtk = 0;
    public int buffStageSpDef = 0;
    public int buffStageSpeed = 0;
    public int buffStageAcc = 0;
    public int buffStageEva = 0;

    [Header("Moves")]
    public MoveData[] learnedMoves = new MoveData[4];
    public MoveData selectedMove;

    [System.Serializable]
    public struct LevelMovePair
    {
        public int levelToLearn;
        public MoveData moveToLearn;
    }

    [Header("Monster Learnset")]
    public LevelMovePair[] learnset;

    private TypeList typeList = new TypeList();

    #endregion

    private void Start()
    {
        pType = typeList.typeList[(int)primaryType];
        sType = typeList.typeList[(int)secondaryType];
    }

    private void CalculateXPToNextLevel(int lvl)
    {
        xpToNextLevel = (int)Mathf.Pow(lvl, 3);
    }

    public int CalculateStat(int statBase, int statIV, int level)
    {
        return (int)Mathf.Floor(Mathf.Floor(2 * statBase + statIV + 0) * level / 100 + 5);
    }

    public int CalculateMaxHP(int hpBase, int hpIV, int level)
    {
        return (int)Mathf.Floor(Mathf.Floor(2 * hpBase + hpIV + 0) * level / 100 + level + 10);
    }

    private void GenerateIVs()
    {
        // Set each IV to a random value between 0 and 31
        ivHP = Random.Range(0, 32);
        ivAtk = Random.Range(0, 32);
        ivDef = Random.Range(0, 32);
        ivSpAtk = Random.Range(0, 32);
        ivSpDef = Random.Range(0, 32);
        ivSpeed = Random.Range(0, 32);
    }

    public void CalculateAllStats(int lvl)
    {
        maxHP = CalculateMaxHP(baseHP, ivHP, lvl);
        curHP = maxHP;
        curAtk = CalculateStat(baseAtk, ivAtk, lvl);
        curDef = CalculateStat(baseDef, ivDef, lvl);
        curSpAtk = CalculateStat(baseSpAtk, ivSpAtk, lvl);
        curSpDef = CalculateStat(baseSpDef, ivSpDef, lvl);
        curSpeed = CalculateStat(baseSpeed, ivSpeed, lvl);
    }

    public void GenerateWildStats(int lvl)
    {
        GenerateIVs();
        CalculateAllStats(lvl);
        CalculateXPToNextLevel(lvl - 1);
        curXP = xpToNextLevel;
        CalculateXPToNextLevel(lvl);
    }

    public void IncrementExp(int expToAdd)
    {
        curXP += expToAdd;
        if (curXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        CalculateAllStats(level);
        CalculateXPToNextLevel(level);
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

    public void Faint()
    {
        // TODO: ACTUALLY CREATE THIS METHOD
        curHP = 0;
        hasStatus = StatusEffect.fainted;
    }

    public Effectiveness GetEffectiveness(TypeNum attackingType)
    {
        // Primary Type
        TypeNum[] pWeak = typeList.typeList[pType.typeID].weaknesses;
        TypeNum[] pRes = typeList.typeList[pType.typeID].resistances;
        TypeNum[] pImm = typeList.typeList[pType.typeID].immunities;
        // Secondary Type
        TypeNum[] sWeak = typeList.typeList[sType.typeID].weaknesses;
        TypeNum[] sRes = typeList.typeList[sType.typeID].resistances;
        TypeNum[] sImm = typeList.typeList[sType.typeID].immunities;

        int damageModifier = 0;

        // If Primary type is weak to attack type, add 2 to damage multiplier
        // Else if Primary type is resistant to attack type, subtract 2 from damage multiplier
        if (ArrayUtility.IndexOf(pWeak, attackingType) != -1)
        {
            damageModifier += 2;
        }
        else if (ArrayUtility.IndexOf(pRes, attackingType) != -1)
        {
            damageModifier -= 2;
        }
        // Same for secondary type
        if (ArrayUtility.IndexOf(sWeak, attackingType) != -1)
        {
            damageModifier += 2;
        }
        else if (ArrayUtility.IndexOf(sRes, attackingType) != -1)
        {
            damageModifier -= 2;
        }

        // If either primary or secondary type is immune to attacking type, the damage multiplier is always -10 (immune)
        if (ArrayUtility.IndexOf(pImm, attackingType) != -1 || ArrayUtility.IndexOf(sImm, attackingType) != -1)
        {
            damageModifier = -10;
        }

        return (Effectiveness)damageModifier;
    }
}
