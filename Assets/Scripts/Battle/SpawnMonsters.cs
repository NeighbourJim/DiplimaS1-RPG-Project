using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour {

    public GameObject playerSpawn;
    public GameObject enemSpawn;

    public GameObject player;
    public GameObject enemy;

    public Copymon playerMon;
    public Copymon enemyMon;


    void Start ()
    {                
        Spawn();
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
}
