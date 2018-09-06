using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public TypeData primaryType;
    public TypeData secondaryType; // hello

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
    
    [Header("HP and Status")]
    public int maxHP;
    public int curHP;
    public StatusEffect hasStatus = StatusEffect.none;
    public int remainingSleepTurns = 0;

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

    #endregion

    private void Start()
    {

    }
    public void CreateFromBase(MonData bm)
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
        if (bm.curXP == 0)
        {
            bm.CalculateXPToNextLevel(bm.level - 1); // set current xp to exactly the amount to level up to current level
        }
        else
        {
            curXP = bm.curXP;
        }
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

    public void GenerateMoveset()
    {
        int i = 0;
        foreach (LevelMovePair lmp in learnset)
        {
            if (lmp.levelToLearn >= level)
            {
                break;
            }
            i++;
        }
        for(int j = 0; j < 4; j++)
        {
            try
            {
                learnedMoves[j] = learnset[i-1].moveToLearn;
                i--;
            }
            catch(System.IndexOutOfRangeException e)
            {
                learnedMoves[j] = null;
            }
        }
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
        level = lvl;
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

    
}
