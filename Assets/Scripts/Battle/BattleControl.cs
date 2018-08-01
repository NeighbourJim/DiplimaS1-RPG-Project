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


    void Start ()
    {                
        Spawn();
        data = GetComponent<DataList>();
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

        playerMon.GenerateStats(5);
        enemyMon.GenerateStats(6);

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
}
