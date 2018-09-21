using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugControl : MonoBehaviour {

    public GameObject gameData;
    public GameObject playerMonsterDropDown;
    public GameObject enemyMonsterDropDown;

    Dropdown pMonSelect;
    Dropdown eMonSelect;

    Monpedia mp;

    List<int> monIDs;
    List<string> monNames;

	// Use this for initialization
	void Start () {
        mp = gameData.GetComponent<Monpedia>();
        pMonSelect = playerMonsterDropDown.GetComponent<Dropdown>();
        eMonSelect = enemyMonsterDropDown.GetComponent<Dropdown>();
        monIDs = new List<int>();
        monNames = new List<string>();

        FillMonsterDropDowns();
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


	// Update is called once per frame
	void Update () {
		
	}
}
