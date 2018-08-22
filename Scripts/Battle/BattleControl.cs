using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {

    DataList data;

    public GameObject playerSpawn;
    public GameObject enemSpawn;

    public GameObject player;
    public GameObject enemy;

    public Copymon playerMon;
    public Copymon enemyMon;

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
        player = (GameObject)Instantiate(Resources.Load("CopymonPrefabs/CM001"));
        enemy = (GameObject)Instantiate(Resources.Load("CopymonPrefabs/CM002"));
        player.tag = "PlayerMonster";
        enemy.tag = "EnemyMonster";

        playerMon = player.GetComponent<Copymon>();
        enemyMon = enemy.GetComponent<Copymon>();

        playerMon.ownership = Ownership.player;

        playerMon.CalculateStats(5);
        enemyMon.CalculateStats(6);

        Face();
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
        uiScript.ChangeButtonColour(uiScript.m1, data.moveDex[playerMon.moveId1].type);
        uiScript.ChangeButtonColour(uiScript.m2, data.moveDex[playerMon.moveId2].type);
        uiScript.ChangeButtonColour(uiScript.m3, data.moveDex[playerMon.moveId3].type);
        uiScript.ChangeButtonColour(uiScript.m4, data.moveDex[playerMon.moveId4].type);
    }

    void SetButtonNames()
    {
        uiScript.ChangeButtonText(uiScript.m1, data.moveDex[playerMon.moveId1].moveName);
        uiScript.ChangeButtonText(uiScript.m2, data.moveDex[playerMon.moveId2].moveName);
        uiScript.ChangeButtonText(uiScript.m3, data.moveDex[playerMon.moveId3].moveName);
        uiScript.ChangeButtonText(uiScript.m4, data.moveDex[playerMon.moveId4].moveName);
    }

    public void SetSelectedMove(string buttonName)
    {
        switch (buttonName)
        {
            case("Move1Button"):
                playerMon.selectedMove = playerMon.moveId1;
                break;
            case("Move2Button"):
                playerMon.selectedMove = playerMon.moveId2;
                break;
            case("Move3Button"):
                playerMon.selectedMove = playerMon.moveId3;
                break;
            case("Move4Button"):
                playerMon.selectedMove = playerMon.moveId4;
                break;
            default:
                playerMon.selectedMove = 0;
                break;
        }
        stateControl.AdvanceState(TurnState.EnemySelectAction);
    }

    public void SelectEnemyMove()
    {
        int[] enemyMoves = new int[] { enemyMon.moveId1, enemyMon.moveId2, enemyMon.moveId3, enemyMon.moveId4 };
        enemyMon.selectedMove = enemyMoves[Random.Range(0, enemyMoves.Length)];
        if(enemyMon.selectedMove == 0)
        {
            SelectEnemyMove();
        }
    }
}
