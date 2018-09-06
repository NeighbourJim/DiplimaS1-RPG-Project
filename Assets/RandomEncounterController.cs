using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncounterController : MonoBehaviour {

    GameObject player;
    PlayerDataHolder playerData;
    EnemyDataHolder enemyData;
    RegionDataHolder regionData;

    public int noRecentBattleMinSecs;
    public int noRecentBattleMaxSecs;
    public int recentBattleMinSecs;
    public int recentBattleMaxSecs;

    public float recentBattleTime = 30f;

    [SerializeField] float timeUntilEncounter;
    [SerializeField] float recentBattleCounter;

    public bool battledRecently = false;

    Monpedia mp;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerData = GetComponent<PlayerDataHolder>();
        enemyData = GetComponent<EnemyDataHolder>();
        mp = GetComponent<Monpedia>();
        regionData = GameObject.Find("RegionData").GetComponent<RegionDataHolder>();

        ResetBattleTimer();
    }

    // Update is called once per frame
    void Update ()
    {
        if (player != null)
        {
            if (player.GetComponent<SimpleCharacterControl>().moving && player.GetComponent<SimpleCharacterControl>().inEncounterZone)
            {
                timeUntilEncounter -= Time.deltaTime;
                if (timeUntilEncounter < 0.2f)
                {
                    StartWildBattle();
                }
            }
            if (battledRecently)
            {
                recentBattleCounter -= Time.deltaTime;
                if (recentBattleCounter < 0.2f)
                {
                    battledRecently = false;
                    int r = Random.Range(noRecentBattleMinSecs, noRecentBattleMaxSecs);
                    if (r < timeUntilEncounter)
                    {
                        timeUntilEncounter = r;
                    }
                }
            }
        }
	}

    void StartWildBattle()
    {
        battledRecently = true;
        playerData.SetData();
        enemyData.SetData(regionData.GetEncounterMon(), regionData.GetEncounterLevel());
        ResetBattleTimer();
        ResetRecentBattleTimer();
        SceneManager.LoadScene("BaseBattle");
    }

    void ResetBattleTimer()
    {
        if (battledRecently)
        {
            timeUntilEncounter = (float)Random.Range(recentBattleMinSecs, recentBattleMaxSecs);
        }
        else
        {
            timeUntilEncounter = (float)Random.Range(noRecentBattleMinSecs, noRecentBattleMaxSecs);
        }
    }

    void ResetRecentBattleTimer()
    {
        recentBattleCounter = recentBattleTime;
    }
}
