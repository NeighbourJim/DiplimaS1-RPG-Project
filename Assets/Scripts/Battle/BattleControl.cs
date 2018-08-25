//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    public GameObject dataController;

    public GameObject playerSpawn;
    public GameObject enemSpawn;

    public GameObject player;
    public GameObject enemy;

    public MonBattleData playerMon;
    public MonBattleData enemyMon;

    public GameObject battleUI;
    BattleUIControl uiScript;
    BattleStateControl stateControl;

    Monpedia mp;

    void Start ()
    {
        mp = dataController.GetComponent<Monpedia>();
        playerMon = Instantiate(new MonBattleData(mp.monpedia[1]));
        enemyMon = Instantiate(new MonBattleData(mp.monpedia[4]));
        print(playerMon.monName);
        Spawn();
        stateControl = GetComponent<BattleStateControl>();
        uiScript = battleUI.GetComponent<BattleUIControl>();

        SetButtonColours();
        SetButtonNames();
    }

    void Spawn()
    {
        player = Instantiate(playerMon.monsterPrefab);
        enemy = Instantiate(enemyMon.monsterPrefab);

        player.tag = "PlayerMonster";
        enemy.tag = "EnemyMonster";

        playerMon.ownership = Ownership.player;
        enemyMon.ownership = Ownership.wild;

        playerMon.GenerateWildStats(5);
        enemyMon.GenerateWildStats(6);

        Face();

    }

    void Face()
    {
        player.transform.position = playerSpawn.transform.position;
        enemy.transform.position = enemSpawn.transform.position;

        player.transform.LookAt(new Vector3(enemy.transform.position.x - 5, enemy.transform.position.y, enemy.transform.position.z));
        enemy.transform.LookAt(player.transform);
    }

    void SetButtonColours()
    {
        if (playerMon.learnedMoves[0] != null)
        {
            uiScript.ChangeButtonColour(uiScript.m1, playerMon.learnedMoves[0].moveType);
        }
        if (playerMon.learnedMoves[1] != null)
        {
            uiScript.ChangeButtonColour(uiScript.m2, playerMon.learnedMoves[1].moveType);
        }
        if (playerMon.learnedMoves[2] != null)
        {
            uiScript.ChangeButtonColour(uiScript.m3, playerMon.learnedMoves[2].moveType);
        }
        if (playerMon.learnedMoves[3] != null)
        {
            uiScript.ChangeButtonColour(uiScript.m4, playerMon.learnedMoves[3].moveType);
        }
    }

    void SetButtonNames()
    {
        if (playerMon.learnedMoves[0] != null)
        {
            uiScript.ChangeButtonText(uiScript.m1, playerMon.learnedMoves[0].moveName);
        }
        if (playerMon.learnedMoves[1] != null)
        {
            uiScript.ChangeButtonText(uiScript.m2, playerMon.learnedMoves[1].moveName);
        }
        if (playerMon.learnedMoves[2] != null)
        {
            uiScript.ChangeButtonText(uiScript.m3, playerMon.learnedMoves[2].moveName);
        }
        if (playerMon.learnedMoves[3] != null)
        {
            uiScript.ChangeButtonText(uiScript.m4, playerMon.learnedMoves[3].moveName);
        }
    }

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

    public void ResolveAttack(MoveData move, MonBattleData attacker, MonBattleData defender)
    {
        bool crit;
        Effectiveness effectiveness;
        int damage;

        if(CheckIfHit(move, attacker, defender))
        {
            crit = CheckIfCrit(move);
            effectiveness = defender.GetEffectiveness(move.moveType);
            damage = CalculateDamage(move, attacker, defender, crit);
            if (crit)
            {
                print("A critical hit!");
            }
            if(effectiveness == Effectiveness.resist2x || effectiveness == Effectiveness.resist4x)
            {
                print("It's not very effective...");
            }
            else if(effectiveness == Effectiveness.weak2x || effectiveness == Effectiveness.weak4x)
            {
                print("It's super effective!!!");
            }
            if(effectiveness == Effectiveness.immune)
            {
                print("It had no effect...");
            }
            defender.TakeDamage(damage);
            print(string.Format("Dealt {0} damage. {1} has {2}/{3} health remaining.", damage, defender.monName, defender.curHP, defender.maxHP));
        }
        else
        {
            print("But it missed!");
        }
    }

    public bool CheckIfHit(MoveData move, MonBattleData attacker, MonBattleData defender)
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

    public bool CheckIfCrit(MoveData move)
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

    public int CalculateDamage(MoveData move, MonBattleData attacker, MonBattleData defender, bool criticalHit)
    {
        int dmg;
        int L;
        int P;
        int A;
        int D;
        int M;

        L = attacker.level;
        P = move.basePower;
        if(move.physSpec == PhysSpec.physical)
        {
            A = attacker.curAtk * (int)GetStatMultiplier(attacker.buffStageAtk);
            D = defender.curDef * (int)GetStatMultiplier(defender.buffStageDef);
        }
        else
        {
            A = attacker.curSpAtk * (int)GetStatMultiplier(attacker.buffStageSpAtk);
            D = defender.curSpDef * (int)GetStatMultiplier(defender.buffStageSpDef);
        }

        M = CalculateDamageModifier(move, attacker, defender, criticalHit);

        // Source: https://bulbapedia.bulbagarden.net/wiki/Damage#Damage_calculation
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

        if(move.physSpec == PhysSpec.physical && attacker.hasStatus == StatusEffect.burned)
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
}
