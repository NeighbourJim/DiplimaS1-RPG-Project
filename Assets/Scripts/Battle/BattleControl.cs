using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {

    DataList data;

    public GameObject playerSpawn;
    public GameObject enemSpawn;

    public GameObject player;
    public GameObject enemy;

    public MonData playerMon;
    public MonData enemyMon;

    public GameObject battleUI;
    BattleUIControl uiScript;
    BattleStateControl stateControl;

    void Start ()
    {                
        Spawn();
        data = GetComponent<DataList>();
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

        print(playerMon.pType.typeName);
    }

    void Face()
    {
        player.transform.position = playerSpawn.transform.position;
        enemy.transform.position = enemSpawn.transform.position;

        player.transform.LookAt(new Vector3(enemy.transform.position.x - 5, enemy.transform.position.y, enemy.transform.position.z));
        enemy.transform.LookAt(player.transform);
    }

    private void Update()
    {
        
    }

    void SetButtonColours()
    {
        uiScript.ChangeButtonColour(uiScript.m1, playerMon.learnedMoves[0].type);
        uiScript.ChangeButtonColour(uiScript.m2, playerMon.learnedMoves[1].type);
        uiScript.ChangeButtonColour(uiScript.m3, playerMon.learnedMoves[2].type);
        uiScript.ChangeButtonColour(uiScript.m4, playerMon.learnedMoves[3].type);
    }

    void SetButtonNames()
    {
        uiScript.ChangeButtonText(uiScript.m1, playerMon.learnedMoves[0].moveName);
        uiScript.ChangeButtonText(uiScript.m2, playerMon.learnedMoves[1].moveName);
        uiScript.ChangeButtonText(uiScript.m3, playerMon.learnedMoves[2].moveName);
        uiScript.ChangeButtonText(uiScript.m4, playerMon.learnedMoves[3].moveName);
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
            if(playerMon.GetEffectiveness(move.type) > 0)
            {
                strongMovesList.Add(move);
            }
        }

        selectable = selectableMovesList.ToArray();
        strong = strongMovesList.ToArray();

        if(strong.Length > 0)
        {
            enemyMon.selectedMove = strong[UnityEngine.Random.Range(0, strong.Length)];
        }
        else
        {
            enemyMon.selectedMove = selectable[UnityEngine.Random.Range(0, selectable.Length)];
        }
    }
}
