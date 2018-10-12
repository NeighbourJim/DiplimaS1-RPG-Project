using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    Image screenRect;

    [SerializeField] float timeUntilEncounter;
    [SerializeField] float recentBattleCounter;

    public static bool battledRecently = false;

    SoundManager soundManager;

    Monpedia mp;
    
    private void Awake()
    {
        InitializeData();
    }


    public void InitializeData()
    {
        player = GameObject.FindWithTag("Player");
        playerData = GetComponent<PlayerDataHolder>();
        enemyData = GetComponent<EnemyDataHolder>();
        soundManager = FindObjectOfType<SoundManager>();
        mp = GetComponent<Monpedia>();
        if (GameObject.Find("RegionData"))
            regionData = GameObject.Find("RegionData").GetComponent<RegionDataHolder>();
        if (GameObject.Find("BattleTransitionRect"))
            screenRect = GameObject.Find("BattleTransitionRect").GetComponent<Image>();
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

    public void StartWildBattle()
    {
        battledRecently = true;
        enemyData.SetWildData(regionData.GetEncounterMon(), regionData.GetEncounterLevel(), true);
        ResetBattleTimer();
        ResetRecentBattleTimer();
        StartCoroutine(BattleTransition());
    }

    public void StartTrainerBattle(Trainer t)
    {
        Debug.Log(t.TrainerName + "imgay");
        battledRecently = true;
        enemyData.SetTrainerData(t);
        ResetBattleTimer();
        ResetRecentBattleTimer();
        StartCoroutine(BattleTransition());
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

    IEnumerator BattleTransition()
    {
        soundManager.StopMusic();
        soundManager.PlaySound("EncounterStart");
        for (float a = 0f; a <= 1f; a += Time.deltaTime * 8)
        {
            screenRect.color = new Color(screenRect.color.r, screenRect.color.g, screenRect.color.b, a);
            yield return null;
        }
        screenRect.color = new Color(screenRect.color.r, screenRect.color.g, screenRect.color.b, 1);
        while (soundManager.FindSound("EncounterStart").GetSource().isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("BaseBattle");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeData();
    }
}
