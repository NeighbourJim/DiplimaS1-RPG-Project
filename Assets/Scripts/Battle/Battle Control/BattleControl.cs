//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    public GameObject dataController;
    SoundManager soundManager;

    public GameObject playerSpawn;
    public GameObject enemSpawn;

    GameObject playerModel;
    GameObject enemyModel;

    public BattleType battleType;
    public MonData playerMonBase;
    public MonData enemyMonBase;
    public MonBattleData playerMon;
    public MonBattleData enemyMon;

    public GameObject battleUIController;
    BattleUIControl uiButtonControl;
    BattleTextUIControl uiTextControl;
    BattleDialogue battleDialogue;
    BattleHPControl uiHPControl;


    BattleStateControl stateControl;

    Monpedia mp;

    int fleeAttempts;

    void Start ()
    {
        dataController = GameObject.Find("GameDataController");
        soundManager = FindObjectOfType<SoundManager>();
        mp = dataController.GetComponent<Monpedia>();
        playerMon = ScriptableObject.CreateInstance<MonBattleData>();
        enemyMon = ScriptableObject.CreateInstance<MonBattleData>();
        stateControl = GetComponent<BattleStateControl>();
        uiButtonControl = battleUIController.GetComponent<BattleUIControl>();
        uiHPControl = battleUIController.GetComponent<BattleHPControl>();
        battleDialogue = battleUIController.GetComponent<BattleDialogue>();
        fleeAttempts = 0;

        soundManager.PlayMusic("Battle");
    }

    public void InitiateWildBattle(MonData player, MonData enemy)
    {
        playerMonBase = player;
        enemyMonBase = enemy;
        playerMon.SetData(player);
        enemyMon.SetData(enemy);
        Spawn();
        SetButtonColours();
        SetButtonNames();
        battleUIController.GetComponent<BattleXPBarControl>().SetXPBarSize(playerMon);
        battleType = BattleType.WildFleeable;
    }

    public void InitiateTrainerBattle(MonData[] playerTeam, MonData[] enemyTeam)
    {
        Spawn();
        SetButtonColours();
        SetButtonNames();
    }

    public void SetPlayerVisibility(bool val)
    {
        playerModel.SetActive(val);
    }

    public void SetEnemyVisibility(bool val)
    {
        enemyModel.SetActive(val);
    }

    void Spawn()
    {

        playerModel = Instantiate(mp.FindByID(playerMon.monID).monsterPrefab);
        enemyModel = Instantiate(mp.FindByID(enemyMon.monID).monsterPrefab);

        playerModel.tag = "PlayerMonster";
        enemyModel.tag = "EnemyMonster";

        playerMon.ownership = Ownership.player;
        enemyMon.ownership = Ownership.wild;

        playerMon.CalculateAllStatsNoHeal(playerMon.level);
        enemyMon.GenerateWildStats(enemyMon.level);

        Face();
        SetPlayerVisibility(false);
        SetEnemyVisibility(false);
    }

    void Face()
    {
        playerModel.transform.position = playerSpawn.transform.position;
        enemyModel.transform.position = enemSpawn.transform.position;

        playerModel.transform.LookAt(new Vector3(enemyModel.transform.position.x - 5, enemyModel.transform.position.y, enemyModel.transform.position.z));
        enemyModel.transform.LookAt(playerModel.transform);
    }

    #region User Interface
    void SetButtonColours()
    {
        if (playerMon.learnedMoves[0] != null)
        {
            uiButtonControl.ChangeButtonColour(uiButtonControl.m1, playerMon.learnedMoves[0].moveType);
        }
        if (playerMon.learnedMoves[1] != null)
        {
            uiButtonControl.ChangeButtonColour(uiButtonControl.m2, playerMon.learnedMoves[1].moveType);
        }
        if (playerMon.learnedMoves[2] != null)
        {
            uiButtonControl.ChangeButtonColour(uiButtonControl.m3, playerMon.learnedMoves[2].moveType);
        }
        if (playerMon.learnedMoves[3] != null)
        {
            uiButtonControl.ChangeButtonColour(uiButtonControl.m4, playerMon.learnedMoves[3].moveType);
        }
    }

    void SetButtonNames()
    {
        if (playerMon.learnedMoves[0] != null)
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m1, playerMon.learnedMoves[0].moveName);
            uiButtonControl.m1.enabled = true;
            uiButtonControl.m1.interactable = true;
        }
        else
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m1, "---");
            uiButtonControl.m1.enabled = false;
            uiButtonControl.m1.interactable = false;
        }
        if (playerMon.learnedMoves[1] != null)
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m2, playerMon.learnedMoves[1].moveName);
            uiButtonControl.m2.enabled = true;
            uiButtonControl.m2.interactable = true;
        }
        else
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m2, "---");
            uiButtonControl.m2.enabled = false;
            uiButtonControl.m2.interactable = false;
        }
        if (playerMon.learnedMoves[2] != null)
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m3, playerMon.learnedMoves[2].moveName);
            uiButtonControl.m3.enabled = true;
            uiButtonControl.m3.interactable = true;
        }
        else
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m3, "---");
            uiButtonControl.m3.enabled = false;
            uiButtonControl.m3.interactable = false;
        }
        if (playerMon.learnedMoves[3] != null)
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m4, playerMon.learnedMoves[3].moveName);
            uiButtonControl.m4.enabled = true;
            uiButtonControl.m4.interactable = true;
        }
        else
        {
            uiButtonControl.ChangeButtonText(uiButtonControl.m4, "---");
            uiButtonControl.m4.enabled = false;
            uiButtonControl.m4.interactable = false;
        }
    }

    void UpdateHPBar(MonBattleData monster)
    {
        uiHPControl.UpdateMonsterHP(monster);
    }
    #endregion

    public void SetSelectedMove(string buttonName)
    {
        switch (buttonName)
        {
            case("Move1Button"):
                playerMon.selectedMove = playerMon.learnedMoves[0];
                break;
            case("Move2Button"):
                playerMon.selectedMove = playerMon.learnedMoves[1];
                break;
            case("Move3Button"):
                playerMon.selectedMove = playerMon.learnedMoves[2];
                break;
            case("Move4Button"):
                playerMon.selectedMove = playerMon.learnedMoves[3];
                break;
            default:
                playerMon.selectedMove = null;
                break;
        }
        stateControl.AdvanceState(TurnState.EnemySelectAction);
    }

    public void SelectEnemyMove()
    {
        List<MoveData> selectableMovesList = new List<MoveData>();
        List<MoveData> strongMovesList = new List<MoveData>();
        MoveData[] selectable;
        MoveData[] strong;

        foreach(MoveData move in enemyMon.learnedMoves)
        {
            if(move != null)
            {
                selectableMovesList.Add(move);
            }
        }

        foreach(MoveData move in selectableMovesList)
        {
            if(playerMon.GetEffectiveness(move.moveType) > 0)
            {
                strongMovesList.Add(move);
            }
        }
        selectable = selectableMovesList.ToArray();
        strong = strongMovesList.ToArray();

        if(strong.Length > 0)
        {
            enemyMon.selectedMove = strong[Random.Range(0, strong.Length)];
        }
        else
        {
            enemyMon.selectedMove = selectable[Random.Range(0, selectable.Length)];
        }
    }

    #region Attack Resolution
    public void ResolveAttack(MoveData move, MonBattleData attacker, MonBattleData defender)
    {
        if(move.physSpec == PhysSpec.Physical || move.physSpec == PhysSpec.Special)
        {
            ResolveDamagingAttack(move, attacker, defender);
        }
        else
        {
            ResolveNonDamagingAttack(move, attacker, defender);
        }
    }

    void ResolveDamagingAttack(MoveData move, MonBattleData attacker, MonBattleData defender)
    {
        bool crit;
        Effectiveness effectiveness;
        int damage;

        if(attacker == playerMon)
        {
            playerModel.GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            enemyModel.GetComponent<Animator>().SetTrigger("Attack");
        }

        if (CheckIfHit(move, attacker, defender))
        {
            crit = CheckIfCrit(move);
            effectiveness = defender.GetEffectiveness(move.moveType);
            damage = CalculateDamage(move, attacker, defender, crit);
            if (crit)
            {
                battleDialogue.AddToMessages("A critical hit!");
            }
            if (effectiveness == Effectiveness.resist2x || effectiveness == Effectiveness.resist4x)
            {
                battleDialogue.AddToMessages("It's not very effective...");
            }
            else if (effectiveness == Effectiveness.weak2x || effectiveness == Effectiveness.weak4x)
            {
                battleDialogue.AddToMessages("It's super effective!!!");
            }
            if (effectiveness == Effectiveness.immune)
            {
                battleDialogue.AddToMessages("It had no effect...");
            }
            defender.TakeDamage(damage);
            if(defender == playerMon)
            {
                playerModel.GetComponent<Animator>().SetTrigger("Damage");
            }
            else
            {
                enemyModel.GetComponent<Animator>().SetTrigger("Damage");
            }

            if (move.causesStatus != StatusEffect.none)
            {
                ResolveStatus(move, defender);
            }
            if (move.statToChange != MonStat.none)
            {
                if (move.affectSelf)
                {
                    ResolveStatChange(move, attacker);
                }
                else
                {
                    ResolveStatChange(move, defender);
                }
            }

            UpdateHPBar(defender);
            UpdateHPBar(attacker);
        }
        else
        {
            battleDialogue.AddToMessages("But it missed!");
        }
    }

    void ResolveNonDamagingAttack(MoveData move, MonBattleData attacker, MonBattleData defender)
    {
        Effectiveness effectiveness;

        if (attacker == playerMon)
        {
            playerModel.GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            enemyModel.GetComponent<Animator>().SetTrigger("Attack");
        }

        effectiveness = defender.GetEffectiveness(move.moveType);
        if (effectiveness != Effectiveness.immune && CheckIfHit(move, attacker, defender))
        {
            if (move.causesStatus != StatusEffect.none)
            {
                ResolveStatus(move, defender);
            }
            if (move.statToChange != MonStat.none)
            {
                if (move.affectSelf)
                {
                    ResolveStatChange(move, attacker);
                }
                else
                {
                    ResolveStatChange(move, defender);
                }
            }

            if (defender == playerMon)
            {
                playerModel.GetComponent<Animator>().SetTrigger("Damage");
            }
            else
            {
                enemyModel.GetComponent<Animator>().SetTrigger("Damage");
            }

            UpdateHPBar(defender);
            UpdateHPBar(attacker);
        }
        else
        {
            battleDialogue.AddToMessages("It had no effect...");
        }
    }

    void ResolveStatus(MoveData move, MonBattleData monster)
    {
        if (CheckEffectHit(move.statusChance))
        {
            if (monster.GainStatus(move.causesStatus))
            {
                battleDialogue.AddToMessages(string.Format("{0} was afflicted with {1}!", monster.monName, move.causesStatus.ToString()));
            }
            else
            {
                battleDialogue.AddToMessages(string.Format("{0} was not affected...", monster.monName));
            }
        }
    }

    void ResolveStatChange(MoveData move, MonBattleData monster)
    {
        string changeDesc = "";

        if(monster.ModStat(move.statToChange, move.buffAmount))
        {
            if(move.buffAmount > 0)
            {
                if(move.buffAmount == 1)
                {
                    changeDesc = "";
                }
                else if(move.buffAmount == 2)
                {
                    changeDesc = " sharply";
                }
                else if(move.buffAmount >= 3)
                {
                    changeDesc = " drastically";
                }

                battleDialogue.AddToMessages(string.Format("{0}'s {1} rose{2}!", monster.monName, move.statToChange.ToString(), changeDesc));
            }
            else
            {
                if (move.buffAmount == -1)
                {
                    changeDesc = "";
                }
                else if (move.buffAmount == -2)
                {
                    changeDesc = " sharply";
                }
                else if (move.buffAmount <= -3)
                {
                    changeDesc = " drastically";
                }

                battleDialogue.AddToMessages(string.Format("{0}'s {1} fell{2}!", monster.monName, move.statToChange.ToString(), changeDesc));
            }
        }
        else
        {
            battleDialogue.AddToMessages(string.Format("{0}'s {1} can't change any more!", monster.monName, move.statToChange));
        }
    }

    bool CheckIfHit(MoveData move, MonBattleData attacker, MonBattleData defender)
    {
        int rand = Random.Range(1, 101);
        int threshold;

        threshold = Mathf.FloorToInt(move.accuracy * GetAccEvaMultiplier(attacker.buffStageAcc, defender.buffStageEva));

        if(rand <= threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    

    bool CheckIfCrit(MoveData move)
    {
        int rand = Random.Range(1, 101);
        float threshold;
        if (move.increasedCritRatio)
        {
            threshold = 1f / 8f;
        }
        else
        {
            threshold = 1f / 16f;
        }

        if(rand <= (int)threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool CheckEffectHit(int baseChance)
    {
        int rand = Random.Range(1, 101);

        return rand <= baseChance;
    }

    public int CalculateDamage(MoveData move, MonBattleData attacker, MonBattleData defender, bool criticalHit)
    {
        // Damage Calculation Formula is ((((2 * L / 5 + 2) * P * A / D) / 50 + 2) * M) / 100
        // Where: 
        // L is Attacker's Level
        // P is the Base Power of the move
        // A is the Attacker's attacking stat, modified by their stat boosts
        // D is the Defender's defending stat, modified by their stat boosts
        // M is an additional modifier, calculated separately
        // Source: https://bulbapedia.bulbagarden.net/wiki/Damage#Damage_calculation
        int dmg;
        int L;
        int P;
        int A;
        int D;
        int M;

        L = attacker.level;
        P = move.basePower;
        if(move.physSpec == PhysSpec.Physical)
        {
            A = Mathf.FloorToInt(attacker.curAtk * GetStatMultiplier(attacker.buffStageAtk));
            D = Mathf.FloorToInt(defender.curDef * GetStatMultiplier(defender.buffStageDef));
        }
        else
        {
            A = Mathf.FloorToInt(attacker.curSpAtk * GetStatMultiplier(attacker.buffStageSpAtk));
            D = Mathf.FloorToInt(defender.curSpDef * GetStatMultiplier(defender.buffStageSpDef));
        }

        M = CalculateDamageModifier(move, attacker, defender, criticalHit);

        dmg = ((((2 * L / 5 + 2) * P * A / D) / 50 + 2) * M) / 100;

        return dmg;
    }

    private int CalculateDamageModifier(MoveData move, MonBattleData attacker, MonBattleData defender, bool criticalHit)
    {
        float modifier;
        int rand = 100;     // Random number that gives some variance in damage
        float stab = 1f;    // Stands for Same Type Attack Bonus, applies a modifier if the move used is the same type as the user
        float burn = 1f;    // Weakens physical moves if the user is burned
        float effect = 1f;  // Type effectiveness
        float critical = 1f;

        rand = Random.Range(85, 101);
        stab = 1f;

        if(attacker.primaryType == move.moveType)
        {
            stab = 1.5f;
        }
        else if(attacker.secondaryType != null)
        {
            if(attacker.secondaryType == move.moveType)
            {
                stab = 1.5f;
            }
        }

        if(move.physSpec == PhysSpec.Physical && attacker.hasStatus == StatusEffect.Burn)
        {
            burn = 0.5f;
        }
        else
        {
            burn = 1.0f;
        }

        if (criticalHit)
        {
            critical = 2f;
        }

        effect = GetEffectivenessModifier(defender.GetEffectiveness(move.moveType));

        modifier = rand * stab * effect * burn * critical;

        return Mathf.FloorToInt(modifier);
    }

    private float GetEffectivenessModifier(Effectiveness eff)
    {
        // returns the damage multiplier for a type's effectiveness
        // resisting halves or quarters the damage, while weak doubles or quadruples it
        switch (eff)
        {
            case Effectiveness.immune:
                return 0f;
            case Effectiveness.resist2x:
                return 0.5f;
            case Effectiveness.resist4x:
                return 0.25f;
            case Effectiveness.weak2x:
                return 2f;
            case Effectiveness.weak4x:
                return 4f;
            default:
                return 1f;
        }
    }

    public float GetStatMultiplier(int stage)
    {
        // This method returns the multiplier for a stat based on its buff stage
        // Eg: 1 stage buffs a stat by 1.5x, 2 stages by 2x, etc.
        // While -1 stage is 0.66x, -2 is 0.5x, etc.
        float mult = 1f;

        if (stage < 0)
        {
            mult = 2f / (2 + (stage * -1f));
        }
        else if (stage > 0)
        {
            mult = 2 + stage / 2;
        }

        return mult;
    }

    public float GetAccEvaMultiplier(int attackerAccStage, int defenderEvaStage)
    {
        // This method returns the multiplier for accuracy or evasion, based on its buff stage
        // Accuracy and Evasion work slightly differently to the other stats
        // 1 stage is 1.33x, then 1.66x, etc.
        // -1 stage is 0.75x, then 0.6x, etc.
        float mult;
        int stage = attackerAccStage - defenderEvaStage;

        // This is gross, I know. Needs to be refactored at some point.
        switch (stage)
        {
            case -6:
                mult = 3f / 9f;
                break;
            case -5:
                mult = 3f / 8f;
                break;
            case -4:
                mult = 3f / 7f;
                break;
            case -3:
                mult = 3f / 6f;
                break;
            case -2:
                mult = 3f / 5f;
                break;
            case -1:
                mult = 3f / 4f;
                break;
            case 1:
                mult = 4f / 3f;
                break;
            case 2:
                mult = 5f / 3f;
                break;
            case 3:
                mult = 6f / 3f;
                break;
            case 4:
                mult = 7f / 3f;
                break;
            case 5:
                mult = 8f / 3f;
                break;
            case 6:
                mult = 9f / 3f;
                break;
            default:
                mult = 1f;
                break;
        }

        return mult;
    }

    void SetFainted()
    {
        if (playerMon.curHP == 0)
            playerMon.hasStatus = StatusEffect.fainted;
        if (enemyMon.curHP == 0)
            enemyMon.hasStatus = StatusEffect.fainted;
    }
    #endregion

    #region Status Resolution

    public void ResolveEndTurnStatusEffect(MonBattleData monster)
    {
        switch (monster.hasStatus)
        {
            case (StatusEffect.Burn):
                battleDialogue.AddToMessages(string.Format("{0} took damage from it's burn.", monster.monName));
                monster.TakeDamage(Mathf.FloorToInt(monster.maxHP * 0.06f));
                if (monster == playerMon)
                {
                    playerModel.GetComponent<Animator>().SetTrigger("Attack");
                }
                else
                {
                    enemyModel.GetComponent<Animator>().SetTrigger("Attack");
                }
                break;
            case (StatusEffect.Poison):
                battleDialogue.AddToMessages(string.Format("{0} took damage from poison.", monster.monName));
                monster.TakeDamage(Mathf.FloorToInt(monster.maxHP * 0.12f));
                if (monster == playerMon)
                {
                    playerModel.GetComponent<Animator>().SetTrigger("Attack");
                }
                else
                {
                    enemyModel.GetComponent<Animator>().SetTrigger("Attack");
                }
                break;
        }
    }

    public bool ResolveMidTurnStatusEffect(MonBattleData monster)
    {
        switch (monster.hasStatus)
        {
            case (StatusEffect.Sleep):
                monster.remainingSleepTurns -= 1;
                if(monster.remainingSleepTurns <= 0)
                {
                    monster.remainingSleepTurns = 0;
                    monster.HealStatus();
                    battleDialogue.AddToMessages(string.Format("{0} awoke from sleep!", monster.monName));
                    return true;
                }
                battleDialogue.AddToMessages(string.Format("{0} is still asleep!", monster.monName));
                return false;
            case (StatusEffect.Paralysis):
                if (CheckEffectHit(50))
                {
                    return true;
                }
                battleDialogue.AddToMessages(string.Format("{0} is fully paralyzed!", monster.monName));
                return false;
            case (StatusEffect.Frozen):
                if (CheckEffectHit(20))
                {
                    monster.HealStatus();
                    battleDialogue.AddToMessages(string.Format("{0} thawed out!", monster.monName));
                    return true;
                }
                battleDialogue.AddToMessages(string.Format("{0} is still frozen!", monster.monName));
                return false;
            case (StatusEffect.Confusion):
                {
                    if (CheckEffectHit(50))
                    {
                        return true;
                    }
                    else
                    {
                        int dmg = CalculateConfuseDamage(monster);
                        monster.TakeDamage(dmg);
                        battleDialogue.AddToMessages(string.Format("{0} hit itself in confusion for {1} damage...", monster.monName, dmg));
                        return false;
                    }
                }
            default:
                return true;
        }
    }

    int CalculateConfuseDamage(MonBattleData monster)
    {
        int dmg;
        int L;
        int P;
        int A;
        int D;
        int M;

        L = monster.level;
        P = 40;
        A = Mathf.FloorToInt(monster.curAtk * GetStatMultiplier(monster.buffStageAtk));
        D = Mathf.FloorToInt(monster.curDef * GetStatMultiplier(monster.buffStageDef));
        M = CalculateConfuseModifier(monster);

        dmg = ((((2 * L / 5 + 2) * P * A / D) / 50 + 2) * M) / 100;

        return dmg;
    }

    int CalculateConfuseModifier(MonBattleData monster)
    {
        float modifier;
        int rand = 100;     // Random number that gives some variance in damage
        float stab = 1f;    // Stands for Same Type Attack Bonus, applies a modifier if the move used is the same type as the user
        float burn = 1f;    // Weakens physical moves if the user is burned
        float effect = 1f;  // Type effectiveness
        float critical = 1f;

        rand = Random.Range(85, 101);

        if (monster.hasStatus == StatusEffect.Burn)
        {
            burn = 0.5f;
        }
        else
        {
            burn = 1.0f;
        }

        modifier = rand * stab * effect * burn * critical;

        return Mathf.FloorToInt(modifier);
    }

    #endregion

    #region Flee Resolution

    public void Flee()
    {
        stateControl.AdvanceState(TurnState.FleeAttempt);
    }

    public bool TryToFlee()
    {
        // Formula for seeing if an escape attempt is as follows:
        // F = ( (A * 128 / B) + 30 * C ) % 256
        // Where:
        // A = Unmodified Speed of the player's active monster
        // B = Unmodified Speed of the opponent monster (minimum 1)
        // C = Number of times escape has been attempted during the battle (including the current attempt)
        // A random number between 0 and 255 is then generated, and if it is less than F, the flee attempt is successful

        float F;
        float A = playerMon.curSpeed;
        float B = (enemyMon.curSpeed > 1) ? enemyMon.curSpeed : 1f; // If the opposing monster has a speed > 1, use that. Otherwise make it 1.
        float C = ++fleeAttempts;

        F = ((A * 128 / B) + 30 * C) % 256;

        return Random.Range(0, 256) < F;
    }

    #endregion

    #region XP Distribution

    public bool DistributeXP()
    {
        int lvlB = playerMon.level;
        int xpToDist = enemyMon.GetXPValue();
        playerMon.IncrementExp(xpToDist);
        battleUIController.GetComponent<BattleXPBarControl>().SetXPBarSize(playerMon);
        battleDialogue.AddToMessages(string.Format("Gained {0} experience!", xpToDist));
        if(lvlB < playerMon.level)
        {
            battleDialogue.AddToMessages(string.Format("{0} advanced to level {1}!", playerMon.monName, playerMon.level));
            if(playerMon.CheckLevelMove() != null)
            {
                if (playerMon.LearnMove(playerMon.CheckLevelMove()))
                {
                    battleDialogue.AddToMessages(string.Format("{0} learned {1}!", playerMon.monName, playerMon.CheckLevelMove().moveName));
                }
                else
                {
                    battleDialogue.AddToMessages(string.Format("{0} is trying to learn {1}, but cannot learn more than 4 moves.", playerMon.monName, playerMon.CheckLevelMove().moveName));
                    battleDialogue.AddToMessages(string.Format("Select which move, if any, to replace with the new move."));
                    stateControl.AdvanceState(TurnState.MonsterLearningNewMove);
                    UpdateHPBar(playerMon);
                    return true;
                }
            }
        }
        UpdateHPBar(playerMon);
        return false;
    }


    #endregion
}
