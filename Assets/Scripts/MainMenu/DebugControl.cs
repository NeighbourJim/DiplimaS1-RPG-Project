using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugControl : MonoBehaviour {

    public GameObject gameData;

    [Header("Monster Select")]
    public GameObject playerMonsterDropDown;
    public GameObject enemyMonsterDropDown;
    [Header("Monster Levels")]
    public GameObject playerMonsterLevel;
    public GameObject enemyMonsterLevel;
    [Header("Player IVs")]
    public GameObject pHPIV;
    public GameObject pATKIV;
    public GameObject pDEFIV;
    public GameObject pSPAIV;
    public GameObject pSPDIV;
    public GameObject pSPEIV;
    [Header("Enemy IVs")]
    public GameObject eHPIV;
    public GameObject eATKIV;
    public GameObject eDEFIV;
    public GameObject eSPAIV;
    public GameObject eSPDIV;
    public GameObject eSPEIV;


    Dropdown pMonSelect;
    Dropdown eMonSelect;
    Slider pLevel;
    Slider eLevel;

    InputField pHP;
    InputField pATK;
    InputField pDEF;
    InputField pSPA;
    InputField pSPD;
    InputField pSPE;

    InputField eHP;
    InputField eATK;
    InputField eDEF;
    InputField eSPA;
    InputField eSPD;
    InputField eSPE;

    Monpedia mp;

    List<int> monIDs;
    List<string> monNames;

	// Use this for initialization
	void Start () {
        AssignObjects();
        monIDs = new List<int>();
        monNames = new List<string>();

        FillMonsterDropDowns();
	}

    void AssignObjects()
    {
        gameData = GameObject.Find("GameDataController");
        mp = gameData.GetComponent<Monpedia>();
        pMonSelect = playerMonsterDropDown.GetComponent<Dropdown>();
        eMonSelect = enemyMonsterDropDown.GetComponent<Dropdown>();

        pLevel = playerMonsterLevel.GetComponent<Slider>();
        eLevel = enemyMonsterLevel.GetComponent<Slider>();

        pHP = pHPIV.GetComponent<InputField>();
        pATK = pATKIV.GetComponent<InputField>();
        pDEF = pDEFIV.GetComponent<InputField>();
        pSPA = pSPAIV.GetComponent<InputField>();
        pSPD = pSPDIV.GetComponent<InputField>();
        pSPE = pSPEIV.GetComponent<InputField>();

        eHP = eHPIV.GetComponent<InputField>();
        eATK = eATKIV.GetComponent<InputField>();
        eDEF = eDEFIV.GetComponent<InputField>();
        eSPA = eSPAIV.GetComponent<InputField>();
        eSPD = eSPDIV.GetComponent<InputField>();
        eSPE = eSPEIV.GetComponent<InputField>();
    }
	
    void FillMonsterDropDowns()
    {
        pMonSelect.ClearOptions();
        eMonSelect.ClearOptions();
        int i = 0;
        foreach (MonData mon in mp.monpedia)
        {
            if(mon != null)
            {
                monIDs.Add(mon.monID);
                monNames.Add(mon.monName);
            }
            i++;
        }

        pMonSelect.AddOptions(monNames);
        eMonSelect.AddOptions(monNames);
    }

    public void StartBattle()
    {
        PlayerDataHolder playerData;
        EnemyDataHolder enemyData;
        playerData = gameData.GetComponent<PlayerDataHolder>();
        enemyData = gameData.GetComponent<EnemyDataHolder>();

        playerData.SetData(monIDs[pMonSelect.value], Mathf.FloorToInt(pLevel.value));
        enemyData.SetData(monIDs[eMonSelect.value], Mathf.FloorToInt(eLevel.value));

        SceneManager.LoadScene("BaseBattle");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
